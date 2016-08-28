import { Routes, RouterModule } from "@angular/router";

import { EncryptComponent } from "./crypt/encrypt.component";

const appRoutes: Routes = [
    { path: "encrypt", component: EncryptComponent },
    { path: "**", component: EncryptComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);
