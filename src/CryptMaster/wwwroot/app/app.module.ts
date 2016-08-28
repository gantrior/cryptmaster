import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";

import { AppComponent }  from "./app.component";
import { EncryptComponent } from "./crypt/encrypt.component";
import { routing, appRoutingProviders } from "./app.routing";
import { CryptService } from "./shared/crypt.service";

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        routing
    ],
    providers: [
        appRoutingProviders,
        CryptService
    ],
    declarations: [
        AppComponent,
        EncryptComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
