import { enableProdMode } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app.module";

// extend Observable through the app
import "rxjs/Rx";

// disable this for development
enableProdMode();

platformBrowserDynamic().bootstrapModule(AppModule);
