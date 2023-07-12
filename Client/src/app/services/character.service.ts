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
        return this.http
        .get<Character[]>(`${environment.apiURL}rooms/${roomId}/characters`)
    }
    GetCharacters(roomId:number): Character[] {
        const characters: Character[] = [];
        this.http
        this.GetCharacterObserv(roomId)
        .subscribe({
            next(value:Character[]) {
                value.forEach((ch:Character) => {characters.push(ch)});
            },
        });
        return characters;
    }
    AddCharacters(roomId:number, characterName:string) {
        return this.http
        .post<Character>(`${environment.apiURL}rooms/${roomId}/characters`,
        {Name: characterName});
    }
    UpateCharacter(roomId:number, character:Character) {
        return this.http
        .put(`${environment.apiURL}rooms/${roomId}/characters/${character.id}`,
        {name: character.name});
    }
}
