   M S B A M L   `   `   `  ????J  FCP_ComputerClub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CP_ComputerClubCP_ComputerClub  5   	        CP_ComputerClub.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CP_ComputerClub     5   	   -   5          ? Title$  
MainWindow??5   	   $	??600??6   $	??800??6)   .??5	      ?  ? RowDefinitions 5
   
   ?? 5      $
??0.7* ?6   6,   ?? 5      6   ?? 5      $	??10* ?6   6+   ?? 5      $??3* ?6   6*   .+?5   
   ?? -   5         ??Name$ listView_Computers??5      $??2??5      
???   ? ?? View 5      ?? 5      .P?5      ??  ?? Width$ 30??6N    ?? DisplayMemberBinding 6%   ??  ?? Path$ Id?.??6Y   №6\   ?? 5      $	 100??6R    6%   ?? $ TypePC?.??6^   Type6d   ?? 5      $	 120??6R    6%   ?? $ IsBusy?.??6^   	Busy/Free6i   ?? 5      $	 120??6T    6%   ?? $ IsBroken?.??6`   Broken/Ready6n   ?? 5      $	 120??6U    6%   ?? $ 	TimeStart?.??6a   Start6h   ?? 5      $	 120??6S    6%   ?? $ TimeEnd?.??6_   End6d   5      5       ]? 5!   
   $??False??6   $??1??6*   .+?5"      ?? -   5         ??Name$ 	btn_start??5"      $
??Left=?6(   $??5q?6>   .??6a   
Reserve PC6m   ?? 5#      -   5        $ btn_stop??5#      $??Right=?6'   $??5q?6>   .??6`   
Release PC6l   5$      ]? 5%   
   $??3??6   $
??True??6!   .+?5&      ?? $	??Top=?6   .??6)   
History:63   ?? 5'      -   5         ??Name$ txtBox_history??5'      $
??Left=?5(      $??5,0,5,5q?5)      "	 ?? VerticalScrollBarVisibility$	 Visible=?5*      
 ?? AcceptsReturn$

 True??5+       ?? 
IsReadOnly$
 True??5,      6*   5-      ?? 5.   
   $??Center=?6    ?? Orientation$ 
Horizontal=?62   .+?5/      ?? -   5        $
??Left=?5/      $??3q?6-   .??6V   Monthly analysis6h   ?? 50      -   5        $??Right=?50      $??3q?6-   .??6Z   ,*Min. or Max. quantity for a certain period6?   51      52      54      