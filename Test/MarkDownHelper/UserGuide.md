# GMT Motor Test Kit User Manual

![Gmt Logo](Assets/gmt_logo.png)

Table Of Content
[TOC]
___
<style>
p {
    color: blue;
    padding: 10px;
    font-family: Consolas, monospace;
}
table,
th,
td {
    color: black;
    border: 1px solid black;
    background-color: pink;
    font-family: Consolas, monospace;
}
</style>

## Working/Test Folder

![Test Folder](Assets/test_folder.png)
<p style = "background-color: #f5f5f5; border: 1px solid #ddd;">
➡️APP初次啟動時會在使用者桌面建立一個名為<span style="color: red;">MotorTest</span>的Working Folder(可以在File Menu中修改)<br>
➡️每次測試時都會在Working Folder中建立一個Test Folder如 <span style="color: red;">rolling_mmdd_hhmmss</span>(會用目前的日期時間為流水號為Test Folder命名)<br>
➡️所有測試結果都會存放於此Test Folder
</p>

### **Menu and Tool Bar**

![toolbar](Assets/toolbar.png)

![menu](Assets/menu.png)

### **PWM Control Panel**

:bulb: <span style="color: red;">輸入後須按Tab才會修改修改</span>

![pwm panel](Assets/pwm_panel.png)

### **Power Supply Control Panel**

:bulb: <span style="color: red;">輸入後須按Tab才會修改修改</span>

![power panel](Assets/power_panel.png)

| Name   | Function   |
|------|------|
| VM   | 電壓   |
| Iset | 電流   |
| OVP  | 過壓保護 |
| OCP  | 過流保護 |
___

## ***Rolling Test***

___

### **主要參數**

:bulb: <span style="color: red;">輸入後須按Tab才會修改修改</span>

![pole pair](Assets/polepair.png)<br>
![misc](Assets/misc.png)<br>
![datagrid](Assets/datagrid.png)

|  Name  |          Function          |
| ------ | -------------------------- |
| Insert | 在選取列下方加入資料       |
| Add    | 在最下方加入資料           |
| Repeat | 重複Rolling List的測試設定 |

👉選取列後按Delete鍵可刪除

___

### **RPM Information: RPM圖上的統計值**

![RPM Information](Assets/rpminformation.png)

___

### **測試按鈕**

![test button](Assets/testbutton.png)

___

### Rolling Plot View

![Rolling Plot View](Assets/rolling_plot_view.png)

<ol style = " color: blue;  background-color: #f5f5f5; border: 1px solid #ddd;">
<li>顯示或隱藏轉速/電流曲線</>
<li>顯示/隱藏 Cursor</li>
<li>顯示/隱藏 圖例</li>
<li>在測試時可選擇要不要即時更新 Rolling Plot</li>
<li>按 F1 可儲存目前Cursor座標於C1</li>
<li>按 F2 可儲存目前Cursor座標於C2</li>
<li>計算 C1,C2 差值</li>
</ol>

#### 右鍵選單

![Plot Right Click](Assets/plot_right_click.png)

#### 快速鍵

![Rpm Hotkey](Assets/rpm_hotkey.png)

___

## History

V1.0.0.0 Initial Release
___
Copyright :copyright:2023 [Global Mixed-mode Technology Inc.](http://www.gmt.com.tw/) All rights reserved.
