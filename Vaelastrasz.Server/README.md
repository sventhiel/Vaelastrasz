# Vaelastrasz.Server

Das Projekt beinhaltet die serverseitige Komponente der Vaelastrasz-Anwendung. Es handelt sich um eine .NET 8 Web-API Webanwendung, die als Backend dient. Die Anwendung bietet RESTful APIs zur Verwaltung von Benutzern, Aufgaben und anderen Ressourcen, welche im DataCite DOI Workflow relevant sind.

## Database

Die Anwendung nutzt mit [LiteDB](https://github.com/litedb-org/LiteDB) eine sehr leichtgewichtige Datenbank, die direkt in .NET integriert ist. Das bedeutet, dass keine zusätzliche Software wie ein SQL-Server oder eine externe NoSQL-Datenbank installiert oder eingerichtet werden muss. Stattdessen arbeitet die Anwendung mit einer einfachen Datei, in der alle Daten gespeichert werden. Diese Datei liegt lokal im Dateisystem und wird von der Anwendung automatisch verwaltet.

Für den Start genügt es daher die Anwendung auszuführen. Eine gesonderte Datenbankinstallation oder -konfiguration entfällt. Das macht den Umgang besonders unkompliziert und erleichtert sowohl die Entwicklung als auch das Deployment erheblich. [LiteDB](https://github.com/litedb-org/LiteDB) ist darauf ausgelegt, überschaubare Datenmengen zuverlässig und schnell zu verwalten. Für die vorgesehenen Anwendungsfälle dieser Anwendung ist das vollkommen ausreichend. Sollte es in Zukunft jedoch um größere Datenbestände oder eine besonders hohe Anzahl gleichzeitiger Zugriffe gehen, wäre der Einsatz einer klassischen Datenbanklösung sinnvoll.

Für die Verwaltung der Datenbank, jeneits der Möglichkeiten via Web-API, benötigen Sie ein Tool für den Zugriff - ähnlich wie beispielsweise pgAdmin für PostgreSQL. Für unsere Anwendung mit [LiteDB](https://github.com/litedb-org/LiteDB) ist dies [LiteDB.Studio](https://github.com/litedb-org/LiteDB.Studio). Eine ausführlicher Einführung finden Sie in dem entsprechenden Abschnitt [LiteDB.Studio](#LiteDB.Studio)

## Requirements

In diesem Abschnitt werden alle notwendigen Requirements aufgelistet und versucht die Gründe bzw. den Nutzen näher zu beschreiben.

## Installation

Im folgenden Abschnitt wird die Installation von _*Vaelastrasz.Server*_ in verschiedenen Umgebungen bzw. Systemarchitekturen gezeigt und erläutert. Auf Grund fehlender Expertise werden ausschließlich zwei Architekturen näher betrachtet. Zum einen handelt es sich um *Armbian', ein ARM-basiertes Linux-Betriebssystem, sowie Windows Server 2016. Falls Sie das System auf einer anderen Distribution/Architektur installieren, sind wir sehr an Ihrer Dokumentation interessiert. Damit können wir diesen Abschnitt mit ausführlicheren Informationen anreichern und erleichtern die Installation für andere Benutzer.

### Linux (e.g Debian/Ubuntu/Armbian)

Für Linux existieren unzählige Distributionen. Daher ist es nahezu unmöglich auf die Besonderheiten jeder Einzelnen einzugehen. Als Demonstrator wird _*Vaelastrasz.Server*_ auf einem armv7-basierten [BananaPi M2 Zero](https://deviwiki.com/wiki/Banana_Pi_BPI_M2_Zero) mit Armbian betrieben. Aus diesem Grund befasst sich dieser Abschnitt exemplarisch mit der Installation des Systems auf eben dieser Architektur. Sollten Sie ein anderes Derivat von Linux benutzen, müssen gegebenenfalls Anpassungen vorgenommen werden, so dass diese Anleitung höchstens als Unterstützung/Hilfestellung diesen kann.

Neben zusätzlicher Dokumentation für die Installation auf weiteren Linux-Distributionen sind wir auch an Ihrem Feedback interessiert. Sollten Sie Fragen oder Verbesserungsvorschläge haben, auf Probleme bzw. Bugs stoßen oder einfach Hilfe benötigen, melden Sie sich bitte bei uns.

#### .NET SDK / Runtime Environment

Im benutzten Betriebssystem (Armbian) gibt es leider keinen Support durch den Packagemanager für die Installation von .NET 8 SDKs bzw. Runtimes. Daher müssen Sie sich die Binaries ([SDK](https://builds.dotnet.microsoft.com/dotnet/Sdk/8.0.413/dotnet-sdk-8.0.413-linux-arm.tar.gz), [Runtime](https://builds.dotnet.microsoft.com/dotnet/aspnetcore/Runtime/8.0.19/aspnetcore-runtime-8.0.19-linux-arm.tar.gz)) direkt von Microsoft herunterladen und anschließend installieren. Nachfolgend möchte ich meine genutzten Befehle für die Installation von .NET 8 mit Ihnen teilen. An dieser Stelle sei erwähnt, dass diese bei Ihnen unter Umständen nicht ohne Änderungen bzw. Anpassungen funktionieren wird. Vor allem wenn Sie eine andere Systemarchitektur verwenden.

Folgender Befehl sorgt dafür, dass Sie das SDK in Version 8.0.413 für ARM-basierte Prozessoren direkt von Microsoft herunterladen. Abhängig davon, wann Sie das SDK herunterladen möchten, kann bereits eine neuere Version verfügbar sein. Dennoch sollte das Projekt unabhängig von dem Release mit jeder Version von .NET 8.0 funktionieren. Ebenso können Sie die [Runtime Environment](https://builds.dotnet.microsoft.com/dotnet/aspnetcore/Runtime/8.0.19/aspnetcore-runtime-8.0.19-linux-arm.tar.gz) herunterladen und für die Verwendung benutzen. Als dritte bzw. vierte Option können Sie das Projekt mit der Option "self-contained" veröffentlichen, oder eine solche Version von [Vaelastrasz.Server](https://github.com/sventhiel/Vaelastrasz/releases) herunterladen und verwenden.

Beachten Sie bitte, dass Sie in den letzten beiden Fällen nachfolgende Anweisungen in diesem Abschnitt ignorieren können.

```bash
sventhiel@bananapi:~$ wget https://builds.dotnet.microsoft.com/dotnet/Sdk/8.0.413/dotnet-sdk-8.0.413-linux-arm.tar.gz
```

Nachfolgend soll der Ordner _/user/share/dotnet_ erstellt werden.

```bash
sventhiel@bananapi:~$ mkdir -p /usr/share/dotnet
```

Im nächsten Schritt wird das heruntergeladene Archiv in den zuvor erstellten Ordner entpackt.

```bash
sventhiel@bananapi:~$ tar -xvzf dotnet-sdk-8.0.413-linux-arm.tar.gz -C /usr/share/dotnet
```

Für den erstellten Ordner müssen noch Zugriffsrechte gesetzt werden.

```bash
sventhiel@bananapi:~$ sudo chown -R root:root /usr/share/dotnet
```

In diesem Schritt öffnen Sie bitte die Datei _~/.bashrc_ um nachfolgende Zeilen hineinzuschreiben.

```bash
sventhiel@bananapi:~$ nano ~/.bashrc
```

Die zuvor geöffnete Datei muss angepasst werden, damit Sie in Ihrem System den Befehl _dotnet_ benutzen können.

```conf
...
export DOTNET_ROOT=/usr/share/dotnet
export PATH=$PATH:/usr/share/dotnet
```

Grundsätzlich wäre nun ein Neustart nötig. Um dies zu umgehen, können Sie einfach folgenden und letzten Befehl ausführen.

```bash
sventhiel@bananapi:~$ source ~/.bashrc
```

#### Vaelastrasz.Server

#### Service

#### Webserver / Reverse Proxy

### Windows Server 2022

#### C# SDK / Runtime Environment

#### Vaelastrasz.Server

#### Service

#### Webserver / Reverse Proxy

## <a id="LiteDB.Studio">LiteDB.Studio</a>

## Manual / Guide

## Contributing

## License


