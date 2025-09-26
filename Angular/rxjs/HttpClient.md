

```ts
import { HttpClient } from '@angular/common/http';

async fetchData(){
    // .. inside a component or service
    this.http.get('/api/data').subscribe(
        (response) => {
            // handle the successful response here
            console.log('data received:', response);
        },
        (error) => {
            // handle any errors during the request
            console.error('error:', error);
        },
        () => {
            // optional: execute code when the observable completes
            console.log('request complete.');
        }
    )
}

```