import argparse
import os
import re
from jinja2 import Template
from jinja2 import Environment, FileSystemLoader

def main(protobuf_code_path, save_path):
    file = open(os.path.join(protobuf_code_path, "MsgCode.java"), 'r', encoding='utf-8')
    content = file.read()

    # 效验
    files = os.listdir(save_path)
    code = ""
    for file in files:
        f = open(os.path.join(save_path, file), 'r', encoding='utf-8')
        code += f.read()
        f.close()

    pattern = "SC_.+;"
    msg_codes = re.findall(pattern, content)

    proto_code_to_type = {}
    proto_type_to_code = {}
    for msg_code in msg_codes:
        if not "//" in msg_code:
            matches = re.findall(r'\w+', msg_code)  # 匹配所有单词字符
            matches[0] = matches[0].replace("_code", "")
            if matches[0] in code:
                proto_code_to_type.update({matches[1]: matches[0]})
                proto_type_to_code.update({matches[0]: matches[1]})

    local_path = os.getcwd()
    # 创建Jinja2环境，指定模板文件所在目录
    env = Environment(loader=FileSystemLoader(local_path))
    # 加载模板
    template = env.get_template('C#ProtobufCodeTemplate.jinja2')
    csharp_code = template.render(proto_code_to_type=proto_code_to_type,proto_type_to_code=proto_type_to_code)

    with open(os.path.join(save_path, "ProtobufCodeInfo.cs"), 'w', encoding='utf-8') as file:
        # 写入字符串到文件
        file.write(csharp_code)

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-sp", "--save_path", help="svn folder path")
    parser.add_argument("-pcp", "--protobuf_code_path", help="svn folder path")
    args = parser.parse_args()

    main(args.protobuf_code_path, args.save_path)