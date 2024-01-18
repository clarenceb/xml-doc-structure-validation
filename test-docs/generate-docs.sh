#!/bin/bash

cat <<EOF > InvalidSOAP-attr-value-length.xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:web="http://www.example.com/">
   <soapenv:Header/>
   <soapenv:Body>
      <web:ExampleRequest>
         <web:ExampleElement attribute1="$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 2050 | head -n 1)">
            Testing
         </web:ExampleElement>
      </web:ExampleRequest>
   </soapenv:Body>
</soapenv:Envelope>
EOF

cat <<EOF > InvalidSOAP-max-text-length.xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:web="http://www.example.com/">
   <soapenv:Header/>
   <soapenv:Body>
      <web:ExampleRequest>
         <web:ExampleElement attribute1="This is a valid attribute value.">
            $(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 17000 | head -n 1)
         </web:ExampleElement>
      </web:ExampleRequest>
   </soapenv:Body>
</soapenv:Envelope>
EOF

cat <<EOF > InvalidSOAP-max-CDATA-length.xml
<?xml version="1.0"?>
<soap:Envelope xmlns:soap="http://www.w3.org/2003/05/soap-envelope">
  <soap:Header>
  </soap:Header>
  <soap:Body>
    <m:GetData xmlns:m="http://www.example.org/data">
      <m:Request>
        <m:Item>
          <![CDATA[
            $(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 17000 | head -n 1)
          ]]>
        </m:Item>
      </m:Request>
    </m:GetData>
  </soap:Body>
</soap:Envelope>
EOF

cat <<EOF > InvalidSOAP-max-message-size.xml
<?xml version="1.0"?>
<soap:Envelope xmlns:soap="http://www.w3.org/2003/05/soap-envelope">
  <soap:Header>
  </soap:Header>
  <soap:Body>
    $(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 2621450 | head -n 1)
  </soap:Body>
</soap:Envelope>
EOF
