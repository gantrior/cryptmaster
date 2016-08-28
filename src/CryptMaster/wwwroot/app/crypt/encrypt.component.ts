import { Component, OnInit } from '@angular/core';

import { CryptService, ICryptError } from '../shared/crypt.service';
import { EncryptModel } from './encryptModel';

@Component({
    moduleId: module.id,
    templateUrl: 'encrypt.component.html'
})

export class EncryptComponent implements OnInit {
    public types: string[];
    public model = new EncryptModel('', "Morse code", '', "encryption");
    public errorMessage: string;
    public isLoadingTypes: boolean;
    public isCrypting: boolean;

    constructor(private cryptService: CryptService) {}

    ngOnInit() {
        this.isLoadingTypes = true;
        this.cryptService.getTypes().subscribe(response => {
            this.types = response;
            this.isLoadingTypes = false;
        });
    }

    public onCryptTypeChanged() {
        this.model.outputText = "";
        this.model.text = "";
        this.errorMessage = "";
    }

    public onSubmit() {
        this.isCrypting = true;
        this.errorMessage = "";
        var config = {
            text: this.model.text,
            type: this.model.type
        };

        if (this.model.cryptType === "encryption") {
            this.cryptService.encrypt(config).subscribe(
                response => {
                    this.model.outputText = response.result;
                    this.isCrypting = false;
                },
                (error: ICryptError) => {
                    this.errorMessage = error.message;
                    this.model.outputText = error.output;
                    this.isCrypting = false;
                });
        } else if (this.model.cryptType === "decryption") {
            this.cryptService.decrypt(config).subscribe(
                response => {
                    this.model.outputText = response.result;
                    this.isCrypting = false;
                },
                (error: ICryptError) => {
                    this.errorMessage = error.message;
                    this.model.outputText = error.output;
                    this.isCrypting = false;
                });
        }
    }
}
