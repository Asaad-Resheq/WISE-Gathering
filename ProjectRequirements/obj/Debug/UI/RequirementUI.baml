   M S B A M L   `   `   `  ????  WISE Gathering*    $ProjectRequirements.UI.RequirementUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $	??300??5   
   $	??500??6     ? WindowStartupLocation$  CenterScreen=?5       ? DataContext 5      ??  ?? RelativeSource ?? *Self+ ? 	Resources 5       ?? 
TargetType ?? Property #  TextBoxBaseControlTemplateStyle ?? Value  StandardCombobox2 ?? BasedOn??%?  '??       '?? ?     7  '?? ?     ?? 5   
   " ??6   .?5      ??  ? ??# ?   6)   6q   ?? 5       ? ??$ Center=?6;   6L   ?? 5       ? ??$ Center=?69   6J   ?? 5       ? ??$ 8q?6'   63   5      ?? 5   
   " ??6   8   6a   ?? 5   
   " ??6   .?5      ??  ? ??$ Center=?62   6C   ?? 5       ? ??$ Center=?64   6E   5      5      .??5       ?  6   ?? *requirement+ ? ColumnDefinitions 5!   
   ?? 5"      $??1* ?6   6,   ?? 5#      $??1* ?6   6,   ?? 5$      $??1* ?6   6,   	 ? RowDefinitions	 5&   
   ?? 5'      6   ?? 5(      6   ?? 5)      6   ?? 5*      6   ?? 5+      6   .+?51   
   ?? .??6   Stakeholder53      ?? 55   
   -   5        
 ?? 
IsEditable
 .  ?? DisplayMemberPath$ StickholderName??59   /    ?? SelectedValuePath$ ID??5:   /   $??1??5>      $??2??6   ??57      ?? *stickholders+ ??  ?? AncestorType$ WindowL? ?? SelectedValue 5;      ?? *StickholderID+ ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=? ?? NotifyOnSourceUpdated . 5@      ?? 5B   
   $??1??6   .??6   	Keyword5D      ?? 5E   
   $??1??5F      $??1??5G      $??2??6   
 .  w? TextPath$
 rKey??5J      $
 rKey??5K      $
 rKey??5L       ?? Text 5F      ?? *BaseKeyWord+ ?? TargetNullValue$  ????5I      ?? *requirementKeywords+ ?? $ WindowL?5O      ?? 5Q   
   $??2??6   .??6   Requirement5S      ?? 5T   
    ?? AcceptsReturn .  ?? TextWrapping$
 Wrap=?5U      $??2??5V      $??2??5W      $??1??5X   +   $??2??6;   ??6   ?? *	ReqText+6Q   ?? 5Z   
   -   5        $??8q?5Z        ActionButtonTemplate#???  5[      $??4??5\      $??1??6   .??6B   Save6H   5^      5_      