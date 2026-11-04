ValueSet: M11CountryRegionVS
Id: m11-country-region-vs
Title: "ICH M11 Study Amendment Country Value Set"
Description: """Codes drawn from
ISO 3166 Country Codes, Alpha 3;
ISO 3166 Country Codes, Alpha 2;
country/region codes. This is ICH M11 Value Set represented here in FHIR format.

The demo app uses udp-country-vs with a small set of countries and udp-region-vs with a small set of regions but the correct value set is m11-country-region-vs (extensible).
Demo value sets are defined in UDPTerminology/UDPCountryRegionVS.fsh.

"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 1
* ^experimental = false
* ^status = #active

* codes from valueset http://hl7.org/fhir/ValueSet/iso3166-1-2
* codes from valueset http://hl7.org/fhir/ValueSet/iso3166-1-3
//* codes from "GENC"  // Not immediately obvious how this is to be used: https://nsgreg.nga.mil/doc/view?i=2500&month=7&day=4&year=2025
