<p align="center"><a href="https://resgate.io" target="_blank" rel="noopener noreferrer"><img width="100" src="https://resgate.io/img/resgate-logo.png" alt="Resgate logo"></a></p>


<h2 align="center"><b>RES Access Demo for C#</b></h2>
</p>

---

An example for [Resgate](https://github.com/resgateio/resgate), written in C#, demonstrating how to grant and revoke access to resources in real time when splitting up the roles of authentication, authorization, and serving resources, into three separate microservices.


## Prerequisite

* [Install and run](https://resgate.io/docs/get-started/installation/) *NATS Server* and *Resgate*.

## Install and run

* Clone *csharp-res-access-demo* repository:
    ```text
    git clone https://github.com/jirenius/csharp-res-access-demo
    ```
* Open the solution, `Demo.sln`, in Visual Studio 2017.
* Set all three projects as *startup projects*, by following [these instructions](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2019).
* Press F5 to build and run.

## Accessing the API
The demo has no web client of its own, but a general viewer can be used instead.  
Go to [resgate.io/viewer](https://resgate.io/viewer/), and enter the resource ID:
```text
awesomeTicker.ticker
```

The ticking counter of the *AwesomeTicker microservice* should now show.

## Things to try out

### Revoke access to resource

While viewing the counter in the viewer client, open the console for the *Authorization microservice*, and press *Enter* to have it switch from public access to private.

The client should immediately have the ticker resource unsubscribed, and the counter should stop.

### Regain access by logging in
Open the browsers development tools (Chrome: *Ctrl + Shift + I*), and under the Console tab, type:
```javascript
client.authenticate("authentication.user", "login", { password: "mysecret" });
```

The *Authentication microservice* will issue an access token for the client connection, allowing the client access again. The counting should resume on the client's next subscription retry (within 3 seconds).

### Lose access by logging out
In the development tools' console tab, type:
```javascript
client.authenticate("authentication.user", "logout");
```

The *Authentication microservice* will revoke the previous access token for the client connection, and the counting should stop immediately.
