# Vaelastrasz

The project enables communication with DataCite to manage DOIs. This includes the creation, updating, and deletion of DOIs. It can be integrated directly into [BEXIS2](https://github.com/BEXIS2/Core) or used as a [standalone service](https://github.com/sventhiel/Vaelastrasz/tree/master/Vaelastrasz.Server).

## General Information

<details>
<summary><h3>What does the project do?</h3></summary>

<!--
	Die Verwaltung von DOIs via DataCite's Webseite ist umständlich und zeitaufwändig. Hier setzt Vaelastrasz an und bietet eine einfache Möglichkeit, DOIs zu verwalten. Hierzu wird eine REST API bereitgestellt, die es ermöglicht, DOIs zu erstellen, zu aktualisieren und zu löschen. Diese API kann direkt in BEXIS2 integriert oder als Stand-Alone-Service genutzt werden. Dabei werden verschiedene Funktionen bereitgestellt, die eine einfache Verwaltung von DOIs ermöglichen. Die API ist so gestaltet, dass sie einfach zu bedienen ist und eine schnelle Integration in bestehende Systeme ermöglicht.
-->
  * BEXIS2 und Stand-Alone
  * DOI Workflow
  * DataCite
</details>

<details>
<summary><h3>What is the app not designed for?</h3></summary>
  ...
</details>

<details>
<summary><h3>Where does the name comes from?</h3></summary>
  * (World of) Warcraft
</details>

## Project(s)

The repository consists of several projects, which are briefly described below.

<details>
<summary><h3>Vaelastrasz.Library</h3></summary>
	This project contains core functionalities of the DataCite workflow(s) that are used by both the Vaelastrasz.Server and corresponding BEXIS2 instance(s). 
	It includes entities, models, schemas and services to manage all relevant information necessary for the DataCite workflow(s) - i.e. accounts, users, placeholders and DOIs. 
</details>

<details>
<summary><h3>Vaelastrasz.Library.Tests</h3></summary>
  * Tests für Funktionen der Bibliothek
</details>

<details>
<summary><h3>Vaelastrasz.Server</h3></summary>
  * ReST API für die Kommunikation mit DataCite
  * Weitere Funktionen (Schema, Namensauflösung von Personen,...)
  * Exceptionless
  * Swagger
  * Serilog
  * LiteDB
</details>

<details>
<summary><h3>Vaelastrasz.Server.Tests</h3></summary>
  * Tests für Funktionen des Servers - limitiert, da keine Integrationstests
</details>

## Errors / Troubleshooting

Despite intensive planning, careful implementation, and continuous improvement, error messages can occur for a variety of reasons (e.g. related to source code or incorrect usage). We understand that encountering such messages can be frustrating and we are glad to assist you in resolving your issue(s).

Please feel free to reach out to us regardless of the nature of the problem. Whether you are unable to resolve the issue, have encountered a new undocumented problem, or have additional insights and solutions to share, we encourage you to contact us. Your feedback is invaluable in helping us improve and provide the best possible support.

<details>
<summary><h3>405 - method not allowed</h3></summary>
  Check out the host of the used account.
</details>