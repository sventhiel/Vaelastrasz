# Vaelastrasz.Library

tba

## Issues

* issues with differences between XML and JSON representation of the schema
* differences in models for post and get
  * post: complex publisher with several properties
  * get: simple provider as a simple string value
  * solution: additional parameters within the url i.e. '?publisher=true&affiliation=true'
* model definitions for required/recommended/optional fields
  * not always that easy to identify that level
  * some properties depend of others, e.g. in general optional, but IF A THAN B
* retrieval of DOIs only from that particular account
  * subsequent creation/insert of DOIs might be tricky
  * get functions are checking valid/correct DOIs (by pattern) as well
* 