   M S B A M L   `   `   `  ���� B  >VeCCtor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:VeCCtorVeCCtor      VeCCtor.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     #yclr-namespace:VeCCtor     -     � Title$  VeCCtor��$	��600��$	��800�� � Icon$ icon.png��.��� ���#dddddd.+��� $	��Top=�$��90��$��	0,0,0,471q�.+�o� $��0q�$��21�� �� Left$ 72��$��10000��o� $��0q�$��21��$��72��.@�m� $��0q�$
��File��$��21��$��43��.@�m� -   $��0q�$	��New�� m� Icon �� $��20��$��20��$��Icons/new.ico��m� -   $��0q�$
��Save�� �� $��20��$��20��$��Icons/save.ico��m� -   $��0q�$
��Open�� �� $��20��$��20��$��Icons/open.ico��o� $��0q�$��21��$ 50��$��57��.@�m� $��0q�$
��Edit��$��47��$��21��.@�m� $��0q�$	��Add��$	��Top=�$
��Left=�$��21��.@�m� -   $��0q�$��	Rectangle��m� -   $��0q�$��Ellipse��m� -   $��0q�$
��Line��m� -   $��CopyMenu��$��0q�$
��Copy��$	��Top=�$
��Left=�$��21��m� -	   $��	PasteMenu��$��0q�$��Paste��$	��Top=�$
��Left=�$��21��m� -
   $��0q�$��Delete��$	��Top=�$
��Left=�$��21��m� -   $��0q�$��Clear��$	��Top=�$
��Left=�$��21��o� $��0q�$��21��$	 100��$��57��.@�m� $��0q�$
��View��$��47��$��21��.@�m� -   $��	GridOnOff��$��0q�$��Off Grid��o� $��0q�$��21��$	 150��$��57��.@�m� -   $��0q�$��About��$��47��$��21��� .+��� $��1q�
���   �$��243,27,15,14q�$	��500��$	��500��._��� -   $��	WorkField��$��Stretch=�$��Stretch=�
�������$	 278��
 �� Top$ 41��.+��� -   $
��Grid��$	��498��$	��498��$��grid.png���� -   $��
LabelClick��$�� ��$��Center=�$
��Left=�$��
10,138,0,0q�$	��Top=�$��25(��� -   $��PropertiesField��$��-6,35,542,268q�.+�   VeCCtor.NumberTextBox  -     Name$
 colA��   VeCCtor.NumberTextBox   TextChanged$ TextChanged��$��23��$��40��$��200,210,538,328q�$ -6��$	 -40��  -   $
 tbX1��$ TextChanged��   KeyDown$ KeyDown��   KeyUp$ KeyDown��$��23��$
��Left=�$��54,0,0,0q�$	��Top=�$	��129��  -   $
 tbX2��$ TextChanged��$ KeyDown��$ KeyDown��$��23��$
��Left=�$��
54,116,0,0q�$	��Top=�$	��129��$ 0��$	 -87��  -   $
 tbY1��$ TextChanged��$ KeyDown��$ KeyDown��$��23��$
��Left=�$��
54,145,0,0q�$	��Top=�$	��129��$ 0��$	 -87��  -   $
 tbY2��$ TextChanged��$ KeyDown��$ KeyDown��$��23��$
��Left=�$��
54,174,0,0q�$	��Top=�$	��129��$ 0��$	 -84��  -   $ tbThinkness��$ TextChanged��$ KeyDown��$ KeyDown��$ 54��$	 117��$��23��$	��129���� -   $��label1��$	��x1:��$��28��$��Right=�$
��Left=�$��6,85,0,0q�$	��Top=�$��40��$ 0��$	 -87���� -   $��label2��$	��x2:��$��28��$��Right=�$
��Left=�$��	6,114,0,0q�$	��Top=�$��40��$ 0��$	 -87���� -   $��label3��$	��y1:��$��28��$��Right=�$
��Left=�$��	6,143,0,0q�$	��Top=�$ 0��$	 -87��$��40���� -   $��label4��$	��y2:��$��28��$��Right=�$
��Left=�$��	6,172,0,0q�$	��Top=�$ -4��$	 -84��$��44���� -   $��label5��$ 2��$	 115��$��thin.:��$��28��$��Right=�$��44��  VeCCtor.ByteTextBox  -   	  Name$
	 colR��   VeCCtor.ByteTextBox
   TextChanged$
 TextChanged��   KeyDown$ KeyDown��   KeyUp$ KeyDown��$ 54��$	 170��$��23��$��40���� -   $��label6��$ 54��$	 146��$��r:��$��28��$��Center=�$��40���� -   $��label7��$	 102��$	 146��$��g:��$��28��$��Center=�$��40���� -    $��label8��$	 148��$	 146��$��b:��$��28��$��Center=�$��40���� -!   $��label9��$	 194��$	 146��$��a:��$��28��$��Center=�$��40���� -"   $��label10��$ 2��$	 168��$��line:��$��28��$��Right=�$��44���� -#   $��label11��$ 2��$	 197��$��fill:��$��28��$��Right=�$��44��  -$   $
	 colG��$
 TextChanged��$ KeyDown��$ KeyDown��$	 102��$	 170��$��23��$��40��  -%   $
	 colB��$
 TextChanged��$ KeyDown��$ KeyDown��$	 148��$	 170��$��23��$��40��  -&   $	 fillA��$
 TextChanged��$ KeyDown��$ KeyDown��$��23��$��40��$	 194��$	 199��  -'   $	 fillR��$
 TextChanged��$ KeyDown��$ KeyDown��$��23��$��40��$ 54��$	 199��  -(   $	 fillG��$
 TextChanged��$ KeyDown��$ KeyDown��$��23��$��40��$	 102��$	 199��  -)   $	 fillB��$
 TextChanged��$ KeyDown��$ KeyDown��$��23��$��40��$	 148��$	 199���� -*   $��Delete��$ 87��$	 236��$��75��