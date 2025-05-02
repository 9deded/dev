# [Signal]


[](https://angular.dev/playground#Signals)
```ts
import { Component, signal, computed } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';



@Component({
    selector: 'app-root',
    template: ` `
})
export class AppComponent {
    // signal
    count = signal(10);

    butter = computed(() => this.count() * 0.1);
    sugar  = computed(() => this.count() * 0.05);
    flour  = computed(() => this.count() * 0.2);

    update(event: Event){
        const input = event.target as HTMLInputElement;
        this.count.set(parseInt(input.value));
    }

    // control flow
    todos: Array<{done: boolean; text: string}> = [];

    add(text: string){
        this.todos.push({text, done: false});
    }

    toggle(index, number){
        this.todos[index].done = !this.todos[index].done;
    }

 }

bootstrapApplication(AppComponent);
```

