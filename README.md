# sulmar-mon-netcore3-group1

## Protokół HTTP 

request:
~~~
  GET /customers/index.html HTTP/1.1
  host: www.sulmar.pl
  accept: text/html
  {blank-line}
~~~

response:
~~~
  200 OK
  content-type: text/html
  
  <html>...</html>
~~~

request:
~~~
GET api/customers HTTP/1.1
  host: www.sulmar.pl
  accept: application/json
  {blank-line}
~~~

response:
~~~  200 OK
  content-type: application/json
  
  {json}
~~~


request:
~~~ 
 POST api/customers HTTP/1.1
  host: www.sulmar.pl
  content-type: application/xml
  <xml><customer>...</customer></xml>
  {blank-line}
~~~

response:
~~~
201 Created
~~~

