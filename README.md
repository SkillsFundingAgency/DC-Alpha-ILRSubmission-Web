# DC-Alpha-ILRSubmission-Web

## Overview

Alpha POC Web front end to allow submitting an ILR file for validation, created as part of the alpha to see feasibility of Service Fabric to host the DC services.

## Instructions

- Clone this repository
- Ensure computer has Service Fabric SDK installed and a local instance is running (5 nodes)
- Open the solution file with Visual Studio in admin mode
- Edit the ApplicationParameters\Cloud.xml file and enter details for valid Azure Service Bus, Azure Storage & Application Insights instances
- Run the solution
- Browse to http://localhost:8409/