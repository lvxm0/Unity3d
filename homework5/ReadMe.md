mode 打勾 为 物理引擎趋势<br>
mode 不打勾 为运动学模式<br>

Adapter模式定义:在设计模式中，适配器模式（英语：adapter pattern）有时候也称包装样式或者包装(wrapper)。将一个类的接口转接成用户所期待的。一个适配使得因接口不兼容而不能在一起工作的类工作在一起，做法是将类自己的接口包裹在一个已存在的类中。
<br>
Adapter链接：http://www.runoob.com/design-pattern/adapter-pattern.html
<br>

编写思路：
- 先写接口，再写Adapter类实现接口，来切换两种模式（mode）。
- 实现PFlyAction动作(使用重力，给一个初速度)以及PManager。
- 更改FirstSceneContrtoller，将IAtionManger的添加进去。

遇到的问题：
- 加入重力以后掉的太快了，最后去Project settings 中改小了重力

<br>
视频链接：
http://v.youku.com/v_show/id_XMzU2MTc3NjI3Ng==.html?spm=a2h3j.8428770.3416059.1

