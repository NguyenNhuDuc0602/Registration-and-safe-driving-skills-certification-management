   M S B A M L   `   `   `  ����F  BProject_Prn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Project_PrnProject_Prn  5   	       Project_Prn.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Project_Prn     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#4CAF50 �� Padding$ 10q�63    �� CornerRadius$ 10��6@   $��0,0,0,10q�6R   ._�5      �� $��Login��6   $��24(�6%   $
��Bold"�63   
�������$��Center=�6X   $��Center=�6u   5      �� 5   
   $��1��6   $��Center=�6"   $��Center=�6=   $��10q�6Z   $	��300��6f   .+�5      �� -   5        $��txtEmail��5      $��40��6&   $��10q�62   $��Center=�6>   $��Stretch=�6Y   
�������$��10q�5   (   $
|�True��65   G� 5      -   5        $��txtPassword��5      $��40��6-   $��10q�69   $��Center=�6E   $��Stretch=�6`   $��10q�6~   �� 5      -   5        $��btnLogin��5      $��40��6%   $��Login��61   ���#4CAF50
����������#4CAF50$��1q�6   $��16(�6�   $��Stretch=�6�   $��10q�6�   6�   �� 5"      -   5        $��btnRegister��5"      $��40��6(   $��Register��64   ���#FF9800
����������#FF9800$��1q�6�   $��16(�6�   $��Stretch=�6�   $��10q�6�   �� 5%      -   5        $��btnForgotPassword��5%      $��30��6.   $��Forgot your password?��6:   
������ 
����  �
������ $��14(�6�   $��Center=�6�   $��Center=�6�   $��10q�6�   5&      5'      5(      