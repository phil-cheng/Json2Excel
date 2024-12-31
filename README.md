## 背景
- 因工作需要，需要学习winform，于是花了一天的时间使用AI做了一款为json数组转excel的工具

## 操作
- 用户输入json，然后使用第三方工具包转换成json预览模式，如果当前节点为Array，则转换成二维表的方式使用excel导出来


## 测试json
```
{
    "title": "test 123",
    "content": {
        "foo": "bar",
        "name": "cf"
    },
    "read": false,
    "ttl": [
        1,
        2,
        3
    ],
    "array": [
        [1,2,3],
        [4,5,6]
    ],
    "user": [
        {
            "name": "alice",
            "age": "30"
        },
        {
            "name": "bob",
            "age": "20"
        }
    ],
    "stu": {
      "susan": {
           "age": 10,
           "sex": 1     
      },
      "lily": {
           "age": 10,
           "sex": 1     
      }
    }
}
```