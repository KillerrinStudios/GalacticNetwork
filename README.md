# GalacticNetwork
Authentication using Skoruba.IdentityServer4.Admin


## Local Debugging
To run the Admin Panel or API locally, the following values need to be swapped in the Database

* ClientRedirectionUris
    * Id:1 | http://localhost:9000/signin-oidc <-> https://killerrin-studios-gn-is4-admin.azurewebsites.net/signin-oidc
    * Id:2 | http://localhost:5001/swagger/oauth2-redirect.html <-> https://killerrin-studios-gn-is4-admin-api.azurewebsites.net/swagger/oauth2-redirect.html
* Clients
    * Id:1 | http://localhost:9000/ <-> https://killerrin-studios-gn-is4-admin.azurewebsites.net/
    * Id:2 | NULL <-> NULL
