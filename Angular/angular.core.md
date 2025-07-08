








## EventEmiter
```ts
const EventEmitter: { 
    new (isAsync?: boolean | undefined): EventEmitter<any>;
    new <T>(isAsync? boolean | undefined): EventEmitter<T>;
    readonly prototype: EventEmitter<any>;
}
```

```ts
// child component
import { Component EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-emitter',
    template: `<button (click)="deleteItem()">Delete</button>`
})
export class ItemComponent {
    @Output() myEvent = new EventEmitter<any>(); // or EventEmitter<void>

    deleteItem(){
        this.myEvent.emit(data);

        this.onDelete.emit();
    }
}
---
(myChildComponent.myEvent)="handleEvent($vent)"
---

// parent component
import { Component } from '@angular/core';

@Component({
    selected: 'app-list',
    template: `<app-item (onDelete)="handlerDelete()"></app-item>"
})


```