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
    GetCharacterObserv(roomId:number) {
        const token = localStorage.getItem("Token")!;
        return this.http
            .get<Character[]>(`${environment.apiURL}rooms/${roomId}/characters`, 
            {headers: {"Authorization": "Bearer " + token}});
    }
    GetCharacters(roomId:number): Character[] {
        const characters: Character[] = [];
        this.GetCharacterObserv(roomId)
        .subscribe({
            next(value:Character[]) {
                value.forEach((ch:Character) => {characters.push(ch)});
            },
        });
        return characters;
    }
    AddCharacters(roomId:number, characterName:string) {
        const token = localStorage.getItem("Token")!;
        return this.http
            .post<Character>(`${environment.apiURL}rooms/${roomId}/characters`,
            {Name: characterName}, {headers: {"Authorization": "Bearer " + token}});
    }
    UpateCharacter(roomId:number, character:Character) {
        const token = localStorage.getItem("Token")!;
        return this.http
            .put(`${environment.apiURL}rooms/${roomId}/characters/${character.id}`,
            {name: character.name}, {headers: {"Authorization": "Bearer " + token}});
    }
}
