   M S B A M L   `   `   `  ����Q  Mprb-session2-first-try, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:prb_session2_first_tryprb_session2_first_try  5   	   /    )prb_session2_first_try.Windows.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:prb_session2_first_try     5   	   -   5          � Title$  Сессия 2��5   	   $	��800��6   $
��1200��6'    � 
ResizeMode$ NoResize=�64    � Icon$ ../Assetts/Logo.png��5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   � $��0��6   $��0��6   $��2��6,      mainBackground#��[  6@    5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $��0��6   $��/Assetts/Logo.png��6$   $	��125��6?   $	��125��6K   �� 5      $��1��6   $��15q�6%   $
��Left=�61   $��Center=�6L   $��40��6g   $	��600��6s   
������� �� CornerRadius$ 25��6�   ._�5      �� $7��1Организационная структура��6   $
��Left=�6=   $��Center=�6X   $
��25 0q�6s   $��17(�6�   5      5      � 5    
   $��1��6   $��0��6    5!      �� 5"      $��25 �6    �� 5#      �� 5$      $��25 �6     5&      �� 5'      $��25 �6#   �� 5(      �� 5)      $��25 �6#   .+�5,      �� $��1��6   $��1��6(   
�������$
��Auto=�6O   $
��Auto=�6r   .��5-      �� -   5        $��graphCanvas��5-      $��Center=�6,   $	��Top=�6I   5.      50      � 52   
   $��1��6   $��1��6    53      �� 54      $��25 �6    �� 55      �� 56      $��25 �6     58      �� 59      $��25 �6#   �� 5:      �� 5;      $��25 �6#   .+�5>      �� -   5        $��listWorkers��5>      $��1��6.   $��1��6>   $��Stretch=�6K   $��Stretch=�6i   
���������5?      �� 5@      .R�5A      � -   5        $	��500��5A      #��[  6z   $��15q�6�   
 � Tag 6+   �� *Worker.IdWorker+ 5B      �� 5C   "   $	��500 �63   �� 5D   "   $��* �63    5F      �� 5G   "   �� 5H   "   �� 5I   "   �� 5J   "   .+�5L      � $��0��6#   $��0��60   $��Stretch=�6@    5M   "   �� 5N   &   $	��200 �67   �� 5O   &   $��10 �67   �� 5P   &   $	��290 �67   .+�5R   "   �� $��0��6,   $��12(�6<   $
��Left=�6J   $��Center=�6e   ��6�   �� *Departament.NameDepartament+�� 5S   "   $��1��6,   $��12(�6<   $��Center=�6J   $��Center=�6g   $��-��6�   �� 5T   "   $��2��6,   $��12(�6<   $
��Left=�6J   $��Center=�6e   $
��Wrap=�6�   ��6�   �� *Position.NamePosition+6�   5U      �� 5V      $��0��6(   $��1��68   $��15(�6E   $��Stretch=�6S   $��Center=�6q   $��5q�6�   ��6�   �� *Worker.FullName+�� 5W      $��0��6)   $��2��69    �� Orientation$ 
Horizontal=�6F   $
��Left=�6_   $	��450��6z   .+�5X   "   �� $��15(�6,   $
��Left=�6:   $��Center=�6U   $��5q�6p   ��6{   �� *Worker.WorkPhoneNumber+�� 5Y   "   $��15(�6,   $
��Left=�6:   $��Center=�6U   $��5q�6p   ��6{   �� *Worker.Email+5Z      �� 5[      $��0��6(   $��3��68   $��15(�6E   $
��Left=�6S   $��Center=�6n   ��6�   �� *Cabinet.CabinetNumber+5\      5]      5_      �� 5a      $��1��6   $��1��6%   $��Right=�62   $��Bottom=�6N   #��[  6i   $��50��6�   $��50��6�   $��15q�6�   .��5b      �� $��24(�6   $��+��6*   $��Stretch=�63   $��Stretch=�6O    �� TextAlignment$ Center=�6m   $��40��6�   5c      5e      5g      5h      