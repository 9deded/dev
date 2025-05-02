# [Anchor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper)


https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro
https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper


### asp-controller
```
<a asp-controller="Speaker" asp-action="Index">All Speakers</a>
```
the generated:
```
<a href="/Speaker">All Speakers</a>
```

### asp-action
```
<a asp-controller="Spraker" asp-action="Evaluations">Speaker Evaluations</a>
```
the generated:
```
<a href="/Speaker/Evaluations">Speaker Evaluations</a>
```

### asp-route-{value}
```
<a asp-controller="Speaker" asp-action="Detail" asp-route-id="123">SpeakerId: 123</a>
```
the generated:
```
<a href="/Speaker/Detail/12">SpeakerId: 123</a>
```

### asp-route
```
c#
[Route("/Speaker/Evaluations", Name = "speakerevals")]

razor
<a asp-route="speakereval">Speaker Evaluations</a>
```
the generated:
```
<a href="/Speaker/Evaluations">Speaker Evaluations</a>
```

### asp-all-route-data
```
@{
    var param = new Dictionary<string, string>{{"speakerId","11}, {"currentYear","true"}};
}

<a asp-route="speakerevalscurrent" asp-all-route-data="param">Speaker Evaluations</a>
```
the generated:
```
<a href="/Speaker/EvaluationsCurrent?speakerId=11&currentYear=true">Speaker Evaluations</a>
```

### asp-fragment
```
<a asp-controller="Speaker" asp-action="Evaluations" asp=fragment="SpeakerEvaluations">Speaker Evaluations</a>
```
the generated:
```
<a href="/Speaker/Evaluation#SpeakerEvaluations">Speaker Evaluations</a>
```

### asp-area
```
<a asp-area="Sessions" asp-page="/Index">View Sessions</a>
```
the generated:
```
<a href="/Sessions">View Sessions</a>
```

### usage in MVC
```
<a asp-area="Blogs" asp-controller="Home" asp-action="AboutBlog">About blog</a>
```
the generated
```
<a href="/Blogs/Home/AboutBlog">About blog</a>
```

### asp-protocol
```
<a asp-protocol="https" asp-controller="Home" asp-action="About">About</a>
```
the generated:
```
<a href="https://localhost/Home/About">About</a>
```

### asp-host
```
<a asp-protocol="https" asp-host="microsoft.com" asp-controller="Home" asp-action="About">About</a>
```
the generated:
```
<a href="https://microsoft.com/Home/About">About</a>
```

### asp-page
```
<a asp-page="/Attendee" asp-route-attendeeid="10">All Attendees</a>

<a href="/Attendee?attendeeid=10">All Attendees</a>  
```

### asp-page-handler
``` 
<a asp-page="/Attendee" asp-page-handler="Profile" asp-route-attendeeid="12"> Attendee Profile </a>

<a href="/Attendee?attendeeid=12&handler=Profile"> Attendee Profile </a>
```

~~~
code
~~~
















https://www.jetbrains.com/help/hub/markdown-syntax.html