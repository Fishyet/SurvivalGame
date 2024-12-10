﻿# 游戏策划案

<tag id="目录"></tag>
## 目录

* [项目概述](#1项目概述)
* [背景设定](#2背景设定)
* [核心玩法](#3核心玩法)
  * [地图系统](#31地图系统)
    * [环境参数](#311环境参数)
    * [地图区域](#312地图区域)
    * [昼夜系统](#313昼夜系统)
    * [天气系统](#314天气系统)
      * [天气行为](#3141天气行为)
      * [季节](#3142季节)
    * [奖励](#315奖励)
    * [战争迷雾效果](#316战争迷雾效果)
    * [预先设计的建筑/地形模板](#317预先设计的建筑地形模板)
  * [基建系统](#32基建系统)
    * [主基地](#321主基地)
      * [主基地的选址与搬迁](#3211主基地的选址与搬迁)
      * [主基地遭遇的袭击](#3212主基地遭遇的袭击)
      * [主基地中允许基建的内容](#3213主基地中允许基建的内容)
    * [副基建区域](#322副基建区域)
    * [物品合成](#323物品合成)
    * [摧毁与重建](#324摧毁与重建)
  * [玩家系统](#33玩家系统)
    * [玩家属性](#331玩家属性)
    * [天赋树](#332天赋树)
    * [背包](#333背包)
  * [怪物系统](#34怪物系统)
    * [NPC](#341NPC)
    * [敌对生物](#342敌对生物)
    * [非敌对生物](#343非敌对生物)
  * [物品系统](#35物品系统)
  * [战斗系统](#36战斗系统)
  * [成就系统](#37成就系统)
* [本月目标](#4本月目标)
* [美术风格](#5美术风格)

---

<tag id="1项目概述"></tag> 
## 1. 项目概述

[返回目录](#目录)

项目名称：我也不知道（

游戏类型：生存/探索/角色扮演。

目标受众：喜欢生存类、开放世界游戏的玩家，年龄范围为10-45岁。

核心理念：在资源有限的末世进行无限的探索与创造。

<tag id="2背景设定"></tag> 
## 2. 背景设定

[返回目录](#目录)


许多年后的末世，玩家是极少数的幸存者，这个世界上肆虐着怪物与各种极端天气，玩家需要收集十分有限的资源、建立能够保护自己的庇护所、探索这片孤独的大地，并竭力生存下去。

<tag id="3核心玩法"></tag> 
## 3. 核心玩法

[返回目录](#目录)


<tag id="31地图系统"></tag> 
### 3.1 地图系统

[返回目录](#目录)

游戏地图将**分为不同的区域**，总体而言是**有限**的。其中，预先设计一些废墟/遗迹/地形模板，在地图区域上的出现方式与排列顺序随机。**玩家可以自由探索每一个区域，尽管越级挑战可能有些困难，但是回报与风险并重。**

<tag id="311环境参数"></tag> 
#### 3.1.1 环境参数

地图空旷程度、加入建筑模板的种类与数量、含有资源数量多少、稀有资源比例、发生恶劣天气的概率、野生动物与怪物的数量、气温、辐射区域多少、NPC分布情况等，这些参数与地图的难度大致挂钩。

<tag id="312地图区域"></tag> 
#### 3.1.2 地图区域

每个区域具有独特的环境特征、难度、资源、怪物、辐射程度，不同区域拼接在一起，组成了大地图。在可供玩家查看的地图上，不同区域需依照其难度等级进行对应的标注，并在玩家进入对应区域时进行提示。
区域暂定如下：

| 区域     | 资源                             | 稀有资源比例 | 发生天气行为的概率 | 辐射区域 | 怪物  | 气温         | 野生动物 | NPC分布 | 总体难度 |
|----------|-----------------------------------|------------|-------------------|----------|-------|------------|----------|--------|----------|
| 森林     | 树木、石头、药草丛、大量食物       | 少         | 低                | 少       | 弱    | 适宜       | 多       | 多       | 1        |
| 沙漠     | 沙子、石头、仙人掌、少量食物       | 少         | 中                | 少       | 中    | 炎热/寒冷（昼夜温差大） | 少       | 中       | 2        |
| 沼泽     | 树木、矿石、少量食物、药草丛       | 中         | 中                | 中       | 中    | 适宜       | 中       | 中       | 3        |
| 苔原     | 冰、树木、矿石、少量食物、药草丛   | 多         | 高                | 多       | 强    | 寒冷       | 少       | 少       | 5        |
| 城市废墟 | 建材、武器、防具、医疗物品、大量食物 | 多         | 高                | 多       | 强    | 适宜       | 少       | 少       | 4        |
| 海洋     | 大量食物、珊瑚、海带、矿石、药草丛 | 中         | 中                | 少       | 弱    | 适宜       | 多       | 中       | 2        |
| 矿山     | 石头、矿石、极少量食物           | 多         | 高                | 多       | 强    | 偏冷       | 少       | 中       | 5        |

<tag id="313昼夜系统"></tag> 
#### 3.1.3 昼夜系统

- 夜间温度降低；
- 怪物的出现率与攻击性增加；
- 光线大幅度变暗；
- 玩家可以在床旁边睡觉。

<tag id="314天气系统"></tag> 
#### 3.1.4 天气系统

**天气系统是对于整个世界而言的，即某个季节中，地图的每个区域都将是这个季节。四个季节是依次变化的，根据当前季节，对应的天气行为会依照概率来发生。**

**我们将天气系统拆分为两个板块，即“天气行为”与“季节”。**


<tag id="3141天气行为"></tag> 
##### 3.1.4.1 天气行为

根据当前地图区域发生天气行为的概率，每半个游戏日进行一次概率的判定。先判断是否发生天气行为；若判定结果为需要发生天气行为，则再依照当前季节进行选择新的天气行为，亦或是有2/3的概率维持上一个天气行为（避免变化太频繁）。

**当玩家离开一个区域时，该区域需要在一个游戏日以内保持“记住”当前天气**，一个游戏日之后再清除，以确保玩家反复横跳的时候不会造成天气反复刷新，也不会造成某个天气一直存在在这个已经离开了的区域。

| 天气行为 | 影响描述                                                         |
|----------|------------------------------------------------------------------|
| 沙暴     | - 阻碍玩家视线；<br>- 使玩家暴露在户外时缓慢失去生命值；<br>- 减慢玩家移动速度；<br>- 在没有大棚的情况下对农作物造成严重损伤；<br>- 怪物预警范围大幅减小。 |
| 降雨     | - 出现雷击，会损失建筑耐久，且被雷击中的木质家具可能会着火；<br>- 可以收集特殊的材料（如蘑菇、藤蔓）；<br>- 篝火燃料加速消耗；<br>- 温度降低，玩家更易失温。 |
| 降雪     | - 在没有大棚的情况下对农作物造成严重损伤；<br>- 可以收集特殊的材料（如某些原矿）；<br>- 单个资源可获取量减少，比如砍伐树木将获得较平时更少的木材；<br>- 减慢玩家移动速度。 |
| 气旋     | - 可以制作“预警器”来提前得知气旋的形成及位置；<br>- 气旋会摧毁野外资源；<br>- 气旋会对玩家的行动造成很大的阻碍；<br>- 气旋在移动一段时间后逐渐消散；<br>- 气旋经过的区域会杀死一部分生物，玩家可以拾取其掉落物。 |

**注：气旋可以与另外三种天气行为之一同时发生**。

<tag id="3142季节"></tag> 
##### 3.1.4.2 季节

季节每隔一定游戏日进行一次改变。

| 季节       | 描述                                                         | 降雨概率 | 降雪概率 | 沙暴概率 | 气旋概率 |
|------------|--------------------------------------------------------------|------|----|----|----|
| 风和日丽   | 一切正常，玩家可在该季节抓紧时机发育与探索。                   | 小 | 极小 | 无 | 无 |
| 旱季       | 水资源大幅减少；部分户外木质家具变得易燃；气温会升高，玩家不易失温。 | 无 | 小 | 大 | 中 |
| 雨季       | 水资源以及降雨行为大幅增加。                                   | 高 | 无 | 无 | 中 |
| 雪季       | 气温大幅降低；部分无法通行的湍急的水域将会结冰，变为可以通行。       | 无 | 高 | 小 | 低 |

<tag id="315奖励"></tag> 
#### 3.1.5 奖励

**基于地图的奖励**主要有几种方式：

1. 探索到了地图的某个难找的/重要的区域。

2. 地图的探索度达到某个阈值。

3. 免费的普通补给箱

4. 需要花费某些游戏道具破解（如电力/撬棍/战斗）才能获得的高级补给，注意，**补给的战利品丰盛程度必须与需要花费的游戏道具成正相关**。

<tag id="316战争迷雾效果"></tag> 
#### 3.1.6 战争迷雾效果

在供玩家查看的地图上是存在战争迷雾的。对于未探索的区域，地图上只会显示一个名称或大概的轮廓，玩家探索到周围后才会显示完整的地图。

<tag id="317预先设计的建筑地形模板"></tag> 
#### 3.1.7 预先设计的建筑/地形模板

- 在不同地图区域的生成率

- 包含一些小剧情

- 某些建筑的大门需要钥匙/特定时间/特定季节才能打开

<tag id="32基建系统"></tag> 
### 3.2 基建系统

[返回目录](#目录)

<tag id="321主基地"></tag> 
#### 3.2.1 主基地

玩家可基建区域分为主基地、副基建区域。

<tag id="3211主基地的选址与搬迁"></tag> 
##### 3.2.1.1 主基地的选址与搬迁

主基地的位置是可以由玩家自行选择的，而且可以选在地图的任何一个区域，也就是**玩家可以选择在任何一个地图区域开局，** 像幻兽帕鲁那样可以自选最开始的出生区域（但同时玩家需要承担那个地图区域难度的风险）。

主基地存在于一个类似于“核心”的东西，核心放置地点的周围即视为主基地，核心完好时周围刷怪率降低，核心损坏后可以使用资源修复。**主基地的核心存在并完好时，玩家死亡后会掉落全部装备并在主基地复活；否则玩家死亡将直接删档。**

选择主基地位置后如果需要进行更改，需要将核心搬过去，但是**在搬运核心的过程中是视为核心已经被摧毁的，**也就是增大风险，防止玩家四海为家**。**

<tag id="3212主基地遭遇的袭击"></tag> 
##### 3.2.1.2 主基地遭遇的袭击

- 事件袭击：如果玩家攻击了某些NPC，或者在探索地图的过程中触发了某些事件，将激活一个倒计时，在一定游戏日后触发针对“核心”的袭击事件，玩家可以选择通过战斗来保护“核心”，也可以选择在袭击事件开始之前扛着“核心”跑路，这样就不会触发袭击事件（倒计时结束时“核心”与玩家都处于另一个地图区域即可让袭击事件立即结束），但跑路的代价是，在倒计时结束时，原有的主基地将被夷为废墟。

- 怪物游荡袭击：如果主基地附近有怪物游荡，他们会攻击主基地。以此同样来避免玩家四海为家（扛着核心到处走，放在哪里就把哪里当作主基地，然后一点防护措施都不做的那种）。

<tag id="3213主基地中允许基建的内容"></tag> 
##### 3.2.1.3 主基地中允许基建的内容

- 初级工作站
- 高级工作站
- **特殊工作站**（注：比如生产某些贵重物品的工作站）
- 墙（注：墙、地板、门等基建内容是根据使用材料分为木质、石质等材质的，下面不再赘述）
- 地板（注：大部分基建内容需要建造在地板上，地板被摧毁时上面的物品也被摧毁，包括高级收纳容器）
- 床（注：睡觉可以加速时间流逝，恢复生命值，降低水和饱食度）
- 窗
- 门
- 桌椅（注：在桌子上吃饭的食物效果有额外15%的加成）
- 马桶（注：答辩可以获得农家肥）
- 普通收纳容器（注：被摧毁时，里面的物品会丢失一半，剩余的掉落在地面上）
- **高级收纳容器**（注：可以用于存放担心丢失的珍贵物品，不会被摧毁，除非玩家手动拆除或者下面的地板被摧毁。所以不建议把它建在地板上）
- 农田（注：农田和大棚在气候更好的地方可以提高产量，使用农家肥等措施也可以提高产量）
- **大棚**（注：可以抵御极端天气，同时可以种植更多种类的食物/药材）
- 防御设施
- 光源
- 火源
- 区域特色建筑（注：有些开采站只能在相应地区建造，或在不同区域产出不同物品）

<tag id="322副基建区域"></tag> 
#### 3.2.2 副基建区域

副基建区域只是作为临时庇护所存在，几乎任何地方都可以作为副基建区域，满足玩家的探索和基建欲望。副基建区域中允许基建的内容：

- 初级工作站

- 高级工作站

- 墙

- 地板

- 床

- 窗

- 门

- 普通收纳容器

- 农田

- 防御设施

- 光源

- 火源

- 区域特色建筑

<tag id="323物品合成"></tag> 
#### 3.2.3 物品合成

玩家可以与工作站交互，并进行物品合成。工作站分为建材制造站、半成品制造站、装备制造站、食物制造站、药物制造站、拆解工作站。

| 工作站类型 | 可制造物品 | 所需材料 |
|------------|-----------|------------|
| 建材制造站 | 木墙       | 木头 x5    |
|            | 木窗       | 木头 x5    |
|            | 木门       | 木头 x5    |
|            | 石墙       | 石头 x3    |
|            | 石窗       | 石头 x3    |
|            | 石门       | 石头 x3    |
|            | 床         | 木头 x5, 羊毛 x5, 布 x3 |
| 半成品制造站 | 布         | 植物纤维 x2 |
|            | 粗铁       | 铁矿 x2, 植物纤维 x1 |
|            | 螺栓       | 粗铁 x2    |
| 装备制造站 | 低级枪     | 螺栓 x4, 木头 x2, 布 x1 |
|            | 木斧       | 木头 x5    |
|            | 石斧       | 木头 x2, 石头 x2 |
|            | 保暖衬衣   | 布 x4, 羊毛 x5 |
|            | 皮革甲     | 布 x6      |
|            | 子弹 x10   | 硝石 x1, 粗铁 x1 |
| 食物制造站 | 鸡汤来喽   | 生鸡肉 x2, 蘑菇 x2 |
|            | 好果汁     | 浆果 x5    |
|            | 烤羊肉     | 生羊肉 x2  |
| 药物制造站 | 绷带       | 植物纤维 x2 |
|            | 医疗箱     | （一大堆复杂的东西） |
| 拆解工作站 | 拆解装备/建材/半成品 | 根据剩余耐久度返还原材料（不超过75%，且向下取整） |

<tag id="324摧毁与重建"></tag> 
#### 3.2.4 摧毁与重建

基建内容都是可以被玩家手动拆除的，绝大部分基建内容是可以被敌人摧毁的。如果是玩家手动拆除，则该建材直接变为可以拾取的掉落物。

<tag id="33玩家系统"></tag> 
### 3.3 玩家系统

[返回目录](#目录)

<tag id="331玩家属性"></tag> 
#### 3.3.1 玩家属性

| 属性   | 描述                                                         |
|--------|--------------------------------------------------------------|
| 生命值 |                                                              |
| 水     | 水值会随时间/玩家行为而逐渐降低，很低之后会逐渐扣血，并降低15%开采资源效率、玩家视野变小。 |
| 饱食度 | 饱食度会随时间/玩家行为而逐渐降低，很低之后会逐渐扣血，并降低15%开采资源效率、移动速度。高饱食度时，玩家移动速度与资源开采效率增加15%。 |
| 感染程度 | 被某些敌人攻击后，或者没穿防护服的情况下在感染区域滞留，都会发生感染，发生感染后必须及时进行处理（用清水冲洗或注射抑制剂），否则玩家的感染程度将持续加重。感染到达某个阈值之后，会依次触发以下debuff：生命上限与防御力减少35%、无法睡眠、附近的NPC会主动攻击玩家。同时也可以考虑触发一些正面buff，比如减少环境因素对玩家的影响，减少水值的流逝等等。感染条满时，玩家将逐渐扣血。 |
| 体温   | 地图区域、季节、天气行为、昼夜、火源都会影响气温。玩家可以通过穿戴保暖衬衣、吃辣椒、吃冰块等方式来抵御低温或高温；否则玩家的体温将发生变化。体温过低/过高：持续扣血、开采资源效率与移动速度减少30%。体温偏低/偏高：开采资源效率与移动速度减少10%。舒适：开采资源效率增加10%。 |
| buff/debuff | Buff：体温舒适时、高饱食度时、食用了某些食物或药剂时，会获得对应的buff。Debuff：体温不舒适时、低水值时、低饱食度时、感染时、天气与季节异常时、被某些怪物攻击时、背包超重时等，会获得对应的debuff。 |

<tag id="332天赋树"></tag> 
#### 3.3.2 天赋树

玩家加点的天赋树具有等级的显示，即显示在对应方向投入的天赋点的多少。天赋点的获得通过对应方向行为的积累，比如砍多少颗树之后可以获得生存方向的天赋点，打多少怪物之后就可以获得战斗方向的天赋点。

| 方向   | 描述                                                         |
|--------|--------------------------------------------------------------|
| 生存方向 | 如增加树木砍伐效率、增加副资源获取概率、增加挖矿效率、减少恶劣天气的影响、农场产量提高、增加负重等。 |
| 战斗方向 | 如枪械弹容量扩大、装备耐久度提升、玩家生命值提升、防御力提升、攻击力提升、增加怪物掉落物的概率等。 |
| 制造方向 | 如解锁各个工作站、减少耗材需要量、减少修复建筑需要的耗材、解锁高级配方（高级容纳箱、药物、撬棍、避雷针等）。 |
| 社交方向 | 如NPC商品降价、解锁更多NPC、NPC出售更多商品等。 |

<tag id="333背包"></tag> 
#### 3.3.3 背包

- 容量有限、负重有限，超重有debuff
- 拾取后的物品加入背包
- 可以在背包中查看物品的详细说明（图标、名称、数量、稀有度、描述、丢弃等）
- 可以在背包中切换当前装备
- 可以在背包中使用物品（药物、食物）
- 玩家手上什么都可以拿，拿了基建物品就是可以放置，拿了食物就是可以吃，对应物品的特有属性

<tag id="34怪物系统"></tag> 
### 3.4 怪物系统

[返回目录](#目录)

<tag id="341NPC"></tag> 
#### 3.4.1 NPC

- **属性**：
  - 职业：售卖物品种类
  - 溢价比例：即这个NPC是良民还是刁民
  - 出现地区
  - 名字
  - 刷新率
  - 生命值
  - 攻击力
  - 攻击方式
  - 防御力
  - 移动速度
  - 掉落物
  - 预警范围

NPC是可以被攻击的对象。NPC被玩家或怪物或环境因素杀死后，玩家可以获得NPC掉落的某些东西，并且NPC在一段时间后刷新。如果玩家杀死了NPC（攻击而未杀死则不计数，仅参考最后一击），这会导致几个游戏日之内该地区的NPC商品售卖价格大幅提高，并且会发生袭击事件。某些NPC卖的东西很便宜，所以玩家要尽可能保护这些NPC。

<tag id="342敌对生物"></tag> 
#### 3.4.2 敌对生物

- **属性**：
  - 出现地区与对应的刷新率
  - 名字
  - 生命值
  - 攻击力
  - 攻击方式与距离
  - 防御力
  - 移动速度
  - 掉落物
  - 预警范围

怪物每隔一定时间进行一次刷新的判定（基于光照等环境因素，且不能生成在距离玩家10格以内）。

- 丧尸（仅举例）
  - 出现地区：森林-中；沙漠-低；沼泽-中；苔原-低；城市废墟-高；矿场-高。
  - 名字：丧尸
  - 生命值：20*地区难度
  - 攻击力：3*地区难度
  - 攻击方式与距离：咬你-0.5格
  - 防御力：1*地区难度
  - 移动速度：（0.95+0.05*地区难度）格/秒
  - 掉落物：100%钱*3-5，70%腐肉*1
  - 预警范围：r=6格

注：战斗方式暂时采用回合制，因此这里的移动速度为靠近玩家的速度；预警范围为寻路后的距离；怪物的攻击距离代表“遭遇”的距离，遭遇后即进入战斗。本例中，玩家与怪物寻路之后的距离小于0.5格时即进入战斗。

<tag id="343非敌对生物"></tag> 
#### 3.4.3 非敌对生物

- **属性**：
  - 出现地区
  - 名字
  - 刷新率
  - 生命值
  - 攻击力（某些非敌对生物具有）
  - 攻击方式（某些非敌对生物具有）
  - 防御力
  - 移动速度
  - 掉落物

<tag id="35物品系统"></tag> 
### 3.5 物品系统

## 物品系统

物品具有以下属性：

| 属性       | 描述                                                         |
|------------|--------------------------------------------------------------|
| 唯一的ID    | 物品的唯一标识符                                               |
| 名字       | 物品的名称                                                     |
| 价值       | 是否可出售                                                   |
| 品质       | 普通、珍贵、超级无敌宇宙爆炸至尊珍贵                             |
| 最大可堆叠数量 | 物品的最大堆叠数量                                           |
| 重量       | 物品的重量                                                     |
| 描述       | 物品的详细描述                                                 |
| 特有属性   | 例如回复类物品可以被使用，装备类物品具有属性加成等                 |

### 回复类物品示例

| ID       | 名字     | 价值  | 品质       | 最大可堆叠数量 | 重量  | 描述                             | 特有属性                         |
|----------|----------|-------|------------|--------------|-------|----------------------------------|--------------------------------|
| 114514   | 生羊肉   | 1块钱 | 普通       | 99          | 1kg   | 看起来血淋淋的，不过你实在太饿了 | 使用后恢复15饱食度               |
| 114515   | 熟羊肉   | 3块钱 | 珍贵       | 99          | 1kg   | 妈妈的味道                        | 使用后恢复40饱食度并获得buff“增加少许夜视能力” |
| 114516   | 绷带     | 5块钱 | 普通       | 99          | 0.15kg| 很简陋，但是足以让你的伤口不再流血 | 使用后恢复10生命值               |
| 114517   | 矿泉水   | 8块钱 | 普通       | 99          | 0.5kg | 这个世界很少还有干净的矿泉水了   | 使用后恢复50水值                 |

### 其他物品类别

- **装备**：待具体化
- **自然资源**：待具体化
- **怪物掉落资源**：
  - **半成品**：待具体化
- **建材**：待具体化
- **其它**：包括彩蛋物品、任务物品、剧情物品等等

<tag id="36战斗系统"></tag> 
### 3.6 战斗系统

战斗方式：即时战斗

<tag id="37成就系统"></tag> 
### 3.7 成就系统

比如第一次建造一堵墙、无伤boss或者使用绷带都可以获得成就。

- 在查看玩家信息的界面允许查看已完成的和未完成的成就。
达成成就有特殊提示与奖励

<tag id="4本月目标"></tag> 
## 4. 本月目标

[返回目录](#目录)

### 初始地图创建
1. 生成一张基础地图（森林）。
2. 加入资源，如树木、石头、草药丛、补给箱等。
3. 设置不能通过的区域，如栅栏、墙体等。

### 玩家基本操作
1. 玩家能在地图中移动。
2. 玩家能与地图资源交互。
3. 玩家有背包，并实现拾取、丢弃功能。
4. 玩家具有生命值，饱食度。
5. 玩家饱食度随时间流逝而缓慢减少，低饱食度时缓慢扣血。
6. 血量归零时玩家死亡。
7. 实现玩家视角下的可见范围（120°）。

### 基础工作站
1. 基础工作站的加入。
2. 玩家可以与基础工作站交互，实现合成背包中的物品。
3. 基础工作站可以被摧毁。

### 基础非敌对生物
1. 会随机生成在玩家附近。
2. 为玩家添加“攻击”功能。
3. 死亡后会掉落可以被拾取的物品（如肉类）。
4. 为玩家添加“进食”功能，

<tag id="5美术风格"></tag> 
## 5. 美术风格

[返回目录](#目录)

2.5D视角，风格统一，偏平静一些，具体风格待讨论后决定。
