🛠️ Certificate Management System – TenneT Offshore Case
Situatieschets
TenneT beheert offshore windmolenparken. De stroom die deze windmolens opwekken, wordt via offshore tussenstations verzameld en doorgestuurd naar het vasteland. Voor werkzaamheden op deze platforms gelden strikte veiligheidseisen. Monteurs moeten gecertificeerd zijn voordat ze toegang krijgen.

2at heeft een systeem ontwikkeld dat deze certificeringsprocessen ondersteunt. Het systeem bestaat uit:

📤 Een portaal waarin monteurs certificaten kunnen uploaden

✅ Een backend waarin medewerkers de certificaten beoordelen

🔄 Een externe API voor het ophalen van gevalideerde certificaten

🔐 Belangrijke thema’s: automatisering, beveiliging en gegevensvalidatie

📚 User Stories
[1] Technician submits certificate via portal
Title: Certificate upload form with Azure Blob integration
Description:
Als technicus wil ik een certificaat (PDF) kunnen uploaden via een beveiligd formulier, zodat dit beoordeeld kan worden voor offshore toegang.

Acceptance Criteria:

Uploadformulier accepteert alleen .pdf-bestanden

Certificaattype wordt geselecteerd via een dropdown

Bestand wordt opgeslagen in Azure Blob Storage

Er wordt een CertificateUpload entiteit aangemaakt met status Pending

Technicus ziet de uploadstatus via een statusoverzicht

Technical Notes:

ASP.NET Core MVC met validatie

Azure Blob Storage SDK

CertificateService via Dependency Injection

[2] Admin reviews uploaded certificate
Title: Review and approval interface for certificate submissions
Description:
Als admin wil ik certificaten kunnen goedkeuren of afwijzen zodat alleen geverifieerde technici toegang krijgen tot offshore platforms.

Acceptance Criteria:

Admin panel toont alle uploads met status Pending

Admin kan goedkeuren of afwijzen met optionele opmerkingen

Status verandert naar Approved of Rejected

Vervaldatum wordt berekend op basis van certificaattype

Alle acties worden gelogd via AuditLogger

Technical Notes:

CertificateReviewService voor statusbeheer

Logging naar logbestand of database

AdminController voor UI en logica

[3] Role-based certificate requirements
Title: Define role eligibility based on required certificates
Description:
Als systeembeheerder wil ik per rol kunnen definiëren welke certificaten vereist zijn, zodat het systeem kan bepalen of iemand geschikt is.

Acceptance Criteria:

Rollen aanmaken, bijwerken en verwijderen

Certificaatvereisten per rol definiëren

Systeem controleert of een technicus voldoet aan de vereisten

Rapportage van ontbrekende of verlopen certificaten per rol

Technical Notes:

Nieuwe modellen: Role, RoleCertificateRequirement

Optioneel: EligibilityCheckService helper

[4] External API for validated certificates
Title: Secure external API to retrieve approved certificates
Description:
Als extern systeem wil ik gevalideerde certificaten kunnen opvragen zodat ik de geldigheid kan controleren.

Acceptance Criteria:

Endpoint: GET /api/certificates/approved?technicianId=...

JSON-response met certificaatgegevens

Informatie bevat type, goedkeuringsdatum, vervaldatum en download-URL

Beveiligd via API Key of OAuth2

Foutafhandeling bij ontbrekende of ongeautoriseerde toegang

Technical Notes:

CertificateQueryService voor data-opvraag

SAS-tokens voor downloadbare bestanden

API Key-validatie via custom middleware

[5] Automatic certificate expiration process
Title: Background job to mark expired certificates
Description:
Het systeem controleert periodiek op verlopen certificaten en past hun status aan, zodat alleen geldige certificaten zichtbaar zijn.

Acceptance Criteria:

Background job draait elke 6 uur

Selecteert alle Approved certificaten met een vervallen datum

Markeert deze als Expired

Elke statusovergang wordt gelogd

Technical Notes:

HostedService via IHostedService of BackgroundService

Gebruik van CertificateRepository en AuditLogger
