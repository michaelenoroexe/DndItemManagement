import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DmService } from "./dm.service";
import { environment } from "src/environment";
import { Character } from "../model/character";

@Injectable({
    providedIn: "root"
})
export class CharacterService {

    constructor(private http:HttpClient, private dmService:DmService) { }

    GetCharacters(roomId:number): Character[] {
        const characters: Character[] = [];
        this.http
        .get<Character[]>(`${environment.apiURL}dm/${this.dmService.dm.id}/rooms/${roomId}/characters`)
        .subscribe({
            next(value:Character[]) {
                value.forEach((ch:Character) => {characters.push(ch)});
            },
        });
        return characters;
    }
    AddCharacters(roomId:number, characterName:string) {
        return this.http
        .post<Character>(`${environment.apiURL}dm/${this.dmService.dm.id}/rooms/${roomId}/characters`,
        {Name: characterName});
    }
    UpateCharacter(roomId:number, character:Character) {
        return this.http
        .put(`${environment.apiURL}dm/${this.dmService.dm.id}/rooms/${roomId}/characters/${character.id}`,
        {name: character.name});
    }
}
