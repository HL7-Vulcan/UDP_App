// UDPCountryRegionVS.fsh
// Demo country and region value sets used in the UDP app.
// ISO-coded, not NCI — placeholder lists.
// Extend by adding ISO 3166 / ISO 3166-2 entries as required.
// The correct production value set is m11-country-region-vs in M11CountryRegionVS.fsh.

//-----------------------------------------------------
ValueSet: UDPCountryVS
Id: udp-country-vs
Title: "UDP Country Value Set"
Description: """ISO 3166 country codes for sponsor country of record.
This value set uses ISO 3166 coding rather than NCI codes. Extend as needed.
This value set is for demo purposes; the correct value set is m11-country-region-vs."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "UDP"

* $iso3166#AU "Australia"
* $iso3166#FR "France"
* $iso3166#DE "Germany"
* $iso3166#IN "India"
* $iso3166#GB "UK"

//-----------------------------------------------------
ValueSet: UDPRegionVS
Id: udp-region-vs
Title: "UDP Region Value Set"
Description: """ISO 3166-2 region/subdivision codes for sponsor region of record.
This value set uses ISO 3166-2 coding rather than NCI codes. Extend as needed.
This value set is for demo purposes; the correct value set is m11-country-region-vs."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "UDP"

* $iso3166-2#AU-NSW "New South Wales"
* $iso3166-2#DE-BY  "Bavaria"
* $iso3166-2#FR-NOR "Normandy"
* $iso3166-2#IN-UP  "Uttar Pradesh"
* $iso3166-2#GB-WLS "Wales"
