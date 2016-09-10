#! usr/bin/python
#coding=utf-8

from BeautifulSoup import BeautifulSoup
import re

doc = ['<html><head><title>Page title</title></head>',
       '<body><p id="firstpara" align="center">This is paragraph <b>one</b>.',
       '<p id="secondpara" align="blah">This is paragraph <b>two</b>.',
       '</html>']
soup = BeautifulSoup(''.join(doc))

##find all return a list of objects
#By setting the para as True to get all tags
print allTags = soup.findAll(True)
#By setting the para to None to return item without contain a property
print soup.findAll(align=None)
#find a list, dictionary
print soup.findAll(['title', 'p'])
#regex expression
print soup('p',align=re.compile('^b.*'))
print soup.findAll('b')
#find by attr filter
print soup.findAll(id='firstpara')
print soup.findAll(attrs={'id':re.compile("para$")})
#callable object
print soup.findAll(lambda t: len(t.attrs)==2)


##find return a BeautifulSoup.Tag
print soup.find('p',id='secondpara')

##Functions used to navigate in relative position
#Navigate by tag
print 'titleTag: ', soup.html.head.title
#Navigate by index, if the contents[0] is a Navigate string, it is same as soup.contents[0].string
print soup.contents[0].contents[0]

#Iterating over the tag
for i in soup.body:
    print i
# <p id="firstpara" align="center">This is paragraph <b>one</b>.</p>
# <p id="secondpara" align="blah">This is paragraph <b>two</b>.</p>

len(soup.body)
# 2
len(soup.body.contents)
# 2


#use type to check return data type
print type(soup)

#use prettify to format output
#print soup.prettify()
# <html>
#  <head>
#   <title>
#    Page title
#   </title>
#  </head>
#  <body>
#   <p id="firstpara" align="center">
#    This is paragraph
#    <b>
#     one
#    </b>
#    .
#   </p>
#   <p id="secondpara" align="blah">
#    This is paragraph
#    <b>
#     two
#    </b>
#    .
#   </p>
#  </body>
# </html>
