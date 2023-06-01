import { AfterContentChecked, Injectable } from "@angular/core";
import { DM } from "../model/dm";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { environment } from "src/environment";
import { firstValueFrom } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class DmService {

    public dm: DM = {id:0, login:""};

    public constructor(private http: HttpClient) {
        this.GetFullDm();
    }
    public async GetFullDm() {
        var th = this;
        const token = localStorage.getItem("Token");
        return firstValueFrom(th.http.get(environment.apiURL+"dm/full", 
        {
            headers: {"Authorization": token ?? ""},
            observe: 'response'
        })).then(responce => {
            const res = responce as any;
            if (res.status != 204) {
                th.dm.id = res.body.id;
                th.dm.login = res.body.login;
            }
        });
    }
    public Register(login:string, password:string) {
        const dm = {login:login, password:password};
        return this.http.post(environment.apiURL + "dm", dm);
    }
    public SignIn(login:string, password:string) {
        const dm = {login:login, password:password};
        const token = localStorage.getItem("Token");
        let options
        if (token == null)
            options = {}
        else
            options = {headers:{"Authorization": token}}
        return this.http.post(environment.apiURL + "dm/login", dm, options);
    }
    public Logout() {
        this.dm.id = 0;
        this.dm.login = "";
        localStorage.removeItem("Token");
    }
}
