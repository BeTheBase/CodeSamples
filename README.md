Casus developer
Situatieschets
TenneT beheert offshore windmolenparken. De stroom van deze windmolens wordt bij
offshore tussenstations “verzameld” en doorgesluisd naar het land. Elke dag moeten er
werkzaamheden plaatsvinden op deze platforms, maar zowel deze werkzaamheden als
de reis ernaartoe zijn niet zonder risico. TenneT stelt daarom strenge eisen aan de
certificeringen waaraan monteurs moeten voldoen voordat ze naar zo’n platform toe
mogen. Het systeem dat dit bijhoudt is gebouwd door 2at.
Ons systeem registreert rollen en de bijbehorende certificaten. Dit omvat:
▪ Een portaal waarin gebruikers zelf certificaten kunnen uploaden.
▪ Een backend waarin medewerkers de ingediende informatie valideren.
▪ Een extern systeem reeds goedgekeurde certificaten die beschikbaar zijn voor
gebruik.
Belangrijke aandachtspunten: automatisering, beveiliging en gegevensvalidatie.



[User Story] #1 – Technician submits certificate via portal
Title: Certificate upload form with Azure Blob integration
 Description:
 As a technician, I want to upload my certification (PDF) via a secure form so that it can be reviewed for offshore access.
Acceptance Criteria:
Upload form accepts only .pdf files.


Technician must select certificate type (from dropdown).


Uploaded file is stored via Azure Blob Storage.


System creates CertificateUpload entity with status Pending.


Upload result is visible to the technician via status history.


Technical Notes:
ASP.NET Core MVC form with validation


Azure Blob Storage SDK for file handling


CertificateService injected via DI



[User Story] #2 – Admin reviews uploaded certificate
Title: Review and approval interface for certificate submissions
 Description:
 As an admin, I want to approve or reject submitted technician certificates so that only validated technicians gain access.
Acceptance Criteria:
Admin panel lists all submissions with Pending status


Admin can approve or reject, with optional comments


Status transitions to Approved or Rejected


Expiry date is calculated on approval based on certificate type validity


All actions are logged via AuditLogger


Technical Notes:
CertificateReviewService handles state changes


Logging writes to audit log file or DB


AdminController manages views and actions



[User Story] #3 – Role-based certificate requirements
Title: Define role eligibility based on required certificates
 Description:
 As a system admin, I want to configure which certificates are required per role, so that the system can determine eligibility.
Acceptance Criteria:
Create, update, delete roles


Define certificate requirements per role


Check eligibility of technician against a role


System reports missing or expired certificates per role


Technical Notes:
Add Role, RoleCertificateRequirement models


Eligibility check service (optional helper)



[User Story] #4 – External API for validated certificates
Title: Secure external API to retrieve approved certificates
 Description:
 As an external system, I want to request a list of approved technician certificates so that I can verify their validity.
Acceptance Criteria:
REST endpoint: GET /api/certificates/approved?technicianId=...


Returns JSON array of approved certificates


Each entry includes type, approval date, expiry date, and download URL


Secured via API Key or OAuth2


API fails gracefully on missing or unauthorized access


Technical Notes:
CertificateQueryService fetches data


SAS-token used for file download URLs


API Key validation via custom middleware



[User Story] #5 – Automatic certificate expiration process
Title: Background job to mark expired certificates
 Description:
 As the system, I want to periodically check for expired certificates and update their status so that outdated data is not shown as valid.
Acceptance Criteria:
Background service runs every 6 hours


Selects all Approved certificates past expiry


Marks them as Expired


Logs each status transition


Technical Notes:
HostedService using IHostedService or BackgroundService


Uses CertificateRepository and logs with AuditLogger
