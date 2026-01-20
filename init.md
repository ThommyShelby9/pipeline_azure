CAS PRATIQUE DEVSECOPS

Transformation SecureFintech

Industrialisation d’une Clean Architecture .NET + React




Auteur : Lead DevOps & Architecture

Secteur : Fintech / Bancaire

Date : 12 Janvier 2026



Informations Générales

Repository Source : https://github.com/fturkyilmaz/dotnet-core-clean-react-shopping-project

Stack Technique : Clean Architecture .NET, React, Azure SQL, Azure App Service, Azure Key Vault, IaC (Bicep / Terraform)

Niveau : Avancé – Sécurité, Scalabilité et Industrialisation

1. CONTEXTE MÉTIER (Storytelling)

SecureFintech est une startup Fintech développant une plateforme e‑commerce moderne orientée paiement et gestion de commandes sécurisées.

Le backend repose sur une Clean Architecture .NET avec séparation stricte Domain / Application / Infrastructure / API, tandis que le frontend est développé en React et communique via des APIs REST.

Malgré une architecture logicielle solide, l’industrialisation pose problème : builds manuels, déploiements risqués, migrations non automatisées et secrets stockés en clair dans appsettings.json.

Votre mission consiste à concevoir une chaîne CI/CD DevSecOps robuste garantissant qualité, sécurité et déploiement sans interruption.

2. CONFIGURATION AZURE BOARDS (Agilité Scrum)

Organisation en Scrum avec des sprints de 2 semaines et backlog piloté par la valeur métier.

US‑01 – Infrastructure as Code : Provisionnement Azure via Bicep / Terraform.

US‑02 – CI Global : Build et tests automatisés de la solution .NET + React.

US‑03 – DevSecOps : Analyse SonarCloud et détection de secrets (Gitleaks).

US‑04 – CD : Déploiement via slots Azure App Service.

US‑05 – Secrets : Intégration Azure Key Vault.

3. STRATÉGIE AZURE REPOS

GitFlow modernisé :

main : Production

develop : Intégration continue

Pull Requests avec minimum 1 reviewer

Build de validation obligatoire

Lien obligatoire avec les Work Items

4. ARCHITECTURE AZURE PIPELINES (YAML)

Le pipeline est structuré en stages distincts avec mutualisation via templates YAML.

Stage Build : dotnet restore / build / test

Stage Frontend : npm install + npm run build

Stage Deploy : AzureWebApp@1 avec déploiement ZIP

Templates YAML pour la logique de build réutilisable

5. ENVIRONNEMENTS & SÉCURITÉ

Environnements Azure DevOps : Staging et Production

Gates : Validation manuelle avant mise en production

Secrets injectés dynamiquement depuis Azure Key Vault

Substitution JSON automatique dans appsettings.json

6. LE DÉFI DU LEAD DEVOPS

Après déploiement, l’application affiche une page blanche ou retourne des erreurs 404 côté API.

Analyse : les fichiers statiques React ne sont pas correctement copiés dans wwwroot.

Solution : ajuster le pipeline pour inclure le dossier build React dans les artefacts publiés.

7. CRITÈRES DE SUCCÈS – KPI

Catégorie

Indicateur (KPI)

Objectif

Build

Pipeline CI vert

100% des builds réussis

Tests

Taux de réussite des tests

> 95%

Sécurité

Secrets détectés

0 secret en clair

Qualité

Code Quality Gate

SonarCloud OK

CD

Disponibilité applicative

> 99%

