import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";

interface ICryptConfig {
    text: string;
    type: string;
}

interface ICryptResponse {
    result: string;
}

export interface ICryptError {
    message: string;
    output: string;
}

@Injectable()
export class CryptService {
    constructor(private http: Http) { }

    public getTypes() {
        return (<any>this.http.get("api/v1/crypt/types"))
            .map(response => <string[]>response.json())
            .catch(error => this.throwError(error));
    }

    public encrypt(config: ICryptConfig) {
        return (<any>this.http.post("api/v1/crypt/encrypt", config))
            .map(response => <ICryptResponse>response.json())
            .catch(error => this.throwError(error));
    }

    public decrypt(config: ICryptConfig) {
        return (<any>this.http.post("api/v1/crypt/decrypt", config))
            .map(response => <ICryptResponse>response.json())
            .catch(error => this.throwError(error));
    }

    private throwError(error: any) {
        let errMsg = (error.message) ? error.message :
            error.status ? error.statusText : "Server error";
        let regex = new RegExp("^(.*)<<([^<>]*)>>$");
        if (regex.test(errMsg)) {
            let matches = regex.exec(errMsg);

            return Observable.throw(<ICryptError>{
                message: matches[1],
                output: matches[2]
            });
        } else {
            return Observable.throw(<ICryptError>{
                message: errMsg,
                output: ""
            });
        }
    }
}
