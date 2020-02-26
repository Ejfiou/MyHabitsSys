$(function () {

    doload();

});
function doload() {
    getPersonallist();
}
function getPersonallist() {
    layui.use('table', function () {
        var table = layui.table
            , form = layui.form;

        table.render({
            elem: '#test'
            , url: '/PersonalList/GetPersonalList'
            , cellMinWidth: 80
            , method: 'post'
            , height: 810
            , page: { //支持传入 laypage 组件的所有参数（某些参数除外，如：jump/elem） - 详见文档
                layout: ['limit', 'count', 'prev', 'page', 'next', 'skip'] //自定义分页布局
                //,curr: 5 //设定初始在第 5 页
                , groups: 5 //只显示 1 个连续页码
                , first: false //不显示首页
                , last: false //不显示尾页
            }
            , cols: [[
                { type: 'numbers' }
                , { field: 'ID', title: 'ID', width: 100, unresize: true, sort: true }
                , {
                    field: 'userImg', title: '头像', width: 150, align: 'center', templet: function (d) {
                        return '<div><img class="userphoto" src="'+ d.userImg +'"></div>';
                    }
                }
                , { field: 'nickName', title: '昵称', Width: 100, }
                , { field: 'userName', title: '用户名', Width: 100,  }
                , { field: 'userEmail', title: '邮箱', Width: 100}
                , {
                    field: 'userSex', title: '性别', width: 85, unresize: true, align: 'center',templet: '#barDemo'}
                , { field: 'userStatus', title: '设为管理员', width: 110, templet: '#switchTpl', unresize: true, align: 'center',sort: true  }
            ]]
           
            //, parseData: function (res) { //将原始数据解析成 table 组件所规定的数据
            //    console.log(res);
            //}
        });

        //监听锁定操作
        form.on('switch(setAdmin)', function (obj) {
            layer.tips(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
            console.log(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
            var userStatus;
            if (obj.elem.checked) {
                userStatus = 1;
            } else {
                userStatus = 0;
            }
            var ID = this.value;
            console.log(userStatus);
            $.post('/PersonalList/UpdateStatus', {
                ID: ID,
                userStatus: userStatus,
            }, function (res) {
                console.log(res);
                if (res.success) {
                    alert("权限更改成功");
                }
            });
        });
    });
}