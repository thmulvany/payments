Payments Platform
=================

A repo containing all the components of the new Payments Platform excluding RiotPay.

This includes:
* Payment Method Web API
* Price Point Web API
* Order Web API
* RP Storefront Web UI
* Universal Payments Admin Web UI


## Run Locally
Simply perform a `git clone` and open the .SLN file under `/src/WebApi` folder.
Ensure "web" is selected in the "Run" dropdown list from the VS 2015 toolbar (probably defaults to IISExpress)
Click that same Run (with "web" selected) and you have the Payment Method Web API up and running locally on port 5000 
The persisent store is a DEV AWS RDS PostgreSQL instance so make sure you're on VPN


## Special Pre-instuctions For Deploying to Linux via Docker
First you need to have the linux runtime locally that will exist on the target so do:
`dnvm install latest -r coreclr -OS linux -a x64`
This installs it for use with `dnu publish` but does not set it active (as you are on win so you wouldn't want that) 

After `git clone`, open powershell and change into the `/tools` directory.
Run `.\apply-patches.ps1`
This ensures that a patched, low level System.Native.so (native Linux binary), that has been patched for RC2 in Feb, is used now by NpgSQL package when connecting to PG DB.
This only affects running on Linux, not Windows, so locally this will have no affect.

## Deploy to AWS Linux via Docker
Open powershell and change into `/deploy` folder
Run `.\deploy-docker.ps1`
This will publish this web API to `/deploy/publish` and then use Docker Remote API running on a DEV AWS EC2 Ubuntu instance: 10.181.78.45 (Docker remote API on port 4243)
It builds the docker image, spins up a docker container listening on port 5000 but maps that to host's port 80 and then tails the logs for you to see all ILogger activity.
You can now access the web API : `http://10.181.78.45/payments/api/v1/paymentmethods`

### Authentication
The web APIs will use a custom basic authorization scheme that allows clients to have an API Key and pass in Authorization header field.
It's hardcoded in dev to "testuser" so be sure and include this the following in your header when calling the Payment Method API endpoint.
`Authorization: Token dGVzdHVzZXI=`
 