import { HttpClient } from "@angular/common/http";
import { ErrorHandler, Injectable, isDevMode } from "@angular/core";
import { environment } from "src/environment";

@Injectable({
  providedIn: "root"
})
export class LoggerProvider {

  constructor(private http:HttpClient) {}

  GetLogger(path:string) {
    return new Logger (
      this.http,
      isDevMode() ? LogLevel.Information : LogLevel.Warning,
      path
    )
  }
}
@Injectable()
export class GlobalErrorLogger implements ErrorHandler {

  private logger:Logger

  constructor(http:HttpClient) {
    this.logger = new Logger(http, LogLevel.Error, "Global");
  }

  handleError(error: any): void {
    this.logger.Error("Something went wrong", error);
  }
}
export class Logger {

  private readonly source = "Client";
  private readonly http:HttpClient;
  private readonly logLevel:LogLevel;
  private readonly requestPath:string

  private Log(level:LogLevel, message:string) {
    this.http.post("http://localhost/log/", {
      Level: LogLevel[level],
      Properties: {
        Source: this.source,
        RequestPath: this.requestPath,
      },
      RenderedMessage: message,
    }).subscribe({next() {}, error() {}});
  }
  private LogErr(level:LogLevel, err:Error) {
    this.http.post("http://localhost/log/", {
      Level: LogLevel[level],
      Properties: {
        Source: this.source,
        RequestPath: this.requestPath,
      },
      Exception: err.stack,
      RenderedMessage: err.message.split("Error:")[1]
    }).subscribe({next(inf) {console.log(inf)}, error(err) {console.error(err)}});
  }

  constructor(http:HttpClient, logLevel:LogLevel, requestPath:string) {
    this.http = http;
    this.logLevel = logLevel;
    this.requestPath = requestPath;
  }

  public Information(message:string) {
    if (this.logLevel > LogLevel.Information) return;
    this.Log(LogLevel.Information, message);
  }

  public Warning(message:string) {
    if (this.logLevel > LogLevel.Warning) return;
    this.Log(LogLevel.Information, message);
  }

  public Error(message:string = "Error while execution", err:Error | null = null) {
    if (this.logLevel > LogLevel.Error) return;
    if (err != null) this.LogErr(LogLevel.Error, err)
    else this.Log(LogLevel.Error, message)
  }

  public Critical(message:string = "Error while execution", err:Error | null = null) {
    if (this.logLevel > LogLevel.Critical) return;
    if (err != null) this.LogErr(LogLevel.Error, err)
    else this.Log(LogLevel.Error, message)
  }
}
export enum LogLevel {
  Information,
  Warning,
  Error,
  Critical
}
