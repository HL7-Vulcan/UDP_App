//=======================================
//
Instance: SponsorOrganization
InstanceOf: Organization
Title: "Exemplar Sponsor Organization"
Usage: #example
Description: "A minimal Organization definition"

* name = "{{C222495}}" //"Exemplar Pharmaceuticals Co."
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "{{C218677}}" //"Exemplar Road"

//=======================================
//
Instance: CoSponsorOrganization
InstanceOf: Organization
Title: "Exemplar CoSponsor Organization"
Usage: #example
Description: "A minimal Organization definition"

//* name = "Exemplaire Produits Pharmaceutiques Co."
* name = "{{C218678}}" //"Exemplar Pharmazeutika GmBH."
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "{{C218679}}" //"Exemplar Strasse"

//=======================================
//
Instance: LocalSponsorOrganization
InstanceOf: Organization
Title: "Exemplar Local Sponsor Organization"
Usage: #example
Description: "A minimal Organization definition"

* name = "{{C218680}}" //"Fictive Pharmaceuticals (Australia) Pty"
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "{{C218681}}"  //"Fictive High Road"


//=======================================
//
Instance: DevicesOrganization
InstanceOf: Organization
Title: "Exemplar Devices Organization"
Usage: #example
Description: "A minimal Organization definition"

* name = "{{C218682}}" //"Exemplar Devices Ltd"
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "{{C218683}}" //"Exemplar Road"


//=======================================
//
Instance: Exemplar-Regulator-Organization
InstanceOf: Organization
Title: "Exemplar Regulating Authority (ERA)"
Usage: #example
Description: "A minimal Regulator definition"

* name = "Exemplar Regulating Authority (ERA)"
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "Exemplar Road"
* contact[=].address.city = "Exemplarton"
* contact[=].address.postalCode = "EX99 2AB"
* contact[=].address.country = "United Kingdom"