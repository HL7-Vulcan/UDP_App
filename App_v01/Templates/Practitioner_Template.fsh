//=======================================
//
Instance: SponsorPractitioner
InstanceOf: Practitioner
Title: "Exemplar Sponsor Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* name.text = "{{C222495}}" //"Dr Exemplar Practitioner"
// assume Sponsor as an individual has the same legal address
* address[+].line[+] = "{{C218677}}" //"Exemplar Road"


//=======================================
//
Instance: CoSponsorPractitioner
InstanceOf: Practitioner
Title: "Exemplar CoSponsor Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* name.text = "{{C218678}}" //"Dr Exemplar von Practitioner"
// assume Sponsor as an individual has the same legal address
* address[+].line[+] = "{{C218679}}" //"Exemplar Strasse"


//=======================================
//
Instance: LocalSponsorPractitioner
InstanceOf: Practitioner
Title: "Exemplar Local Sponsor Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* name.text = "{{C218680}}" //"Bruce Exemplar"
// assume Sponsor as an individual has the same legal address
* address[+].line[+] = "{{C218681}}" //"Fictive High Road"

//=======================================
//
Instance: SponsorExpertPractitioner
InstanceOf: Practitioner
Title: "Exemplar Sponsor Expert Medical Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* text.status = #generated
* text.div = "<div xmlns=\"http://www.w3.org/1999/xhtml\" lang = \"en\" xml:lang = \"en\">
  {{C222063}}
    </div>"
* name.text = "{{C218693}}" //"Dr Exemplar Consultant"