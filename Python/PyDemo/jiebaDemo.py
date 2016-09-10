#encoding=utf-8
import jieba

#not support in windows server
#jieba.enable_parallel(4) 

seg_list = jieba.cut("我来到北京清华大学",cut_all=True)
print "Full Mode:", "/ ".join(seg_list) #全模式

seg_list = jieba.cut("我来到北京清华大学",cut_all=False)
print "Default Mode:", "/ ".join(seg_list) #精确模式

seg_list = jieba.cut("他来到了网易杭研大厦") #默认是精确模式
print ", ".join(seg_list)

seg_list = jieba.cut_for_search("小明硕士毕业于中国科学院计算所，后在日本京都大学深造") #搜索引擎模式
print ", ".join(seg_list)

seg_list = jieba.cut_for_search("李小福是创新办主任也是云计算方面的专家")
print ", ".join(seg_list)

# load customer dict file
jieba.load_userdict("G:\GitHub\MyRepository\Python\TestFiles\user_dict.txt")
seg_list = jieba.cut_for_search("李小福是创新办主任也是云计算方面的专家")
print ", ".join(seg_list)

import jieba.posseg as pseg
words =pseg.cut("我爱北京天安门")
for w in words:
    print w.word,w.flag

result = jieba.tokenize(u'永和服装饰品有限公司')
for tk in result:
    print "word %s\t\t start: %d \t\t end:%d" % (tk[0],tk[1],tk[2])
