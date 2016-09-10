import xml.etree.ElementTree as ET

data='''
<person>
    <name>Thomas</name>
    <comment word="1">Hello World!</comment>
    <email hide="yes"/>
</person>'''

tree=ET.fromstring(data)
print 'Name:',tree.find('name').text
print 'Attr:',tree.find('email').get('hide')
