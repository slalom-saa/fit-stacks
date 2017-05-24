Æ
FC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\CacheUpdatedMessage.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
public 

class 
CacheUpdatedMessage $
:% &
Message' .
{ 
public 
CacheUpdatedMessage "
(" #
IEnumerable# .
<. /
string/ 5
>5 6
keysUpdated7 B
)B C
: 
base 
( 
keysUpdated 
) 
{ 	
} 	
public   
IEnumerable   
<   
string   !
>  ! "
KeysUpdated  # .
=>  / 1
this  2 6
.  6 7
Body  7 ;
as  < >
IEnumerable  ? J
<  J K
string  K Q
>  Q R
;  R S
}!! 
}"" Å	
JC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\ConfigurationExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
public 

static 
class #
ConfigurationExtensions /
{ 
public 
static 
void 
UseLocalCache (
(( )
this) -
Stack. 3
	container4 =
)= >
{ 	
Argument 
. 
NotNull 
( 
	container &
,& '
nameof( .
(. /
	container/ 8
)8 9
)9 :
;: ;
	container 
. 
	Container 
.  
Update  &
(& '
builder' .
=>/ 1
{2 3
builder4 ;
.; <
RegisterModule< J
(J K
newK N
LocalCacheModuleO _
(_ `
)` a
)a b
;b c
}d e
)e f
;f g
} 	
} 
} Ø
BC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\ICacheConnector.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
public 

	interface 
ICacheConnector $
{ 
void 

OnReceived 
( 
Action 
< 
CacheUpdatedMessage 2
>2 3
action4 :
): ;
;; <
Task 
PublishChangesAsync  
(  !
IEnumerable! ,
<, -
string- 3
>3 4
keys5 9
)9 :
;: ;
} 
}   è

@C:\Source\Stacks\Core\src\Slalom.Stacks\Caching\ICacheManager.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Caching

 
{ 
public 

	interface 
ICacheManager "
{ 
Task 
AddAsync 
< 
TItem 
> 
( 
params #
TItem$ )
[) *
]* +
	instances, 5
)5 6
;6 7
Task 

ClearAsync 
( 
) 
; 
Task%% 
<%% 
TItem%% 
>%% 
	FindAsync%% 
<%% 
TItem%% #
>%%# $
(%%$ %
string%%% +
id%%, .
)%%. /
;%%/ 0
Task-- 
RemoveAsync-- 
<-- 
TItem-- 
>-- 
(--  
params--  &
TItem--' ,
[--, -
]--- .
	instances--/ 8
)--8 9
;--9 :
Task55 
UpdateAsync55 
<55 
TItem55 
>55 
(55  
params55  &
TItem55' ,
[55, -
]55- .
	instances55/ 8
)558 9
;559 :
}66 
}77 π
?C:\Source\Stacks\Core\src\Slalom.Stacks\Caching\ItemIdentity.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
internal 
static 
class 
ItemIdentity &
{ 
public 
static 
string 
GetIdentity (
(( )
object) /
instance0 8
)8 9
{ 	
var 
entity 
= 
instance !
as" $
IAggregateRoot% 3
;3 4
if 
( 
entity 
!= 
null 
) 
{ 
return 
entity 
. 
Id  
;  !
} 
var 
result 
= 
instance !
as" $
ISearchResult% 2
;2 3
if 
( 
result 
!= 
null 
) 
{ 
return 
result 
. 
Id  
.  !
ToString! )
() *
)* +
;+ ,
} 
return 
instance 
. 
GetHashCode '
(' (
)( )
.) *
ToString* 2
(2 3
)3 4
;4 5
} 	
} 
} ‰6
DC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\LocalCacheManager.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
public 

class 
LocalCacheManager "
:# $
ICacheManager% 2
{ 
private 
readonly  
ReaderWriterLockSlim -

_cacheLock. 8
=9 :
new; > 
ReaderWriterLockSlim? S
(S T
)T U
;U V
private 
readonly 
List 
< 
object $
>$ %

_instances& 0
=1 2
new3 6
List7 ;
<; <
object< B
>B C
(C D
)D E
;E F
public 
int 
	ItemCount 
=> 

_instances  *
.* +
Count+ 0
;0 1
public&& 
virtual&& 
Task&& 
AddAsync&& $
<&&$ %
TItem&&% *
>&&* +
(&&+ ,
params&&, 2
TItem&&3 8
[&&8 9
]&&9 :
	instances&&; D
)&&D E
{'' 	
Argument(( 
.(( 
NotNull(( 
((( 
	instances(( &
,((& '
nameof((( .
(((. /
	instances((/ 8
)((8 9
)((9 :
;((: ;

_cacheLock** 
.** 
EnterWriteLock** %
(**% &
)**& '
;**' (
try++ 
{,, 
foreach-- 
(-- 
var-- 
instance-- %
in--& (
	instances--) 2
)--2 3
{.. 

_instances// 
.// 
Add// "
(//" #
instance//# +
)//+ ,
;//, -
}00 
}11 
finally22 
{33 

_cacheLock44 
.44 
ExitWriteLock44 (
(44( )
)44) *
;44* +
}55 
return66 
Task66 
.66 

FromResult66 "
(66" #
$num66# $
)66$ %
;66% &
}77 	
public== 
Task== 

ClearAsync== 
(== 
)==  
{>> 	

_instances?? 
.?? 
Clear?? 
(?? 
)?? 
;?? 
returnAA 
TaskAA 
.AA 

FromResultAA "
(AA" #
$numAA# $
)AA$ %
;AA% &
}BB 	
publicJJ 
virtualJJ 
TaskJJ 
<JJ 
TItemJJ !
>JJ! "
	FindAsyncJJ# ,
<JJ, -
TItemJJ- 2
>JJ2 3
(JJ3 4
stringJJ4 :
idJJ; =
)JJ= >
{KK 	

_cacheLockLL 
.LL 
EnterReadLockLL $
(LL$ %
)LL% &
;LL& '
tryMM 
{NN 
returnOO 
TaskOO 
.OO 

FromResultOO &
(OO& '
(OO' (
TItemOO( -
)OO- .

_instancesOO/ 9
.OO9 :
FindOO: >
(OO> ?
eOO? @
=>OOA C
ItemIdentityOOD P
.OOP Q
GetIdentityOOQ \
(OO\ ]
eOO] ^
)OO^ _
==OO` b
idOOc e
)OOe f
)OOf g
;OOg h
}PP 
finallyQQ 
{RR 

_cacheLockSS 
.SS 
ExitReadLockSS '
(SS' (
)SS( )
;SS) *
}TT 
}UU 	
public]] 
virtual]] 
Task]] 
RemoveAsync]] '
<]]' (
TItem]]( -
>]]- .
(]]. /
params]]/ 5
TItem]]6 ;
[]]; <
]]]< =
	instances]]> G
)]]G H
{^^ 	

_cacheLock__ 
.__ 
EnterWriteLock__ %
(__% &
)__& '
;__' (
try`` 
{aa 
varbb 
idsbb 
=bb 
	instancesbb #
.bb# $
Selectbb$ *
(bb* +
ebb+ ,
=>bb- /
ItemIdentitybb0 <
.bb< =
GetIdentitybb= H
(bbH I
ebbI J
)bbJ K
)bbK L
.bbL M
ToListbbM S
(bbS T
)bbT U
;bbU V

_instancescc 
.cc 
	RemoveAllcc $
(cc$ %
ecc% &
=>cc' )
idscc* -
.cc- .
Containscc. 6
(cc6 7
ItemIdentitycc7 C
.ccC D
GetIdentityccD O
(ccO P
eccP Q
)ccQ R
)ccR S
)ccS T
;ccT U
}dd 
finallyee 
{ff 

_cacheLockgg 
.gg 
ExitWriteLockgg (
(gg( )
)gg) *
;gg* +
}hh 
returnii 
Taskii 
.ii 

FromResultii "
(ii" #
$numii# $
)ii$ %
;ii% &
}jj 	
publicrr 
virtualrr 
asyncrr 
Taskrr !
UpdateAsyncrr" -
<rr- .
TItemrr. 3
>rr3 4
(rr4 5
paramsrr5 ;
TItemrr< A
[rrA B
]rrB C
	instancesrrD M
)rrM N
{ss 	
Argumenttt 
.tt 
NotNulltt 
(tt 
	instancestt &
,tt& '
nameoftt( .
(tt. /
	instancestt/ 8
)tt8 9
)tt9 :
;tt: ;
awaitvv 
thisvv 
.vv 
RemoveAsyncvv "
(vv" #
	instancesvv# ,
)vv, -
;vv- .
awaitxx 
thisxx 
.xx 
AddAsyncxx 
(xx  
	instancesxx  )
)xx) *
;xx* +
}yy 	
public
ÄÄ 
virtual
ÄÄ 
Task
ÄÄ 
RemoveAsync
ÄÄ '
(
ÄÄ' (
params
ÄÄ( .
string
ÄÄ/ 5
[
ÄÄ5 6
]
ÄÄ6 7
keys
ÄÄ8 <
)
ÄÄ< =
{
ÅÅ 	

_cacheLock
ÇÇ 
.
ÇÇ 
EnterWriteLock
ÇÇ %
(
ÇÇ% &
)
ÇÇ& '
;
ÇÇ' (
try
ÉÉ 
{
ÑÑ 

_instances
ÖÖ 
.
ÖÖ 
	RemoveAll
ÖÖ $
(
ÖÖ$ %
e
ÖÖ% &
=>
ÖÖ' )
keys
ÖÖ* .
.
ÖÖ. /
Contains
ÖÖ/ 7
(
ÖÖ7 8
ItemIdentity
ÖÖ8 D
.
ÖÖD E
GetIdentity
ÖÖE P
(
ÖÖP Q
e
ÖÖQ R
)
ÖÖR S
)
ÖÖS T
)
ÖÖT U
;
ÖÖU V
}
ÜÜ 
finally
áá 
{
àà 

_cacheLock
ââ 
.
ââ 
ExitWriteLock
ââ (
(
ââ( )
)
ââ) *
;
ââ* +
}
ää 
return
ãã 
Task
ãã 
.
ãã 

FromResult
ãã "
(
ãã" #
$num
ãã# $
)
ãã$ %
;
ãã% &
}
åå 	
}
çç 
}éé Õ
CC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\LocalCacheModule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Caching

 
{ 
public 

class 
LocalCacheModule !
:" #
Module$ *
{ 
	protected 
override 
void 
Load  $
($ %
ContainerBuilder% 5
builder6 =
)= >
{ 	
base 
. 
Load 
( 
builder 
) 
; 
builder 
. 
RegisterType  
<  !
LocalCacheManager! 2
>2 3
(3 4
)4 5
. #
AsImplementedInterfaces +
(+ ,
), -
. 
SingleInstance "
(" #
)# $
;$ %
} 	
}   
}!! Ø
EC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\NullCacheConnector.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Caching 
{ 
public 

class 
NullCacheConnector #
:$ %
ICacheConnector& 5
{ 
public 
void 

OnReceived 
( 
Action %
<% &
CacheUpdatedMessage& 9
>9 :
action; A
)A B
{ 	
} 	
public!! 
Task!! 
PublishChangesAsync!! '
(!!' (
IEnumerable!!( 3
<!!3 4
string!!4 :
>!!: ;
keys!!< @
)!!@ A
{"" 	
return## 
Task## 
.## 

FromResult## "
(##" #
$num### $
)##$ %
;##% &
}$$ 	
}%% 
}&& ⁄
CC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\NullCacheManager.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Caching

 
{ 
public 

class 
NullCacheManager !
:" #
ICacheManager$ 1
{ 
public 
Task 
AddAsync 
< 
TItem "
>" #
(# $
params$ *
TItem+ 0
[0 1
]1 2
	instances3 <
)< =
{ 	
return 
Task 
. 

FromResult "
(" #
$num# $
)$ %
;% &
} 	
public!! 
Task!! 

ClearAsync!! 
(!! 
)!!  
{"" 	
return## 
Task## 
.## 

FromResult## "
(##" #
$num### $
)##$ %
;##% &
}$$ 	
public,, 
Task,, 
<,, 
TItem,, 
>,, 
	FindAsync,, $
<,,$ %
TItem,,% *
>,,* +
(,,+ ,
string,,, 2
id,,3 5
),,5 6
{-- 	
return.. 
Task.. 
... 

FromResult.. "
(.." #
default..# *
(..* +
TItem..+ 0
)..0 1
)..1 2
;..2 3
}// 	
public77 
Task77 
RemoveAsync77 
<77  
TItem77  %
>77% &
(77& '
params77' -
TItem77. 3
[773 4
]774 5
	instances776 ?
)77? @
{88 	
return99 
Task99 
.99 

FromResult99 "
(99" #
$num99# $
)99$ %
;99% &
}:: 	
publicBB 
TaskBB 
UpdateAsyncBB 
<BB  
TItemBB  %
>BB% &
(BB& '
paramsBB' -
TItemBB. 3
[BB3 4
]BB4 5
	instancesBB6 ?
)BB? @
{CC 	
returnDD 
TaskDD 
.DD 

FromResultDD "
(DD" #
$numDD# $
)DD$ %
;DD% &
}EE 	
}FF 
}GG Ï

DC:\Source\Stacks\Core\src\Slalom.Stacks\Caching\NullCachingModule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Caching

 
{ 
internal 
class 
NullCachingModule $
:% &
Module' -
{ 
	protected 
override 
void 
Load  $
($ %
ContainerBuilder% 5
builder6 =
)= >
{ 	
base 
. 
Load 
( 
builder 
) 
; 
builder 
. 
Register 
( 
c 
=> !
new" %
NullCacheManager& 6
(6 7
)7 8
)8 9
. #
AsImplementedInterfaces (
(( )
)) *
.   
SingleInstance   
(    
)    !
;  ! "
builder"" 
."" 
Register"" 
("" 
c"" 
=>"" !
new""" %
NullCacheConnector""& 8
(""8 9
)""9 :
)"": ;
.## #
AsImplementedInterfaces## (
(##( )
)##) *
.$$ 
SingleInstance$$ 
($$  
)$$  !
;$$! "
}%% 	
}&& 
}'' ˘
FC:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\AllProperties.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Configuration %
{ 
public 

class 
AllProperties 
:  
IPropertySelector! 2
{ 
public 
static 
IPropertySelector '
Instance( 0
=>1 3
new4 7
AllProperties8 E
(E F
)F G
;G H
public 
bool 
InjectProperty "
(" #
PropertyInfo# /
propertyInfo0 <
,< =
object> D
instanceE M
)M N
{ 	
return   
propertyInfo   
.    
CanWrite    (
;  ( )
}!! 	
}"" 
}## Ë
DC:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\Application.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Configuration %
{ 
public 

class 
Application 
{ 
public 
Contact 
Contact 
{  
get! $
;$ %
set& )
;) *
}+ ,
public   
string   
Description   !
{  " #
get  $ '
;  ' (
set  ) ,
;  , -
}  . /
public(( 
License(( 
License(( 
{((  
get((! $
;(($ %
set((& )
;(() *
}((+ ,
public00 
string00 
TermsOfService00 $
{00% &
get00' *
;00* +
set00, /
;00/ 0
}001 2
public88 
string88 
Title88 
{88 
get88 !
;88! "
set88# &
;88& '
}88( )
public@@ 
string@@ 
Version@@ 
{@@ 
get@@  #
;@@# $
set@@% (
;@@( )
}@@* +
[HH 	
JsonPropertyHH	 
(HH 
$strHH %
)HH% &
]HH& '
publicII 
stringII 
EnvironmentII !
{II" #
getII$ '
;II' (
setII) ,
;II, -
}II. /
}JJ 
}KK í
JC:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\AutoLoadAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Configuration

 %
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
)* +
]+ ,
public 

sealed 
class 
AutoLoadAttribute )
:* +
	Attribute, 5
{ 
} 
} Ω6
LC:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\ConfigurationModule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Configuration %
{ 
internal 
class 
ConfigurationModule &
:' (
Module) /
{ 
private 
readonly 
Stack 
_stack %
;% &
public   
ConfigurationModule   "
(  " #
Stack  # (
stack  ) .
)  . /
{!! 	
_stack"" 
="" 
stack"" 
;"" 
}## 	
	protected-- 
override-- 
void-- 
Load--  $
(--$ %
ContainerBuilder--% 5
builder--6 =
)--= >
{.. 	
base// 
.// 
Load// 
(// 
builder// 
)// 
;// 
builder11 
.11 
Register11 
(11 
c11 
=>11 !
{22 
var33 
currentDirectory33 +
=33, -
	Directory33. 7
.337 8
GetCurrentDirectory338 K
(33K L
)33L M
;33M N
var44  
configurationBuilder44 /
=440 1
new442 5 
ConfigurationBuilder446 J
(44J K
)44K L
;44L M 
configurationBuilder55 +
.55+ ,
SetBasePath55, 7
(557 8
currentDirectory558 H
)55H I
;55I J 
configurationBuilder66 +
.66+ ,
AddJsonFile66, 7
(667 8
$str668 J
,66J K
true66L P
,66P Q
true66R V
)66V W
;66W X 
configurationBuilder77 +
.77+ ,
AddJsonFile77, 7
(777 8
$str778 E
,77E F
true77G K
,77K L
true77M Q
)77Q R
;77R S
foreach88 
(88  
var88  #
path88$ (
in88) +
	Directory88, 5
.885 6
GetFiles886 >
(88> ?
currentDirectory88? O
,88O P
$str88Q `
)88` a
)88a b
{99  
configurationBuilder:: /
.::/ 0
AddJsonFile::0 ;
(::; <
Path::< @
.::@ A
GetFileName::A L
(::L M
path::M Q
)::Q R
,::R S
true::T X
,::X Y
true::Z ^
)::^ _
;::_ `
};; 
if<< 
(<< 
	Directory<< $
.<<$ %
Exists<<% +
(<<+ ,
Path<<, 0
.<<0 1
Combine<<1 8
(<<8 9
currentDirectory<<9 I
,<<I J
$str<<K S
)<<S T
)<<T U
)<<U V
{== 
foreach>> "
(>># $
var>>$ '
path>>( ,
in>>- /
	Directory>>0 9
.>>9 :
GetFiles>>: B
(>>B C
currentDirectory>>C S
,>>S T
$str>>U l
)>>l m
)>>m n
{??  
configurationBuilder@@ 3
.@@3 4
AddJsonFile@@4 ?
(@@? @
$str@@@ J
+@@K L
Path@@M Q
.@@Q R
GetFileName@@R ]
(@@] ^
path@@^ b
)@@b c
,@@c d
true@@e i
,@@i j
true@@k o
)@@o p
;@@p q
}AA 
}BB  
configurationBuilderCC +
.CC+ ,#
AddEnvironmentVariablesCC, C
(CCC D
)CCD E
;CCE F
returnDD  
configurationBuilderDD 2
.DD2 3
BuildDD3 8
(DD8 9
)DD9 :
;DD: ;
}EE 
)EE 
.FF 
AsFF 
<FF 
IConfigurationFF %
>FF% &
(FF& '
)FF' (
.GG 
SingleInstanceGG "
(GG" #
)GG# $
;GG$ %
builderII 
.II 
RegisterTypeII  
<II  !
ApplicationII! ,
>II, -
(II- .
)II. /
.JJ 
SingleInstanceJJ "
(JJ" #
)JJ# $
.KK 
OnActivatedKK 
(KK  
cKK  !
=>KK" $
{LL 
varMM 
configurationMM (
=MM) *
cMM+ ,
.MM, -
ContextMM- 4
.MM4 5
ResolveMM5 <
<MM< =
IConfigurationMM= K
>MMK L
(MML M
)MMM N
;MMN O
configurationNN $
.NN$ %

GetSectionNN% /
(NN/ 0
$strNN0 8
)NN8 9
?NN9 :
.NN: ;
BindNN; ?
(NN? @
cNN@ A
.NNA B
InstanceNNB J
)NNJ K
;NNK L
configurationOO $
.OO$ %
GetReloadTokenOO% 3
(OO3 4
)OO4 5
.OO5 6"
RegisterChangeCallbackOO6 L
(OOL M
_OOM N
=>OOO Q
{PP 
configurationQQ (
.QQ( )

GetSectionQQ) 3
(QQ3 4
$strQQ4 <
)QQ< =
?QQ= >
.QQ> ?
BindQQ? C
(QQC D
cQQD E
.QQE F
InstanceQQF N
)QQN O
;QQO P
}RR 
,RR 
configurationRR '
)RR' (
;RR( )
}SS 
)SS 
;SS 
builderUU 
.UU 
RegisterModuleUU "
(UU" #
newUU# &
DomainModuleUU' 3
(UU3 4
_stackUU4 :
)UU: ;
)UU; <
;UU< =
builderVV 
.VV 
RegisterModuleVV "
(VV" #
newVV# &
ServicesModuleVV' 5
(VV5 6
_stackVV6 <
)VV< =
)VV= >
;VV> ?
builderWW 
.WW 
RegisterModuleWW "
(WW" #
newWW# &
SearchModuleWW' 3
(WW3 4
_stackWW4 :
)WW: ;
)WW; <
;WW< =
builderXX 
.XX 
RegisterModuleXX "
(XX" #
newXX# &
ReflectionModuleXX' 7
(XX7 8
_stackXX8 >
)XX> ?
)XX? @
;XX@ A
builderZZ 
.ZZ 
RegisterModuleZZ "
(ZZ" #
newZZ# &
LoggingModuleZZ' 4
(ZZ4 5
)ZZ5 6
)ZZ6 7
;ZZ7 8
builder[[ 
.[[ 
RegisterModule[[ "
([[" #
new[[# &
NullCachingModule[[' 8
([[8 9
)[[9 :
)[[: ;
;[[; <
builder]] 
.]] 
RegisterType]]  
<]]  !
DiscoveryService]]! 1
>]]1 2
(]]2 3
)]]3 4
.^^ 
As^^ 
<^^ 
IDiscoverTypes^^ %
>^^% &
(^^& '
)^^' (
.__ 
SingleInstance__ "
(__" #
)__# $
;__$ %
}`` 	
}aa 
}bb Ù
@C:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\Contact.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Configuration %
{		 
public 

class 
Contact 
{ 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public&& 
string&& 
Url&& 
{&& 
get&& 
;&&  
set&&! $
;&&$ %
}&&& '
}'' 
}(( L
JC:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\IComponentContext.cs€
@C:\Source\Stacks\Core\src\Slalom.Stacks\Configuration\License.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Configuration %
{		 
public 

class 
License 
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Url 
{ 
get 
;  
set! $
;$ %
}& '
} 
}   é
?C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\AggregateRoot.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{		 
public 

abstract 
class 
AggregateRoot '
:( )
Entity* 0
,0 1
IAggregateRoot2 @
{ 
} 
} Ÿ@
;C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\ConceptAs.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
[ 
JsonConverter 
( 
typeof 
( 
ConceptConverter *
)* +
)+ ,
], -
public 

abstract 
class 
	ConceptAs #
<# $
TValue$ *
>* +
:, -

IEquatable. 8
<8 9
	ConceptAs9 B
<B C
TValueC I
>I J
>J K
,K L
	IValidateM V
{ 
	protected 
	ConceptAs 
( 
) 
{ 	
} 	
	protected## 
	ConceptAs## 
(## 
TValue## "
value### (
)##( )
{$$ 	
this%% 
.%% 
Value%% 
=%% 
value%% 
;%% 
}&& 	
public,, 
TValue,, 
Value,, 
{,, 
get,, !
;,,! "
	protected,,# ,
set,,- 0
;,,0 1
},,2 3
public33 
virtual33 
bool33 
Equals33 "
(33" #
	ConceptAs33# ,
<33, -
TValue33- 3
>333 4
other335 :
)33: ;
{44 	
if55 
(55 
other55 
==55 
null55 
)55 
{66 
return77 
false77 
;77 
}88 
if:: 
(:: 
ReferenceEquals:: 
(::  
this::  $
,::$ %
other::& +
)::+ ,
)::, -
{;; 
return<< 
true<< 
;<< 
}== 
return?? 
this?? 
.?? 
GetType?? 
(??  
)??  !
==??" $
other??% *
.??* +
GetType??+ 2
(??2 3
)??3 4
&&??5 7
EqualityComparer??8 H
<??H I
TValue??I O
>??O P
.??P Q
Default??Q X
.??X Y
Equals??Y _
(??_ `
this??` d
.??d e
Value??e j
,??j k
other??l q
.??q r
Value??r w
)??w x
;??x y
}@@ 	
publicII 
abstractII 
IEnumerableII #
<II# $
ValidationErrorII$ 3
>II3 4
ValidateII5 =
(II= >
)II> ?
;II? @
publicOO 
voidOO 
EnsureValidOO 
(OO  
)OO  !
{PP 	
varQQ 
resultsQQ 
=QQ 
thisQQ 
.QQ 
ValidateQQ '
(QQ' (
)QQ( )
.QQ) *
ToArrayQQ* 1
(QQ1 2
)QQ2 3
;QQ3 4
ifRR 
(RR 
resultsRR 
.RR 
AnyRR 
(RR 
)RR 
)RR 
{SS 
throwTT 
newTT 
ValidationExceptionTT -
(TT- .
resultsTT. 5
)TT5 6
;TT6 7
}UU 
}VV 	
public]] 
override]] 
bool]] 
Equals]] #
(]]# $
object]]$ *
obj]]+ .
)]]. /
{^^ 	
return__ 
obj__ 
!=__ 
null__ 
&&__ !
this__" &
.__& '
Equals__' -
(__- .
obj__. 1
as__2 4
	ConceptAs__5 >
<__> ?
TValue__? E
>__E F
)__F G
;__G H
}`` 	
publicff 
overrideff 
intff 
GetHashCodeff '
(ff' (
)ff( )
{gg 	
returnhh #
GenerateHashForInstancehh *
(hh* +
typeofhh+ 1
(hh1 2
TValuehh2 8
)hh8 9
,hh9 :
thishh; ?
.hh? @
Valuehh@ E
)hhE F
;hhF G
}ii 	
publicqq 
staticqq 
boolqq 
operatorqq #
==qq$ &
(qq& '
	ConceptAsqq' 0
<qq0 1
TValueqq1 7
>qq7 8
leftqq9 =
,qq= >
	ConceptAsqq? H
<qqH I
TValueqqI O
>qqO P
rightqqQ V
)qqV W
{rr 	
ifss 
(ss 
ReferenceEqualsss 
(ss  
leftss  $
,ss$ %
nullss& *
)ss* +
&&ss, .
ReferenceEqualsss/ >
(ss> ?
rightss? D
,ssD E
nullssF J
)ssJ K
)ssK L
{tt 
returnuu 
trueuu 
;uu 
}vv 
ifxx 
(xx 
ReferenceEqualsxx 
(xx  
leftxx  $
,xx$ %
nullxx& *
)xx* +
^xx, -
ReferenceEqualsxx. =
(xx= >
rightxx> C
,xxC D
nullxxE I
)xxI J
)xxJ K
{yy 
returnzz 
falsezz 
;zz 
}{{ 
return}} 
(}} 
bool}} 
)}} 
left}} 
?}} 
.}}  
Equals}}  &
(}}& '
right}}' ,
)}}, -
;}}- .
}~~ 	
public
ÖÖ 
static
ÖÖ 
implicit
ÖÖ 
operator
ÖÖ '
TValue
ÖÖ( .
(
ÖÖ. /
	ConceptAs
ÖÖ/ 8
<
ÖÖ8 9
TValue
ÖÖ9 ?
>
ÖÖ? @
value
ÖÖA F
)
ÖÖF G
{
ÜÜ 	
return
áá 
value
áá 
==
áá 
null
áá  
?
áá! "
default
áá# *
(
áá* +
TValue
áá+ 1
)
áá1 2
:
áá3 4
value
áá5 :
.
áá: ;
Value
áá; @
;
áá@ A
}
àà 	
public
êê 
static
êê 
bool
êê 
operator
êê #
!=
êê$ &
(
êê& '
	ConceptAs
êê' 0
<
êê0 1
TValue
êê1 7
>
êê7 8
left
êê9 =
,
êê= >
	ConceptAs
êê? H
<
êêH I
TValue
êêI O
>
êêO P
right
êêQ V
)
êêV W
{
ëë 	
return
íí 
!
íí 
(
íí 
left
íí 
==
íí 
right
íí "
)
íí" #
;
íí# $
}
ìì 	
public
ôô 
override
ôô 
string
ôô 
ToString
ôô '
(
ôô' (
)
ôô( )
{
öö 	
return
õõ 
this
õõ 
.
õõ 
Value
õõ 
==
õõ  
null
õõ! %
?
õõ& '
default
õõ( /
(
õõ/ 0
TValue
õõ0 6
)
õõ6 7
==
õõ8 :
null
õõ; ?
?
õõ@ A
null
õõB F
:
õõG H
default
õõI P
(
õõP Q
TValue
õõQ W
)
õõW X
?
õõX Y
.
õõY Z
ToString
õõZ b
(
õõb c
)
õõc d
:
õõe f
this
õõg k
.
õõk l
Value
õõl q
.
õõq r
ToString
õõr z
(
õõz {
)
õõ{ |
;
õõ| }
}
úú 	
	protected
££ 
static
££ 
int
££ %
GenerateHashForInstance
££ 4
(
££4 5
params
££5 ;
object
££< B
[
££B C
]
££C D

parameters
££E O
)
££O P
{
§§ 	
	unchecked
•• 
{
¶¶ 
return
ßß 

parameters
ßß !
.
ßß! "
Where
ßß" '
(
ßß' (
param
ßß( -
=>
ßß. 0
param
ßß1 6
!=
ßß7 9
null
ßß: >
)
ßß> ?
.
ßß? @
	Aggregate
ßß@ I
(
ßßI J
$num
ßßJ L
,
ßßL M
(
ßßN O
current
ßßO V
,
ßßV W
param
ßßX ]
)
ßß] ^
=>
ßß_ a
current
ßßb i
*
ßßj k
$num
ßßl n
+
ßßo p
param
ßßq v
.
ßßv w
GetHashCodeßßw Ç
(ßßÇ É
)ßßÉ Ñ
)ßßÑ Ö
;ßßÖ Ü
}
®® 
}
©© 	
}
™™ 
}´´ ÖΩ
>C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\DomainFacade.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

class 
DomainFacade 
: 
IDomainFacade  -
{ 
private 
readonly 
ICacheManager &
_cacheManager' 4
;4 5
private 
readonly 
IComponentContext *
_componentContext+ <
;< =
private 
readonly  
ConcurrentDictionary -
<- .
Type. 2
,2 3
object4 :
>: ;

_instances< F
=G H
newI L 
ConcurrentDictionaryM a
<a b
Typeb f
,f g
objecth n
>n o
(o p
)p q
;q r
public## 
DomainFacade## 
(## 
IComponentContext## -
componentContext##. >
,##> ?
ICacheManager##@ M
cacheManager##N Z
)##Z [
{$$ 	
Argument%% 
.%% 
NotNull%% 
(%% 
componentContext%% -
,%%- .
nameof%%/ 5
(%%5 6
componentContext%%6 F
)%%F G
)%%G H
;%%H I
Argument&& 
.&& 
NotNull&& 
(&& 
cacheManager&& )
,&&) *
nameof&&+ 1
(&&1 2
cacheManager&&2 >
)&&> ?
)&&? @
;&&@ A
_componentContext(( 
=(( 
componentContext((  0
;((0 1
_cacheManager)) 
=)) 
cacheManager)) (
;))( )
}** 	
public-- 
async-- 
Task-- 
Add-- 
<-- 
TAggregateRoot-- ,
>--, -
(--- .
TAggregateRoot--. <
[--< =
]--= >
	instances--? H
)--H I
where--J O
TAggregateRoot--P ^
:--_ `
IAggregateRoot--a o
{.. 	
if// 
(// 
	instances// 
==// 
null// !
)//! "
{00 
throw11 
new11 !
ArgumentNullException11 /
(11/ 0
nameof110 6
(116 7
	instances117 @
)11@ A
)11A B
;11B C
}22 
if44 
(44 
!44 
	instances44 
.44 
Any44 
(44 
)44  
)44  !
{55 
return66 
;66 
}77 
var99 

repository99 
=99 
(99 
IRepository99 )
<99) *
TAggregateRoot99* 8
>998 9
)999 :

_instances99; E
.99E F
GetOrAdd99F N
(99N O
typeof99O U
(99U V
TAggregateRoot99V d
)99d e
,99e f
t99g h
=>99i k
_componentContext99l }
.99} ~
Resolve	99~ Ö
<
99Ö Ü
IRepository
99Ü ë
<
99ë í
TAggregateRoot
99í †
>
99† °
>
99° ¢
(
99¢ £
)
99£ §
)
99§ •
;
99• ¶
if;; 
(;; 

repository;; 
==;; 
null;; "
);;" #
{<< 
throw== 
new== %
InvalidOperationException== 3
(==3 4
$"==4 67
+No repository has been registered for type ==6 a
{==a b
typeof==b h
(==h i
TAggregateRoot==i w
)==w x
}==x y
.==y z
"==z {
)=={ |
;==| }
}>> 
await@@ 

repository@@ 
.@@ 
Add@@  
(@@  !
	instances@@! *
)@@* +
;@@+ ,
awaitAA 
_cacheManagerAA 
.AA  
AddAsyncAA  (
(AA( )
	instancesAA) 2
)AA2 3
;AA3 4
}BB 	
publicEE 
TaskEE 
AddEE 
<EE 
TAggregateRootEE &
>EE& '
(EE' (
IEnumerableEE( 3
<EE3 4
TAggregateRootEE4 B
>EEB C
	instancesEED M
)EEM N
whereEEO T
TAggregateRootEEU c
:EEd e
IAggregateRootEEf t
{FF 	
returnGG 
thisGG 
.GG 
AddGG 
(GG 
	instancesGG %
.GG% &
ToArrayGG& -
(GG- .
)GG. /
)GG/ 0
;GG0 1
}HH 	
publicKK 
TaskKK 
AddKK 
<KK 
TAggregateRootKK &
>KK& '
(KK' (
ListKK( ,
<KK, -
TAggregateRootKK- ;
>KK; <
	instancesKK= F
)KKF G
whereKKH M
TAggregateRootKKN \
:KK] ^
IAggregateRootKK_ m
{LL 	
returnMM 
thisMM 
.MM 
AddMM 
(MM 
	instancesMM %
.MM% &
ToArrayMM& -
(MM- .
)MM. /
)MM/ 0
;MM0 1
}NN 	
publicQQ 
asyncQQ 
TaskQQ 
ClearQQ 
<QQ  
TAggregateRootQQ  .
>QQ. /
(QQ/ 0
)QQ0 1
whereQQ2 7
TAggregateRootQQ8 F
:QQG H
IAggregateRootQQI W
{RR 	
varSS 

repositorySS 
=SS 
(SS 
IRepositorySS )
<SS) *
TAggregateRootSS* 8
>SS8 9
)SS9 :

_instancesSS; E
.SSE F
GetOrAddSSF N
(SSN O
typeofSSO U
(SSU V
TAggregateRootSSV d
)SSd e
,SSe f
tSSg h
=>SSi k
_componentContextSSl }
.SS} ~
Resolve	SS~ Ö
<
SSÖ Ü
IRepository
SSÜ ë
<
SSë í
TAggregateRoot
SSí †
>
SS† °
>
SS° ¢
(
SS¢ £
)
SS£ §
)
SS§ •
;
SS• ¶
ifUU 
(UU 

repositoryUU 
==UU 
nullUU "
)UU" #
{VV 
throwWW 
newWW %
InvalidOperationExceptionWW 3
(WW3 4
$"WW4 67
+No repository has been registered for type WW6 a
{WWa b
typeofWWb h
(WWh i
TAggregateRootWWi w
)WWw x
}WWx y
.WWy z
"WWz {
)WW{ |
;WW| }
}XX 
awaitZZ 

repositoryZZ 
.ZZ 
ClearZZ "
(ZZ" #
)ZZ# $
;ZZ$ %
await\\ 
_cacheManager\\ 
.\\  

ClearAsync\\  *
(\\* +
)\\+ ,
;\\, -
}]] 	
public`` 
Task`` 
<`` 
bool`` 
>`` 
Exists``  
<``  !
TAggregateRoot``! /
>``/ 0
(``0 1

Expression``1 ;
<``; <
Func``< @
<``@ A
TAggregateRoot``A O
,``O P
bool``Q U
>``U V
>``V W

expression``X b
)``b c
where``d i
TAggregateRoot``j x
:``y z
IAggregateRoot	``{ â
{aa 	
varbb 

repositorybb 
=bb 
(bb 
IRepositorybb )
<bb) *
TAggregateRootbb* 8
>bb8 9
)bb9 :

_instancesbb; E
.bbE F
GetOrAddbbF N
(bbN O
typeofbbO U
(bbU V
TAggregateRootbbV d
)bbd e
,bbe f
tbbg h
=>bbi k
_componentContextbbl }
.bb} ~
Resolve	bb~ Ö
<
bbÖ Ü
IRepository
bbÜ ë
<
bbë í
TAggregateRoot
bbí †
>
bb† °
>
bb° ¢
(
bb¢ £
)
bb£ §
)
bb§ •
;
bb• ¶
ifdd 
(dd 

repositorydd 
==dd 
nulldd "
)dd" #
{ee 
throwff 
newff %
InvalidOperationExceptionff 3
(ff3 4
$"ff4 67
+No repository has been registered for type ff6 a
{ffa b
typeofffb h
(ffh i
TAggregateRootffi w
)ffw x
}ffx y
.ffy z
"ffz {
)ff{ |
;ff| }
}gg 
returnii 

repositoryii 
.ii 
Existsii $
(ii$ %

expressionii% /
)ii/ 0
;ii0 1
}jj 	
publicmm 
asyncmm 
Taskmm 
<mm 
TAggregateRootmm (
>mm( )
Findmm* .
<mm. /
TAggregateRootmm/ =
>mm= >
(mm> ?
stringmm? E
idmmF H
)mmH I
wheremmJ O
TAggregateRootmmP ^
:mm_ `
IAggregateRootmma o
{nn 	
varoo 
targetoo 
=oo 
awaitoo 
_cacheManageroo ,
.oo, -
	FindAsyncoo- 6
<oo6 7
TAggregateRootoo7 E
>ooE F
(ooF G
idooG I
)ooI J
;ooJ K
ifpp 
(pp 
targetpp 
!=pp 
nullpp 
)pp 
{qq 
returnrr 
targetrr 
;rr 
}ss 
varuu 

repositoryuu 
=uu 
(uu 
IRepositoryuu )
<uu) *
TAggregateRootuu* 8
>uu8 9
)uu9 :

_instancesuu; E
.uuE F
GetOrAdduuF N
(uuN O
typeofuuO U
(uuU V
TAggregateRootuuV d
)uud e
,uue f
tuug h
=>uui k
_componentContextuul }
.uu} ~
Resolve	uu~ Ö
<
uuÖ Ü
IRepository
uuÜ ë
<
uuë í
TAggregateRoot
uuí †
>
uu† °
>
uu° ¢
(
uu¢ £
)
uu£ §
)
uu§ •
;
uu• ¶
ifww 
(ww 

repositoryww 
==ww 
nullww "
)ww" #
{xx 
throwyy 
newyy %
InvalidOperationExceptionyy 3
(yy3 4
$"yy4 67
+No repository has been registered for type yy6 a
{yya b
typeofyyb h
(yyh i
TAggregateRootyyi w
)yyw x
}yyx y
.yyy z
"yyz {
)yy{ |
;yy| }
}zz 
target|| 
=|| 
await|| 

repository|| %
.||% &
Find||& *
(||* +
id||+ -
)||- .
;||. /
if~~ 
(~~ 
target~~ 
!=~~ 
null~~ 
)~~ 
{ 
await
ÄÄ 
_cacheManager
ÄÄ #
.
ÄÄ# $
AddAsync
ÄÄ$ ,
(
ÄÄ, -
target
ÄÄ- 3
)
ÄÄ3 4
;
ÄÄ4 5
}
ÅÅ 
return
ÉÉ 
target
ÉÉ 
;
ÉÉ 
}
ÑÑ 	
public
áá 
Task
áá 
<
áá 
IEnumerable
áá 
<
áá  
TAggregateRoot
áá  .
>
áá. /
>
áá/ 0
Find
áá1 5
<
áá5 6
TAggregateRoot
áá6 D
>
ááD E
(
ááE F

Expression
ááF P
<
ááP Q
Func
ááQ U
<
ááU V
TAggregateRoot
ááV d
,
áád e
bool
ááf j
>
ááj k
>
áák l

expression
áám w
)
ááw x
where
ááy ~
TAggregateRootáá ç
:ááé è
IAggregateRootááê û
{
àà 	
var
ââ 

repository
ââ 
=
ââ 
(
ââ 
IRepository
ââ )
<
ââ) *
TAggregateRoot
ââ* 8
>
ââ8 9
)
ââ9 :

_instances
ââ; E
.
ââE F
GetOrAdd
ââF N
(
ââN O
typeof
ââO U
(
ââU V
TAggregateRoot
ââV d
)
ââd e
,
ââe f
t
ââg h
=>
ââi k
_componentContext
ââl }
.
ââ} ~
Resolveââ~ Ö
<ââÖ Ü
IRepositoryââÜ ë
<ââë í
TAggregateRootââí †
>ââ† °
>ââ° ¢
(ââ¢ £
)ââ£ §
)ââ§ •
;ââ• ¶
if
ãã 
(
ãã 

repository
ãã 
==
ãã 
null
ãã "
)
ãã" #
{
åå 
throw
çç 
new
çç '
InvalidOperationException
çç 3
(
çç3 4
$"
çç4 69
+No repository has been registered for type 
çç6 a
{
çça b
typeof
ççb h
(
ççh i
TAggregateRoot
ççi w
)
ççw x
}
ççx y
.
ççy z
"
ççz {
)
çç{ |
;
çç| }
}
éé 
return
êê 

repository
êê 
.
êê 
Find
êê "
(
êê" #

expression
êê# -
)
êê- .
;
êê. /
}
ëë 	
public
îî 
Task
îî 
<
îî 
IEnumerable
îî 
<
îî  
TAggregateRoot
îî  .
>
îî. /
>
îî/ 0
Find
îî1 5
<
îî5 6
TAggregateRoot
îî6 D
>
îîD E
(
îîE F
)
îîF G
where
îîH M
TAggregateRoot
îîN \
:
îî] ^
IAggregateRoot
îî_ m
{
ïï 	
var
ññ 

repository
ññ 
=
ññ 
(
ññ 
IRepository
ññ )
<
ññ) *
TAggregateRoot
ññ* 8
>
ññ8 9
)
ññ9 :

_instances
ññ; E
.
ññE F
GetOrAdd
ññF N
(
ññN O
typeof
ññO U
(
ññU V
TAggregateRoot
ññV d
)
ññd e
,
ññe f
t
ññg h
=>
ññi k
_componentContext
ññl }
.
ññ} ~
Resolveññ~ Ö
<ññÖ Ü
IRepositoryññÜ ë
<ññë í
TAggregateRootññí †
>ññ† °
>ññ° ¢
(ññ¢ £
)ññ£ §
)ññ§ •
;ññ• ¶
if
òò 
(
òò 

repository
òò 
==
òò 
null
òò "
)
òò" #
{
ôô 
throw
öö 
new
öö '
InvalidOperationException
öö 3
(
öö3 4
$"
öö4 69
+No repository has been registered for type 
öö6 a
{
ööa b
typeof
ööb h
(
ööh i
TAggregateRoot
ööi w
)
ööw x
}
ööx y
.
ööy z
"
ööz {
)
öö{ |
;
öö| }
}
õõ 
return
ùù 

repository
ùù 
.
ùù 
Find
ùù "
(
ùù" #
)
ùù# $
;
ùù$ %
}
ûû 	
public
°° 
async
°° 
Task
°° 
Remove
°°  
<
°°  !
TAggregateRoot
°°! /
>
°°/ 0
(
°°0 1
TAggregateRoot
°°1 ?
[
°°? @
]
°°@ A
	instances
°°B K
)
°°K L
where
°°M R
TAggregateRoot
°°S a
:
°°b c
IAggregateRoot
°°d r
{
¢¢ 	
if
££ 
(
££ 
	instances
££ 
==
££ 
null
££ !
)
££! "
{
§§ 
throw
•• 
new
•• #
ArgumentNullException
•• /
(
••/ 0
nameof
••0 6
(
••6 7
	instances
••7 @
)
••@ A
)
••A B
;
••B C
}
¶¶ 
if
®® 
(
®® 
!
®® 
	instances
®® 
.
®® 
Any
®® 
(
®® 
)
®®  
)
®®  !
{
©© 
return
™™ 
;
™™ 
}
´´ 
var
≠≠ 

repository
≠≠ 
=
≠≠ 
(
≠≠ 
IRepository
≠≠ )
<
≠≠) *
TAggregateRoot
≠≠* 8
>
≠≠8 9
)
≠≠9 :

_instances
≠≠; E
.
≠≠E F
GetOrAdd
≠≠F N
(
≠≠N O
typeof
≠≠O U
(
≠≠U V
TAggregateRoot
≠≠V d
)
≠≠d e
,
≠≠e f
t
≠≠g h
=>
≠≠i k
_componentContext
≠≠l }
.
≠≠} ~
Resolve≠≠~ Ö
<≠≠Ö Ü
IRepository≠≠Ü ë
<≠≠ë í
TAggregateRoot≠≠í †
>≠≠† °
>≠≠° ¢
(≠≠¢ £
)≠≠£ §
)≠≠§ •
;≠≠• ¶
if
ØØ 
(
ØØ 

repository
ØØ 
==
ØØ 
null
ØØ "
)
ØØ" #
{
∞∞ 
throw
±± 
new
±± '
InvalidOperationException
±± 3
(
±±3 4
$"
±±4 69
+No repository has been registered for type 
±±6 a
{
±±a b
typeof
±±b h
(
±±h i
TAggregateRoot
±±i w
)
±±w x
}
±±x y
.
±±y z
"
±±z {
)
±±{ |
;
±±| }
}
≤≤ 
await
¥¥ 

repository
¥¥ 
.
¥¥ 
Remove
¥¥ #
(
¥¥# $
	instances
¥¥$ -
)
¥¥- .
;
¥¥. /
await
∂∂ 
_cacheManager
∂∂ 
.
∂∂  
RemoveAsync
∂∂  +
(
∂∂+ ,
	instances
∂∂, 5
)
∂∂5 6
;
∂∂6 7
}
∑∑ 	
public
∫∫ 
Task
∫∫ 
Remove
∫∫ 
<
∫∫ 
TAggregateRoot
∫∫ )
>
∫∫) *
(
∫∫* +
IEnumerable
∫∫+ 6
<
∫∫6 7
TAggregateRoot
∫∫7 E
>
∫∫E F
	instances
∫∫G P
)
∫∫P Q
where
∫∫R W
TAggregateRoot
∫∫X f
:
∫∫g h
IAggregateRoot
∫∫i w
{
ªª 	
return
ºº 
this
ºº 
.
ºº 
Remove
ºº 
(
ºº 
	instances
ºº (
.
ºº( )
ToArray
ºº) 0
(
ºº0 1
)
ºº1 2
)
ºº2 3
;
ºº3 4
}
ΩΩ 	
public
¿¿ 
Task
¿¿ 
Remove
¿¿ 
<
¿¿ 
TAggregateRoot
¿¿ )
>
¿¿) *
(
¿¿* +
List
¿¿+ /
<
¿¿/ 0
TAggregateRoot
¿¿0 >
>
¿¿> ?
	instances
¿¿@ I
)
¿¿I J
where
¿¿K P
TAggregateRoot
¿¿Q _
:
¿¿` a
IAggregateRoot
¿¿b p
{
¡¡ 	
return
¬¬ 
this
¬¬ 
.
¬¬ 
Remove
¬¬ 
(
¬¬ 
	instances
¬¬ (
.
¬¬( )
ToArray
¬¬) 0
(
¬¬0 1
)
¬¬1 2
)
¬¬2 3
;
¬¬3 4
}
√√ 	
public
∆∆ 
async
∆∆ 
Task
∆∆ 
Update
∆∆  
<
∆∆  !
TAggregateRoot
∆∆! /
>
∆∆/ 0
(
∆∆0 1
TAggregateRoot
∆∆1 ?
[
∆∆? @
]
∆∆@ A
	instances
∆∆B K
)
∆∆K L
where
∆∆M R
TAggregateRoot
∆∆S a
:
∆∆b c
IAggregateRoot
∆∆d r
{
«« 	
if
»» 
(
»» 
	instances
»» 
==
»» 
null
»» !
)
»»! "
{
…… 
throw
   
new
   #
ArgumentNullException
   /
(
  / 0
nameof
  0 6
(
  6 7
	instances
  7 @
)
  @ A
)
  A B
;
  B C
}
ÀÀ 
if
ÕÕ 
(
ÕÕ 
!
ÕÕ 
	instances
ÕÕ 
.
ÕÕ 
Any
ÕÕ 
(
ÕÕ 
)
ÕÕ  
)
ÕÕ  !
{
ŒŒ 
return
œœ 
;
œœ 
}
–– 
var
““ 

repository
““ 
=
““ 
(
““ 
IRepository
““ )
<
““) *
TAggregateRoot
““* 8
>
““8 9
)
““9 :

_instances
““; E
.
““E F
GetOrAdd
““F N
(
““N O
typeof
““O U
(
““U V
TAggregateRoot
““V d
)
““d e
,
““e f
t
““g h
=>
““i k
_componentContext
““l }
.
““} ~
Resolve““~ Ö
<““Ö Ü
IRepository““Ü ë
<““ë í
TAggregateRoot““í †
>““† °
>““° ¢
(““¢ £
)““£ §
)““§ •
;““• ¶
if
‘‘ 
(
‘‘ 

repository
‘‘ 
==
‘‘ 
null
‘‘ "
)
‘‘" #
{
’’ 
throw
÷÷ 
new
÷÷ '
InvalidOperationException
÷÷ 3
(
÷÷3 4
$"
÷÷4 69
+No repository has been registered for type 
÷÷6 a
{
÷÷a b
typeof
÷÷b h
(
÷÷h i
TAggregateRoot
÷÷i w
)
÷÷w x
}
÷÷x y
.
÷÷y z
"
÷÷z {
)
÷÷{ |
;
÷÷| }
}
◊◊ 
await
ŸŸ 

repository
ŸŸ 
.
ŸŸ 
Update
ŸŸ #
(
ŸŸ# $
	instances
ŸŸ$ -
)
ŸŸ- .
;
ŸŸ. /
await
€€ 
_cacheManager
€€ 
.
€€  
UpdateAsync
€€  +
(
€€+ ,
	instances
€€, 5
)
€€5 6
;
€€6 7
}
‹‹ 	
public
ﬂﬂ 
Task
ﬂﬂ 
Update
ﬂﬂ 
<
ﬂﬂ 
TAggregateRoot
ﬂﬂ )
>
ﬂﬂ) *
(
ﬂﬂ* +
IEnumerable
ﬂﬂ+ 6
<
ﬂﬂ6 7
TAggregateRoot
ﬂﬂ7 E
>
ﬂﬂE F
	instances
ﬂﬂG P
)
ﬂﬂP Q
where
ﬂﬂR W
TAggregateRoot
ﬂﬂX f
:
ﬂﬂg h
IAggregateRoot
ﬂﬂi w
{
‡‡ 	
return
·· 
this
·· 
.
·· 
Update
·· 
(
·· 
	instances
·· (
.
··( )
ToArray
··) 0
(
··0 1
)
··1 2
)
··2 3
;
··3 4
}
‚‚ 	
public
ÂÂ 
Task
ÂÂ 
Update
ÂÂ 
<
ÂÂ 
TAggregateRoot
ÂÂ )
>
ÂÂ) *
(
ÂÂ* +
List
ÂÂ+ /
<
ÂÂ/ 0
TAggregateRoot
ÂÂ0 >
>
ÂÂ> ?
	instances
ÂÂ@ I
)
ÂÂI J
where
ÂÂK P
TAggregateRoot
ÂÂQ _
:
ÂÂ` a
IAggregateRoot
ÂÂb p
{
ÊÊ 	
return
ÁÁ 
this
ÁÁ 
.
ÁÁ 
Update
ÁÁ 
(
ÁÁ 
	instances
ÁÁ (
.
ÁÁ( )
ToArray
ÁÁ) 0
(
ÁÁ0 1
)
ÁÁ1 2
)
ÁÁ2 3
;
ÁÁ3 4
}
ËË 	
public
ÎÎ 
Task
ÎÎ 
<
ÎÎ 
bool
ÎÎ 
>
ÎÎ 
Exists
ÎÎ  
<
ÎÎ  !
TAggregateRoot
ÎÎ! /
>
ÎÎ/ 0
(
ÎÎ0 1
string
ÎÎ1 7
id
ÎÎ8 :
)
ÎÎ: ;
where
ÎÎ< A
TAggregateRoot
ÎÎB P
:
ÎÎQ R
IAggregateRoot
ÎÎS a
{
ÏÏ 	
var
ÌÌ 

repository
ÌÌ 
=
ÌÌ 
(
ÌÌ 
IRepository
ÌÌ )
<
ÌÌ) *
TAggregateRoot
ÌÌ* 8
>
ÌÌ8 9
)
ÌÌ9 :

_instances
ÌÌ; E
.
ÌÌE F
GetOrAdd
ÌÌF N
(
ÌÌN O
typeof
ÌÌO U
(
ÌÌU V
TAggregateRoot
ÌÌV d
)
ÌÌd e
,
ÌÌe f
t
ÌÌg h
=>
ÌÌi k
_componentContext
ÌÌl }
.
ÌÌ} ~
ResolveÌÌ~ Ö
<ÌÌÖ Ü
IRepositoryÌÌÜ ë
<ÌÌë í
TAggregateRootÌÌí †
>ÌÌ† °
>ÌÌ° ¢
(ÌÌ¢ £
)ÌÌ£ §
)ÌÌ§ •
;ÌÌ• ¶
if
ÔÔ 
(
ÔÔ 

repository
ÔÔ 
==
ÔÔ 
null
ÔÔ "
)
ÔÔ" #
{
 
throw
ÒÒ 
new
ÒÒ '
InvalidOperationException
ÒÒ 3
(
ÒÒ3 4
$"
ÒÒ4 69
+No repository has been registered for type 
ÒÒ6 a
{
ÒÒa b
typeof
ÒÒb h
(
ÒÒh i
TAggregateRoot
ÒÒi w
)
ÒÒw x
}
ÒÒx y
.
ÒÒy z
"
ÒÒz {
)
ÒÒ{ |
;
ÒÒ| }
}
ÚÚ 
return
ÙÙ 

repository
ÙÙ 
.
ÙÙ 
Exists
ÙÙ $
(
ÙÙ$ %
id
ÙÙ% '
)
ÙÙ' (
;
ÙÙ( )
}
ıı 	
}
ˆˆ 
}˜˜ ë'
8C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\Entity.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

abstract 
class 
Entity  
:! "
IEntity# *
{ 
	protected 
Entity 
( 
) 
: 
this 
( 
NewId 
. 
NextId 
(  
)  !
)! "
{ 	
} 	
	protected!! 
Entity!! 
(!! 
string!! 
id!!  "
)!!" #
{"" 	
this## 
.## 
Id## 
=## 
id## 
;## 
}$$ 	
public** 
string** 
Id** 
{** 
get** 
;** 
	protected**  )
set*** -
;**- .
}**/ 0
public11 
virtual11 
object11 
GetKeys11 %
(11% &
)11& '
{22 	
return33 
new33 
{44 
this55 
.55 
Id55 
}66 
;66 
}77 	
publicAA 
staticAA 
boolAA 
operatorAA #
==AA$ &
(AA& '
EntityAA' -
aAA. /
,AA/ 0
EntityAA1 7
bAA8 9
)AA9 :
{BB 	
ifCC 
(CC 
ReferenceEqualsCC 
(CC  
aCC  !
,CC! "
nullCC# '
)CC' (
&&CC) +
ReferenceEqualsCC, ;
(CC; <
bCC< =
,CC= >
nullCC? C
)CCC D
)CCD E
{DD 
returnEE 
trueEE 
;EE 
}FF 
ifGG 
(GG 
ReferenceEqualsGG 
(GG  
aGG  !
,GG! "
nullGG# '
)GG' (
||GG) +
ReferenceEqualsGG, ;
(GG; <
bGG< =
,GG= >
nullGG? C
)GGC D
)GGD E
{HH 
returnII 
falseII 
;II 
}JJ 
returnKK 
aKK 
.KK 
EqualsKK 
(KK 
bKK 
)KK 
;KK 
}LL 	
publicTT 
staticTT 
boolTT 
operatorTT #
!=TT$ &
(TT& '
EntityTT' -
aTT. /
,TT/ 0
EntityTT1 7
bTT8 9
)TT9 :
{UU 	
returnVV 
!VV 
(VV 
aVV 
==VV 
bVV 
)VV 
;VV 
}WW 	
	protected__ 
bool__ 
Equals__ 
(__ 
Entity__ $
other__% *
)__* +
{`` 	
returnaa 
otheraa 
!=aa 
nullaa  
&&aa! #
thisaa$ (
.aa( )
Idaa) +
.aa+ ,
Equalsaa, 2
(aa2 3
otheraa3 8
.aa8 9
Idaa9 ;
,aa; <
StringComparisonaa= M
.aaM N
OrdinalIgnoreCaseaaN _
)aa_ `
;aa` a
}bb 	
publicii 
overrideii 
boolii 
Equalsii #
(ii# $
objectii$ *
objii+ .
)ii. /
{jj 	
ifkk 
(kk 
ReferenceEqualskk 
(kk  
nullkk  $
,kk$ %
objkk& )
)kk) *
)kk* +
{ll 
returnmm 
falsemm 
;mm 
}nn 
ifoo 
(oo 
ReferenceEqualsoo 
(oo  
thisoo  $
,oo$ %
objoo& )
)oo) *
)oo* +
{pp 
returnqq 
trueqq 
;qq 
}rr 
ifss 
(ss 
objss 
.ss 
GetTypess 
(ss 
)ss 
!=ss  
thisss! %
.ss% &
GetTypess& -
(ss- .
)ss. /
)ss/ 0
{tt 
returnuu 
falseuu 
;uu 
}vv 
returnww 
thisww 
.ww 
Equalsww 
(ww 
(ww  
Entityww  &
)ww& '
objww( +
)ww+ ,
;ww, -
}xx 	
[~~ 	
SuppressMessage~~	 
(~~ 
$str~~ $
,~~$ %
$str~~& F
)~~F G
]~~G H
public 
override 
int 
GetHashCode '
(' (
)( )
{
ÄÄ 	
return
ÅÅ 
(
ÅÅ 
this
ÅÅ 
.
ÅÅ 
GetType
ÅÅ  
(
ÅÅ  !
)
ÅÅ! "
+
ÅÅ# $
this
ÅÅ% )
.
ÅÅ) *
Id
ÅÅ* ,
)
ÅÅ, -
.
ÅÅ- .
GetHashCode
ÅÅ. 9
(
ÅÅ9 :
)
ÅÅ: ;
;
ÅÅ; <
}
ÇÇ 	
}
ÖÖ 
}ÜÜ ∂
@C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\IAggregateRoot.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{		 
public 

	interface 
IAggregateRoot #
:$ %
IEntity& -
{ 
string 
Id 
{ 
get 
; 
} 
} 
} ç,
?C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\IDomainFacade.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

	interface 
IDomainFacade "
{ 
Task## 
Add## 
<## 
TAggregateRoot## 
>##  
(##  !
params##! '
TAggregateRoot##( 6
[##6 7
]##7 8
	instances##9 B
)##B C
where##D I
TAggregateRoot##J X
:##Y Z
IAggregateRoot##[ i
;##i j
Task11 
Add11 
<11 
TAggregateRoot11 
>11  
(11  !
IEnumerable11! ,
<11, -
TAggregateRoot11- ;
>11; <
	instances11= F
)11F G
where11H M
TAggregateRoot11N \
:11] ^
IAggregateRoot11_ m
;11m n
Task?? 
Add?? 
<?? 
TAggregateRoot?? 
>??  
(??  !
List??! %
<??% &
TAggregateRoot??& 4
>??4 5
	instances??6 ?
)??? @
where??A F
TAggregateRoot??G U
:??V W
IAggregateRoot??X f
;??f g
TaskFF 
ClearFF 
<FF 
TAggregateRootFF !
>FF! "
(FF" #
)FF# $
whereFF% *
TAggregateRootFF+ 9
:FF: ;
IAggregateRootFF< J
;FFJ K
TaskNN 
<NN 
boolNN 
>NN 
ExistsNN 
<NN 
TAggregateRootNN (
>NN( )
(NN) *

ExpressionNN* 4
<NN4 5
FuncNN5 9
<NN9 :
TAggregateRootNN: H
,NNH I
boolNNJ N
>NNN O
>NNO P

expressionNNQ [
)NN[ \
whereNN] b
TAggregateRootNNc q
:NNr s
IAggregateRoot	NNt Ç
;
NNÇ É
TaskVV 
<VV 
boolVV 
>VV 
ExistsVV 
<VV 
TAggregateRootVV (
>VV( )
(VV) *
stringVV* 0
idVV1 3
)VV3 4
whereVV5 :
TAggregateRootVV; I
:VVJ K
IAggregateRootVVL Z
;VVZ [
Task^^ 
<^^ 
TAggregateRoot^^ 
>^^ 
Find^^ !
<^^! "
TAggregateRoot^^" 0
>^^0 1
(^^1 2
string^^2 8
id^^9 ;
)^^; <
where^^= B
TAggregateRoot^^C Q
:^^R S
IAggregateRoot^^T b
;^^b c
Taskff 
<ff 
IEnumerableff 
<ff 
TAggregateRootff '
>ff' (
>ff( )
Findff* .
<ff. /
TAggregateRootff/ =
>ff= >
(ff> ?

Expressionff? I
<ffI J
FuncffJ N
<ffN O
TAggregateRootffO ]
,ff] ^
boolff_ c
>ffc d
>ffd e

expressionfff p
)ffp q
whereffr w
TAggregateRoot	ffx Ü
:
ffá à
IAggregateRoot
ffâ ó
;
ffó ò
Taskmm 
<mm 
IEnumerablemm 
<mm 
TAggregateRootmm '
>mm' (
>mm( )
Findmm* .
<mm. /
TAggregateRootmm/ =
>mm= >
(mm> ?
)mm? @
wheremmA F
TAggregateRootmmG U
:mmV W
IAggregateRootmmX f
;mmf g
Taskvv 
Removevv 
<vv 
TAggregateRootvv "
>vv" #
(vv# $
paramsvv$ *
TAggregateRootvv+ 9
[vv9 :
]vv: ;
	instancesvv< E
)vvE F
wherevvG L
TAggregateRootvvM [
:vv\ ]
IAggregateRootvv^ l
;vvl m
Task 
Remove 
< 
TAggregateRoot "
>" #
(# $
IEnumerable$ /
</ 0
TAggregateRoot0 >
>> ?
	instances@ I
)I J
whereK P
TAggregateRootQ _
:` a
IAggregateRootb p
;p q
Task
àà 
Remove
àà 
<
àà 
TAggregateRoot
àà "
>
àà" #
(
àà# $
List
àà$ (
<
àà( )
TAggregateRoot
àà) 7
>
àà7 8
	instances
àà9 B
)
ààB C
where
ààD I
TAggregateRoot
ààJ X
:
ààY Z
IAggregateRoot
àà[ i
;
àài j
Task
ññ 
Update
ññ 
<
ññ 
TAggregateRoot
ññ "
>
ññ" #
(
ññ# $
params
ññ$ *
TAggregateRoot
ññ+ 9
[
ññ9 :
]
ññ: ;
	instances
ññ< E
)
ññE F
where
ññG L
TAggregateRoot
ññM [
:
ññ\ ]
IAggregateRoot
ññ^ l
;
ññl m
Task
§§ 
Update
§§ 
<
§§ 
TAggregateRoot
§§ "
>
§§" #
(
§§# $
IEnumerable
§§$ /
<
§§/ 0
TAggregateRoot
§§0 >
>
§§> ?
	instances
§§@ I
)
§§I J
where
§§K P
TAggregateRoot
§§Q _
:
§§` a
IAggregateRoot
§§b p
;
§§p q
Task
≤≤ 
Update
≤≤ 
<
≤≤ 
TAggregateRoot
≤≤ "
>
≤≤" #
(
≤≤# $
List
≤≤$ (
<
≤≤( )
TAggregateRoot
≤≤) 7
>
≤≤7 8
	instances
≤≤9 B
)
≤≤B C
where
≤≤D I
TAggregateRoot
≤≤J X
:
≤≤Y Z
IAggregateRoot
≤≤[ i
;
≤≤i j
}
≥≥ 
}¥¥ ¢
9C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\IEntity.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{		 
public 

	interface 
IEntity 
{ 
} 
} Ò
@C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\IEntityContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

	interface 
IEntityContext #
{ 
Task 
Add 
< 
TEntity 
> 
( 
TEntity !
[! "
]" #
	instances$ -
)- .
where/ 4
TEntity5 <
:= >
class? D
,D E
IAggregateRootF T
;T U
Task"" 
Clear"" 
<"" 
TEntity"" 
>"" 
("" 
)"" 
where"" #
TEntity""$ +
:"", -
class"". 3
,""3 4
IAggregateRoot""5 C
;""C D
Task)) 
<)) 
bool)) 
>)) 
Exists)) 
<)) 
TEntity)) !
>))! "
())" #

Expression))# -
<))- .
Func)). 2
<))2 3
TEntity))3 :
,)): ;
bool))< @
>))@ A
>))A B

expression))C M
)))M N
where))O T
TEntity))U \
:))] ^
class))_ d
,))d e
IAggregateRoot))f t
;))t u
Task11 
<11 
bool11 
>11 
Exists11 
<11 
TEntity11 !
>11! "
(11" #
string11# )
id11* ,
)11, -
where11. 3
TEntity114 ;
:11< =
class11> C
,11C D
IAggregateRoot11E S
;11S T
Task99 
<99 
TEntity99 
>99 
Find99 
<99 
TEntity99 "
>99" #
(99# $
string99$ *
id99+ -
)99- .
where99/ 4
TEntity995 <
:99= >
class99? D
,99D E
IAggregateRoot99F T
;99T U
TaskAA 
<AA 
IEnumerableAA 
<AA 
TEntityAA  
>AA  !
>AA! "
FindAA# '
<AA' (
TEntityAA( /
>AA/ 0
(AA0 1

ExpressionAA1 ;
<AA; <
FuncAA< @
<AA@ A
TEntityAAA H
,AAH I
boolAAJ N
>AAN O
>AAO P

expressionAAQ [
)AA[ \
whereAA] b
TEntityAAc j
:AAk l
classAAm r
,AAr s
IAggregateRoot	AAt Ç
;
AAÇ É
TaskHH 
<HH 
IEnumerableHH 
<HH 
TEntityHH  
>HH  !
>HH! "
FindHH# '
<HH' (
TEntityHH( /
>HH/ 0
(HH0 1
)HH1 2
whereHH3 8
TEntityHH9 @
:HHA B
classHHC H
,HHH I
IAggregateRootHHJ X
;HHX Y
TaskQQ 
RemoveQQ 
<QQ 
TEntityQQ 
>QQ 
(QQ 
TEntityQQ $
[QQ$ %
]QQ% &
	instancesQQ' 0
)QQ0 1
whereQQ2 7
TEntityQQ8 ?
:QQ@ A
classQQB G
,QQG H
IAggregateRootQQI W
;QQW X
TaskZZ 
UpdateZZ 
<ZZ 
TEntityZZ 
>ZZ 
(ZZ 
TEntityZZ $
[ZZ$ %
]ZZ% &
	instancesZZ' 0
)ZZ0 1
whereZZ2 7
TEntityZZ8 ?
:ZZ@ A
classZZB G
,ZZG H
IAggregateRootZZI W
;ZZW X
}[[ 
}\\ ÔR
GC:\Source\Stacks\Core\src\Slalom.Stacks\Domain\InMemoryEntityContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

class !
InMemoryEntityContext &
:' (
IEntityContext) 7
{ 
private 
readonly  
ReaderWriterLockSlim -
	CacheLock. 7
=8 9
new: = 
ReaderWriterLockSlim> R
(R S
)S T
;T U
private"" 
readonly"" 
List"" 
<"" 
IAggregateRoot"" ,
>"", -
	Instances"". 7
=""8 9
new"": =
List""> B
<""B C
IAggregateRoot""C Q
>""Q R
(""R S
)""S T
;""T U
public%% 
Task%% 
Add%% 
<%% 
TEntity%% 
>%%  
(%%  !
TEntity%%! (
[%%( )
]%%) *
	instances%%+ 4
)%%4 5
where%%6 ;
TEntity%%< C
:%%D E
class%%F K
,%%K L
IAggregateRoot%%M [
{&& 	
Argument'' 
.'' 
NotNull'' 
('' 
	instances'' &
,''& '
nameof''( .
(''. /
	instances''/ 8
)''8 9
)''9 :
;'': ;
	CacheLock)) 
.)) 
EnterWriteLock)) $
())$ %
)))% &
;))& '
try** 
{++ 
foreach,, 
(,, 
var,, 
instance,, %
in,,& (
	instances,,) 2
),,2 3
{-- 
	Instances.. 
... 
Add.. !
(..! "
instance.." *
)..* +
;..+ ,
}// 
}00 
finally11 
{22 
	CacheLock33 
.33 
ExitWriteLock33 '
(33' (
)33( )
;33) *
}44 
return55 
Task55 
.55 

FromResult55 "
(55" #
$num55# $
)55$ %
;55% &
}66 	
public99 
Task99 
Clear99 
<99 
TEntity99 !
>99! "
(99" #
)99# $
where99% *
TEntity99+ 2
:993 4
class995 :
,99: ;
IAggregateRoot99< J
{:: 	
	CacheLock;; 
.;; 
EnterWriteLock;; $
(;;$ %
);;% &
;;;& '
try<< 
{== 
	Instances>> 
.>> 
	RemoveAll>> #
(>># $
e>>$ %
=>>>& (
e>>) *
is>>+ -
TEntity>>. 5
)>>5 6
;>>6 7
}?? 
finally@@ 
{AA 
	CacheLockBB 
.BB 
ExitWriteLockBB '
(BB' (
)BB( )
;BB) *
}CC 
returnDD 
TaskDD 
.DD 

FromResultDD "
(DD" #
$numDD# $
)DD$ %
;DD% &
}EE 	
publicHH 
asyncHH 
TaskHH 
<HH 
boolHH 
>HH 
ExistsHH  &
<HH& '
TEntityHH' .
>HH. /
(HH/ 0

ExpressionHH0 :
<HH: ;
FuncHH; ?
<HH? @
TEntityHH@ G
,HHG H
boolHHI M
>HHM N
>HHN O

expressionHHP Z
)HHZ [
whereHH\ a
TEntityHHb i
:HHj k
classHHl q
,HHq r
IAggregateRoot	HHs Å
{II 	
varJJ 
currentJJ 
=JJ 
awaitJJ 
thisJJ  $
.JJ$ %
FindJJ% )
(JJ) *

expressionJJ* 4
)JJ4 5
;JJ5 6
returnLL 
currentLL 
.LL 
AnyLL 
(LL 
)LL  
;LL  !
}MM 	
publicPP 
TaskPP 
<PP 
TEntityPP 
>PP 
FindPP !
<PP! "
TEntityPP" )
>PP) *
(PP* +
stringPP+ 1
idPP2 4
)PP4 5
wherePP6 ;
TEntityPP< C
:PPD E
classPPF K
,PPK L
IAggregateRootPPM [
{QQ 	
	CacheLockRR 
.RR 
EnterReadLockRR #
(RR# $
)RR$ %
;RR% &
trySS 
{TT 
returnUU 
TaskUU 
.UU 

FromResultUU &
(UU& '
(UU' (
TEntityUU( /
)UU/ 0
	InstancesUU1 :
.UU: ;
FindUU; ?
(UU? @
eUU@ A
=>UUB D
eUUE F
.UUF G
IdUUG I
==UUJ L
idUUM O
)UUO P
)UUP Q
;UUQ R
}VV 
finallyWW 
{XX 
	CacheLockYY 
.YY 
ExitReadLockYY &
(YY& '
)YY' (
;YY( )
}ZZ 
}[[ 	
public^^ 
async^^ 
Task^^ 
<^^ 
IEnumerable^^ %
<^^% &
TEntity^^& -
>^^- .
>^^. /
Find^^0 4
<^^4 5
TEntity^^5 <
>^^< =
(^^= >

Expression^^> H
<^^H I
Func^^I M
<^^M N
TEntity^^N U
,^^U V
bool^^W [
>^^[ \
>^^\ ]

expression^^^ h
)^^h i
where^^j o
TEntity^^p w
:^^x y
class^^z 
,	^^ Ä
IAggregateRoot
^^Å è
{__ 	
	CacheLock`` 
.`` 
EnterReadLock`` #
(``# $
)``$ %
;``% &
tryaa 
{bb 
varcc 
functioncc 
=cc 

expressioncc )
.cc) *
Compilecc* 1
(cc1 2
)cc2 3
;cc3 4
varee 
resultee 
=ee 
	Instancesee &
.ee& '
OfTypeee' -
<ee- .
TEntityee. 5
>ee5 6
(ee6 7
)ee7 8
.ee8 9
Whereee9 >
(ee> ?
functionee? G
)eeG H
.eeH I
ToListeeI O
(eeO P
)eeP Q
;eeQ R
returngg 
resultgg 
;gg 
}hh 
finallyii 
{jj 
	CacheLockkk 
.kk 
ExitReadLockkk &
(kk& '
)kk' (
;kk( )
}ll 
}mm 	
publicpp 
asyncpp 
Taskpp 
<pp 
IEnumerablepp %
<pp% &
TEntitypp& -
>pp- .
>pp. /
Findpp0 4
<pp4 5
TEntitypp5 <
>pp< =
(pp= >
)pp> ?
wherepp@ E
TEntityppF M
:ppN O
classppP U
,ppU V
IAggregateRootppW e
{qq 	
	CacheLockrr 
.rr 
EnterReadLockrr #
(rr# $
)rr$ %
;rr% &
tryss 
{tt 
varuu 
resultuu 
=uu 
	Instancesuu &
.uu& '
OfTypeuu' -
<uu- .
TEntityuu. 5
>uu5 6
(uu6 7
)uu7 8
.uu8 9
ToListuu9 ?
(uu? @
)uu@ A
;uuA B
returnww 
resultww 
;ww 
}xx 
finallyyy 
{zz 
	CacheLock{{ 
.{{ 
ExitReadLock{{ &
({{& '
){{' (
;{{( )
}|| 
}}} 	
public
ÄÄ 
Task
ÄÄ 
Remove
ÄÄ 
<
ÄÄ 
TEntity
ÄÄ "
>
ÄÄ" #
(
ÄÄ# $
TEntity
ÄÄ$ +
[
ÄÄ+ ,
]
ÄÄ, -
	instances
ÄÄ. 7
)
ÄÄ7 8
where
ÄÄ9 >
TEntity
ÄÄ? F
:
ÄÄG H
class
ÄÄI N
,
ÄÄN O
IAggregateRoot
ÄÄP ^
{
ÅÅ 	
	CacheLock
ÇÇ 
.
ÇÇ 
EnterWriteLock
ÇÇ $
(
ÇÇ$ %
)
ÇÇ% &
;
ÇÇ& '
try
ÉÉ 
{
ÑÑ 
var
ÖÖ 
ids
ÖÖ 
=
ÖÖ 
	instances
ÖÖ #
.
ÖÖ# $
Select
ÖÖ$ *
(
ÖÖ* +
e
ÖÖ+ ,
=>
ÖÖ- /
e
ÖÖ0 1
.
ÖÖ1 2
Id
ÖÖ2 4
)
ÖÖ4 5
.
ÖÖ5 6
ToList
ÖÖ6 <
(
ÖÖ< =
)
ÖÖ= >
;
ÖÖ> ?
	Instances
ÜÜ 
.
ÜÜ 
	RemoveAll
ÜÜ #
(
ÜÜ# $
e
ÜÜ$ %
=>
ÜÜ& (
ids
ÜÜ) ,
.
ÜÜ, -
Contains
ÜÜ- 5
(
ÜÜ5 6
e
ÜÜ6 7
.
ÜÜ7 8
Id
ÜÜ8 :
)
ÜÜ: ;
)
ÜÜ; <
;
ÜÜ< =
}
áá 
finally
àà 
{
ââ 
	CacheLock
ää 
.
ää 
ExitWriteLock
ää '
(
ää' (
)
ää( )
;
ää) *
}
ãã 
return
åå 
Task
åå 
.
åå 

FromResult
åå "
(
åå" #
$num
åå# $
)
åå$ %
;
åå% &
}
çç 	
public
êê 
async
êê 
Task
êê 
Update
êê  
<
êê  !
TEntity
êê! (
>
êê( )
(
êê) *
TEntity
êê* 1
[
êê1 2
]
êê2 3
	instances
êê4 =
)
êê= >
where
êê? D
TEntity
êêE L
:
êêM N
class
êêO T
,
êêT U
IAggregateRoot
êêV d
{
ëë 	
await
íí 
this
íí 
.
íí 
Remove
íí 
(
íí 
	instances
íí '
)
íí' (
;
íí( )
await
îî 
this
îî 
.
îî 
Add
îî 
(
îî 
	instances
îî $
)
îî$ %
;
îî% &
}
ïï 	
public
òò 
async
òò 
Task
òò 
<
òò 
bool
òò 
>
òò 
Exists
òò  &
<
òò& '
TEntity
òò' .
>
òò. /
(
òò/ 0
string
òò0 6
id
òò7 9
)
òò9 :
where
òò; @
TEntity
òòA H
:
òòI J
class
òòK P
,
òòP Q
IAggregateRoot
òòR `
{
ôô 	
var
öö 
target
öö 
=
öö 
await
öö 
this
öö #
.
öö# $
Find
öö$ (
<
öö( )
TEntity
öö) 0
>
öö0 1
(
öö1 2
id
öö2 4
)
öö4 5
;
öö5 6
return
úú 
target
úú 
!=
úú 
null
úú !
;
úú! "
}
ùù 	
}
ûû 
}üü Ú
=C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\IRepository.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

	interface 
IRepository  
<  !
TRoot! &
>& '
where( -
TRoot. 3
:4 5
IAggregateRoot6 D
{ 
Task 
Add 
( 
TRoot 
[ 
] 
	instances "
)" #
;# $
Task!! 
Clear!! 
(!! 
)!! 
;!! 
Task(( 
<(( 
bool(( 
>(( 
Exists(( 
((( 

Expression(( $
<(($ %
Func((% )
<(() *
TRoot((* /
,((/ 0
bool((1 5
>((5 6
>((6 7

expression((8 B
)((B C
;((C D
Task// 
<// 
bool// 
>// 
Exists// 
(// 
string//  
id//! #
)//# $
;//$ %
Task66 
<66 
TRoot66 
>66 
Find66 
(66 
string66 
id66  "
)66" #
;66# $
Task== 
<== 
IEnumerable== 
<== 
TRoot== 
>== 
>==  
Find==! %
(==% &

Expression==& 0
<==0 1
Func==1 5
<==5 6
TRoot==6 ;
,==; <
bool=== A
>==A B
>==B C

expression==D N
)==N O
;==O P
TaskCC 
<CC 
IEnumerableCC 
<CC 
TRootCC 
>CC 
>CC  
FindCC! %
(CC% &
)CC& '
;CC' (
TaskKK 
RemoveKK 
(KK 
TRootKK 
[KK 
]KK 
	instancesKK %
)KK% &
;KK& '
TaskRR 
UpdateRR 
(RR 
TRootRR 
[RR 
]RR 
	instancesRR %
)RR% &
;RR& '
}SS 
}TT ƒ
FC:\Source\Stacks\Core\src\Slalom.Stacks\Domain\Modules\DomainModule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
. 
Modules &
{ 
internal 
class 
DomainModule 
:  !
Module" (
{ 
private 
readonly 
Stack 
_stack %
;% &
public 
DomainModule 
( 
Stack !
stack" '
)' (
{ 	
_stack 
= 
stack 
; 
} 	
	protected&& 
override&& 
void&& 
Load&&  $
(&&$ %
ContainerBuilder&&% 5
builder&&6 =
)&&= >
{'' 	
base(( 
.(( 
Load(( 
((( 
builder(( 
)(( 
;(( 
builder** 
.** 
Register** 
(** 
c** 
=>** !
new**" %
DomainFacade**& 2
(**2 3
c**3 4
.**4 5
Resolve**5 <
<**< =
IComponentContext**= N
>**N O
(**O P
)**P Q
,**Q R
c**S T
.**T U
Resolve**U \
<**\ ]
ICacheManager**] j
>**j k
(**k l
)**l m
)**m n
)**n o
.++ 
As++ 
<++ 
IDomainFacade++ !
>++! "
(++" #
)++# $
.,, 
SingleInstance,, 
(,,  
),,  !
;,,! "
builder.. 
... 
Register.. 
(.. 
e.. 
=>.. !
new.." %!
InMemoryEntityContext..& ;
(..; <
)..< =
)..= >
.// 
As// 
<// 
IEntityContext// "
>//" #
(//# $
)//$ %
.00 
SingleInstance00 
(00  
)00  !
;00! "
builder22 
.22 
RegisterGeneric22 #
(22# $
typeof22$ *
(22* +

Repository22+ 5
<225 6
>226 7
)227 8
)228 9
.33 
As33 
(33 
typeof33 
(33 
IRepository33 &
<33& '
>33' (
)33( )
)33) *
.44 
PropertiesAutowired44 $
(44$ %
)44% &
.55 
SingleInstance55 
(55  
)55  !
;55! "
}66 	
}77 
}88 ‰a
<C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\Repository.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

class 

Repository 
< 
TRoot !
>! "
:# $
IRepository% 0
<0 1
TRoot1 6
>6 7
where8 =
TRoot> C
:D E
classF K
,K L
IAggregateRootM [
{ 
private 
readonly 
IEntityContext '
_context( 0
;0 1
public 

Repository 
( 
IEntityContext (
context) 0
)0 1
{   	
Argument!! 
.!! 
NotNull!! 
(!! 
context!! $
,!!$ %
nameof!!& ,
(!!, -
context!!- 4
)!!4 5
)!!5 6
;!!6 7
_context## 
=## 
context## 
;## 
}$$ 	
public'' 
ILogger'' 
Logger'' 
{'' 
get''  #
;''# $
set''% (
;''( )
}''* +
public** 
Task** 
Add** 
(** 
TRoot** 
[** 
]** 
	instances**  )
)**) *
{++ 	
Argument,, 
.,, 
NotNull,, 
(,, 
	instances,, &
,,,& '
nameof,,( .
(,,. /
	instances,,/ 8
),,8 9
),,9 :
;,,: ;
this.. 
... 
Logger.. 
... 
Verbose.. 
(..  
$"..  "
Adding .." )
{..) *
	instances..* 3
...3 4
Count..4 9
(..9 :
)..: ;
}..; <
 items of type ..< K
{..K L
typeof..L R
(..R S
TRoot..S X
)..X Y
}..Y Z
 using ..Z a
{..a b
this..b f
...f g
GetType..g n
(..n o
)..o p
...p q
Name..q u
}..u v
:..v w
{..w x
_context	..x Ä
.
..Ä Å
GetType
..Å à
(
..à â
)
..â ä
.
..ä ã
Name
..ã è
}
..è ê
.
..ê ë
"
..ë í
)
..í ì
;
..ì î
return00 
_context00 
.00 
Add00 
(00  
	instances00  )
)00) *
;00* +
}11 	
public44 
Task44 
Clear44 
(44 
)44 
{55 	
this66 
.66 
Logger66 
.66 
Verbose66 
(66  
$"66  "'
Clearing all items of type 66" =
{66= >
typeof66> D
(66D E
TRoot66E J
)66J K
}66K L
 using 66L S
{66S T
this66T X
.66X Y
GetType66Y `
(66` a
)66a b
.66b c
Name66c g
}66g h
:66h i
{66i j
_context66j r
.66r s
GetType66s z
(66z {
)66{ |
.66| }
Name	66} Å
}
66Å Ç
.
66Ç É
"
66É Ñ
)
66Ñ Ö
;
66Ö Ü
return88 
_context88 
.88 
Clear88 !
<88! "
TRoot88" '
>88' (
(88( )
)88) *
;88* +
}99 	
public<< 
Task<< 
<<< 
bool<< 
><< 
Exists<<  
(<<  !

Expression<<! +
<<<+ ,
Func<<, 0
<<<0 1
TRoot<<1 6
,<<6 7
bool<<8 <
><<< =
><<= >

expression<<? I
)<<I J
{== 	
Argument>> 
.>> 
NotNull>> 
(>> 

expression>> '
,>>' (
nameof>>) /
(>>/ 0

expression>>0 :
)>>: ;
)>>; <
;>>< =
this@@ 
.@@ 
Logger@@ 
.@@ 
Verbose@@ 
(@@  
$"@@  "-
!Checking to see if items of type @@" C
{@@C D
typeof@@D J
(@@J K
TRoot@@K P
)@@P Q
}@@Q R
 exist using @@R _
{@@_ `
this@@` d
.@@d e
GetType@@e l
(@@l m
)@@m n
.@@n o
Name@@o s
}@@s t
:@@t u
{@@u v
_context@@v ~
.@@~ 
GetType	@@ Ü
(
@@Ü á
)
@@á à
.
@@à â
Name
@@â ç
}
@@ç é
.
@@é è
"
@@è ê
)
@@ê ë
;
@@ë í
returnBB 
_contextBB 
.BB 
ExistsBB "
(BB" #

expressionBB# -
)BB- .
;BB. /
}CC 	
publicFF 
TaskFF 
<FF 
IEnumerableFF 
<FF  
TRootFF  %
>FF% &
>FF& '
FindFF( ,
(FF, -

ExpressionFF- 7
<FF7 8
FuncFF8 <
<FF< =
TRootFF= B
,FFB C
boolFFD H
>FFH I
>FFI J

expressionFFK U
)FFU V
{GG 	
ArgumentHH 
.HH 
NotNullHH 
(HH 

expressionHH '
,HH' (
nameofHH) /
(HH/ 0

expressionHH0 :
)HH: ;
)HH; <
;HH< =
thisJJ 
.JJ 
LoggerJJ 
.JJ 
VerboseJJ 
(JJ  
$"JJ  ""
Finding items of type JJ" 8
{JJ8 9
typeofJJ9 ?
(JJ? @
TRootJJ@ E
)JJE F
}JJF G
 using JJG N
{JJN O
thisJJO S
.JJS T
GetTypeJJT [
(JJ[ \
)JJ\ ]
.JJ] ^
NameJJ^ b
}JJb c
:JJc d
{JJd e
_contextJJe m
.JJm n
GetTypeJJn u
(JJu v
)JJv w
.JJw x
NameJJx |
}JJ| }
.JJ} ~
"JJ~ 
)	JJ Ä
;
JJÄ Å
returnLL 
_contextLL 
.LL 
FindLL  
(LL  !

expressionLL! +
)LL+ ,
;LL, -
}MM 	
publicTT 
TaskTT 
<TT 
TRootTT 
>TT 
FindTT 
(TT  
stringTT  &
idTT' )
)TT) *
{UU 	
thisVV 
.VV 
LoggerVV 
.VV 
VerboseVV 
(VV  
$"VV  "!
Finding item of type VV" 7
{VV7 8
typeofVV8 >
(VV> ?
TRootVV? D
)VVD E
}VVE F
	 with ID VVF O
{VVO P
idVVP R
}VVR S
 using VVS Z
{VVZ [
thisVV[ _
.VV_ `
GetTypeVV` g
(VVg h
)VVh i
.VVi j
NameVVj n
}VVn o
:VVo p
{VVp q
_contextVVq y
.VVy z
GetType	VVz Å
(
VVÅ Ç
)
VVÇ É
.
VVÉ Ñ
Name
VVÑ à
}
VVà â
.
VVâ ä
"
VVä ã
)
VVã å
;
VVå ç
returnXX 
_contextXX 
.XX 
FindXX  
<XX  !
TRootXX! &
>XX& '
(XX' (
idXX( *
)XX* +
;XX+ ,
}YY 	
public\\ 
Task\\ 
<\\ 
IEnumerable\\ 
<\\  
TRoot\\  %
>\\% &
>\\& '
Find\\( ,
(\\, -
)\\- .
{]] 	
this^^ 
.^^ 
Logger^^ 
.^^ 
Verbose^^ 
(^^  
$"^^  "&
Finding all items of type ^^" <
{^^< =
typeof^^= C
(^^C D
TRoot^^D I
)^^I J
}^^J K
 using ^^K R
{^^R S
this^^S W
.^^W X
GetType^^X _
(^^_ `
)^^` a
.^^a b
Name^^b f
}^^f g
:^^g h
{^^h i
_context^^i q
.^^q r
GetType^^r y
(^^y z
)^^z {
.^^{ |
Name	^^| Ä
}
^^Ä Å
.
^^Å Ç
"
^^Ç É
)
^^É Ñ
;
^^Ñ Ö
return`` 
_context`` 
.`` 
Find``  
<``  !
TRoot``! &
>``& '
(``' (
)``( )
;``) *
}aa 	
publicdd 
Taskdd 
Removedd 
(dd 
TRootdd  
[dd  !
]dd! "
	instancesdd# ,
)dd, -
{ee 	
Argumentff 
.ff 
NotNullff 
(ff 
	instancesff &
,ff& '
nameofff( .
(ff. /
	instancesff/ 8
)ff8 9
)ff9 :
;ff: ;
thishh 
.hh 
Loggerhh 
.hh 
Verbosehh 
(hh  
$"hh  "
	Removing hh" +
{hh+ ,
	instanceshh, 5
.hh5 6
Counthh6 ;
(hh; <
)hh< =
}hh= >
 items of type hh> M
{hhM N
typeofhhN T
(hhT U
TRoothhU Z
)hhZ [
}hh[ \
 using hh\ c
{hhc d
thishhd h
.hhh i
GetTypehhi p
(hhp q
)hhq r
.hhr s
Namehhs w
}hhw x
:hhx y
{hhy z
_context	hhz Ç
.
hhÇ É
GetType
hhÉ ä
(
hhä ã
)
hhã å
.
hhå ç
Name
hhç ë
}
hhë í
.
hhí ì
"
hhì î
)
hhî ï
;
hhï ñ
returnjj 
_contextjj 
.jj 
Removejj "
(jj" #
	instancesjj# ,
)jj, -
;jj- .
}kk 	
publicnn 
Tasknn 
Updatenn 
(nn 
TRootnn  
[nn  !
]nn! "
	instancesnn# ,
)nn, -
{oo 	
Argumentpp 
.pp 
NotNullpp 
(pp 
	instancespp &
,pp& '
nameofpp( .
(pp. /
	instancespp/ 8
)pp8 9
)pp9 :
;pp: ;
thisrr 
.rr 
Loggerrr 
.rr 
Verboserr 
(rr  
$"rr  "
	Updating rr" +
{rr+ ,
	instancesrr, 5
.rr5 6
Countrr6 ;
(rr; <
)rr< =
}rr= >
 items of type rr> M
{rrM N
typeofrrN T
(rrT U
TRootrrU Z
)rrZ [
}rr[ \
 using rr\ c
{rrc d
thisrrd h
.rrh i
GetTyperri p
(rrp q
)rrq r
.rrr s
Namerrs w
}rrw x
:rrx y
{rry z
_context	rrz Ç
.
rrÇ É
GetType
rrÉ ä
(
rrä ã
)
rrã å
.
rrå ç
Name
rrç ë
}
rrë í
.
rrí ì
"
rrì î
)
rrî ï
;
rrï ñ
returntt 
_contexttt 
.tt 
Updatett "
(tt" #
	instancestt# ,
)tt, -
;tt- .
}uu 	
publicxx 
Taskxx 
<xx 
boolxx 
>xx 
Existsxx  
(xx  !
stringxx! '
idxx( *
)xx* +
{yy 	
Argumentzz 
.zz 
NotNullOrWhiteSpacezz (
(zz( )
idzz) +
,zz+ ,
nameofzz- 3
(zz3 4
idzz4 6
)zz6 7
)zz7 8
;zz8 9
this|| 
.|| 
Logger|| 
.|| 
Verbose|| 
(||  
$"||  "-
!Checking to see if items of type ||" C
{||C D
typeof||D J
(||J K
TRoot||K P
)||P Q
}||Q R
 exist using ||R _
{||_ `
this||` d
.||d e
GetType||e l
(||l m
)||m n
.||n o
Name||o s
}||s t
:||t u
{||u v
_context||v ~
.||~ 
GetType	|| Ü
(
||Ü á
)
||á à
.
||à â
Name
||â ç
}
||ç é
.
||é è
"
||è ê
)
||ê ë
;
||ë í
return~~ 
_context~~ 
.~~ 
Exists~~ "
<~~" #
TRoot~~# (
>~~( )
(~~) *
id~~* ,
)~~, -
;~~- .
} 	
}
ÄÄ 
}ÅÅ „
PC:\Source\Stacks\Core\src\Slalom.Stacks\Domain\Serialization\ConceptConverter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
. 
Serialization ,
{ 
public 

class 
ConceptConverter !
:" #
JsonConverter$ 1
{ 
public 
override 
bool 

CanConvert '
(' (
Type( ,

objectType- 7
)7 8
{ 	
var 
typeInfo 
= 

objectType %
.% &
GetTypeInfo& 1
(1 2
)2 3
;3 4
return 
typeInfo 
. 
IsGenericType )
&&   
typeInfo   
.   $
GetGenericTypeDefinition   7
(  7 8
)  8 9
==  : <
typeof  = C
(  C D
	ConceptAs  D M
<  M N
>  N O
)  O P
;  P Q
}!! 	
public++ 
override++ 
object++ 
ReadJson++ '
(++' (

JsonReader++( 2
reader++3 9
,++9 :
Type++; ?

objectType++@ J
,++J K
object++L R
existingValue++S `
,++` a
JsonSerializer++b p

serializer++q {
)++{ |
{,, 	
var-- 
	valueType-- 
=-- 

objectType-- &
.--& '
GetTypeInfo--' 2
(--2 3
)--3 4
.--4 5
BaseType--5 =
.--= >
GetGenericArguments--> Q
(--Q R
)--R S
[--S T
$num--T U
]--U V
;--V W
var// 
value// 
=// 

serializer// "
.//" #
Deserialize//# .
(//. /
reader/// 5
,//5 6
	valueType//7 @
)//@ A
;//A B
var11 
instance11 
=11 
	Activator11 $
.11$ %
CreateInstance11% 3
(113 4

objectType114 >
)11> ?
;11? @

objectType33 
.33 
GetProperty33 "
(33" #
$str33# *
)33* +
.33+ ,
SetValue33, 4
(334 5
instance335 =
,33= >
value33? D
)33D E
;33E F
return55 
instance55 
;55 
}66 	
public>> 
override>> 
void>> 
	WriteJson>> &
(>>& '

JsonWriter>>' 1
writer>>2 8
,>>8 9
object>>: @
value>>A F
,>>F G
JsonSerializer>>H V

serializer>>W a
)>>a b
{?? 	
var@@ 
inner@@ 
=@@ 
value@@ 
.@@ 
GetType@@ %
(@@% &
)@@& '
.@@' (
GetProperty@@( 3
(@@3 4
$str@@4 ;
)@@; <
.@@< =
GetValue@@= E
(@@E F
value@@F K
)@@K L
;@@L M

serializerBB 
.BB 
	SerializeBB  
(BB  !
writerBB! '
,BB' (
innerBB) .
)BB. /
;BB/ 0
}CC 	
}DD 
}EE ®G
=C:\Source\Stacks\Core\src\Slalom.Stacks\Domain\ValueObject.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Domain 
{ 
public 

abstract 
class 
ValueObject %
<% &
T& '
>' (
:) *

IEquatable+ 5
<5 6
T6 7
>7 8
where9 >
T? @
:A B
ValueObjectC N
<N O
TO P
>P Q
{ 
private 
static 
IList 
< 
	FieldInfo &
>& '
_fields( /
=0 1
new2 5
List6 :
<: ;
	FieldInfo; D
>D E
(E F
)F G
;G H
public 
virtual 
bool 
Equals "
(" #
T# $
other% *
)* +
{ 	
if 
( 
other 
== 
null 
) 
{ 
return 
false 
; 
} 
var   
t   
=   
this   
.   
GetType    
(    !
)  ! "
;  " #
var!! 
	otherType!! 
=!! 
other!! !
.!!! "
GetType!!" )
(!!) *
)!!* +
;!!+ ,
if## 
(## 
t## 
!=## 
	otherType## 
)## 
{$$ 
return%% 
false%% 
;%% 
}&& 
var(( 
fields(( 
=(( 
this(( 
.(( 
	GetFields(( '
(((' (
)((( )
;(() *
foreach** 
(** 
var** 
field** 
in** !
fields**" (
)**( )
{++ 
var,, 
value1,, 
=,, 
field,, "
.,," #
GetValue,,# +
(,,+ ,
other,,, 1
),,1 2
;,,2 3
var-- 
value2-- 
=-- 
field-- "
.--" #
GetValue--# +
(--+ ,
this--, 0
)--0 1
;--1 2
if// 
(// 
value1// 
==// 
null// "
)//" #
{00 
if11 
(11 
value211 
!=11 !
null11" &
)11& '
{22 
return33 
false33 $
;33$ %
}44 
}55 
else66 
if66 
(66 
!66 
value166  
.66  !
Equals66! '
(66' (
value266( .
)66. /
)66/ 0
{77 
return88 
false88  
;88  !
}99 
}:: 
return<< 
true<< 
;<< 
}== 	
public@@ 
override@@ 
bool@@ 
Equals@@ #
(@@# $
object@@$ *
obj@@+ .
)@@. /
{AA 	
ifBB 
(BB 
objBB 
==BB 
nullBB 
)BB 
{CC 
returnDD 
falseDD 
;DD 
}EE 
varGG 
otherGG 
=GG 
objGG 
asGG 
TGG  
;GG  !
returnII 
thisII 
.II 
EqualsII 
(II 
otherII $
)II$ %
;II% &
}JJ 	
publicMM 
overrideMM 
intMM 
GetHashCodeMM '
(MM' (
)MM( )
{NN 	
varOO 
fieldsOO 
=OO 
thisOO 
.OO 
	GetFieldsOO '
(OO' (
)OO( )
.OO) *
SelectOO* 0
(OO0 1
fieldOO1 6
=>OO7 9
fieldOO: ?
.OO? @
GetValueOO@ H
(OOH I
thisOOI M
)OOM N
)OON O
.OOO P
WhereOOP U
(OOU V
valueOOV [
=>OO\ ^
valueOO_ d
!=OOe g
nullOOh l
)OOl m
.OOm n
ToListOOn t
(OOt u
)OOu v
;OOv w
fieldsPP 
.PP 
AddPP 
(PP 
thisPP 
.PP 
GetTypePP #
(PP# $
)PP$ %
)PP% &
;PP& '
returnQQ 
GetHashCodeQQ 
(QQ 
fieldsQQ %
.QQ% &
ToArrayQQ& -
(QQ- .
)QQ. /
)QQ/ 0
;QQ0 1
}RR 	
publicYY 
staticYY 
intYY 
GetHashCodeYY %
(YY% &
paramsYY& ,
objectYY- 3
[YY3 4
]YY4 5
objectsYY6 =
)YY= >
{ZZ 	
	unchecked[[ 
{\\ 
var]] 
hash]] 
=]] 
$num]] 
;]] 
foreach^^ 
(^^ 
var^^ 
item^^ !
in^^" $
objects^^% ,
)^^, -
{__ 
hash`` 
=`` 
hash`` 
*``  !
$num``" $
+``% &
item``' +
.``+ ,
GetHashCode``, 7
(``7 8
)``8 9
;``9 :
}aa 
returnbb 
hashbb 
;bb 
}cc 
}dd 	
publicmm 
staticmm 
boolmm 
operatormm #
==mm$ &
(mm& '
ValueObjectmm' 2
<mm2 3
Tmm3 4
>mm4 5
xmm6 7
,mm7 8
ValueObjectmm9 D
<mmD E
TmmE F
>mmF G
ymmH I
)mmI J
{nn 	
returnoo 
ReferenceEqualsoo "
(oo" #
xoo# $
,oo$ %
yoo& '
)oo' (
||oo) +
xoo, -
.oo- .
Equalsoo. 4
(oo4 5
yoo5 6
)oo6 7
;oo7 8
}pp 	
publicxx 
staticxx 
boolxx 
operatorxx #
!=xx$ &
(xx& '
ValueObjectxx' 2
<xx2 3
Txx3 4
>xx4 5
xxx6 7
,xx7 8
ValueObjectxx9 D
<xxD E
TxxE F
>xxF G
yxxH I
)xxI J
{yy 	
returnzz 
!zz 
(zz 
xzz 
==zz 
yzz 
)zz 
;zz 
}{{ 	
private}} 
IEnumerable}} 
<}} 
	FieldInfo}} %
>}}% & 
BuildFieldCollection}}' ;
(}}; <
)}}< =
{~~ 	
var 
t 
= 
typeof 
( 
T 
) 
; 
var
ÄÄ 
fields
ÄÄ 
=
ÄÄ 
new
ÄÄ 
List
ÄÄ !
<
ÄÄ! "
	FieldInfo
ÄÄ" +
>
ÄÄ+ ,
(
ÄÄ, -
)
ÄÄ- .
;
ÄÄ. /
while
ÇÇ 
(
ÇÇ 
t
ÇÇ 
!=
ÇÇ 
typeof
ÇÇ 
(
ÇÇ 
object
ÇÇ %
)
ÇÇ% &
)
ÇÇ& '
{
ÉÉ 
var
ÑÑ 
typeInfo
ÑÑ 
=
ÑÑ 
t
ÑÑ  
.
ÑÑ  !
GetTypeInfo
ÑÑ! ,
(
ÑÑ, -
)
ÑÑ- .
;
ÑÑ. /
fields
ÜÜ 
.
ÜÜ 
AddRange
ÜÜ 
(
ÜÜ  
typeInfo
ÜÜ  (
.
ÜÜ( )
	GetFields
ÜÜ) 2
(
ÜÜ2 3
BindingFlags
ÜÜ3 ?
.
ÜÜ? @
Public
ÜÜ@ F
|
ÜÜG H
BindingFlags
ÜÜI U
.
ÜÜU V
	NonPublic
ÜÜV _
|
ÜÜ` a
BindingFlags
ÜÜb n
.
ÜÜn o
Instance
ÜÜo w
)
ÜÜw x
)
ÜÜx y
;
ÜÜy z
var
áá 
fieldInfoCache
áá "
=
áá# $
typeInfo
áá% -
.
áá- .
GetField
áá. 6
(
áá6 7
$str
áá7 @
)
áá@ A
;
ááA B
fields
àà 
.
àà 
Remove
àà 
(
àà 
fieldInfoCache
àà ,
)
àà, -
;
àà- .
t
ââ 
=
ââ 
typeInfo
ââ 
.
ââ 
BaseType
ââ %
;
ââ% &
}
ää 
return
ãã 
fields
ãã 
;
ãã 
}
åå 	
private
éé 
IEnumerable
éé 
<
éé 
	FieldInfo
éé %
>
éé% &
	GetFields
éé' 0
(
éé0 1
)
éé1 2
{
èè 	
if
êê 
(
êê 
!
êê 
_fields
êê 
.
êê 
Any
êê 
(
êê 
)
êê 
)
êê 
{
ëë 
_fields
íí 
=
íí 
new
íí 
List
íí "
<
íí" #
	FieldInfo
íí# ,
>
íí, -
(
íí- .
this
íí. 2
.
íí2 3"
BuildFieldCollection
íí3 G
(
ííG H
)
ííH I
)
ííI J
;
ííJ K
}
ìì 
return
îî 
_fields
îî 
;
îî 
}
ïï 	
}
ññ 
}óó È`
BC:\Source\Stacks\Core\src\Slalom.Stacks\Logging\CompositeLogger.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Logging 
{ 
public 

class 
CompositeLogger  
:! "
ILogger# *
{ 
private 
readonly 
IComponentContext *
_components+ 6
;6 7
private 
readonly 
Application $
_environment% 1
;1 2
public 
CompositeLogger 
( 
IComponentContext 0

components1 ;
,; <
Application= H
environmentI T
)T U
{ 	
_components   
=   

components   $
;  $ %
_environment!! 
=!! 
environment!! &
;!!& '
}"" 	
public** 
void** 
Debug** 
(** 
	Exception** #
	exception**$ -
,**- .
string**/ 5
template**6 >
,**> ?
params**@ F
object**G M
[**M N
]**N O

properties**P Z
)**Z [
{++ 	
foreach,, 
(,, 
var,, 
logger,, 
in,,  "
this,,# '
.,,' (

GetLoggers,,( 2
(,,2 3
),,3 4
),,4 5
{-- 
logger.. 
... 
Debug.. 
(.. 
	exception.. &
,..& '
template..( 0
,..0 1
this..2 6
...6 7
CreateProperties..7 G
(..G H

properties..H R
)..R S
)..S T
;..T U
}// 
}00 	
public77 
void77 
Debug77 
(77 
string77  
template77! )
,77) *
params77+ 1
object772 8
[778 9
]779 :

properties77; E
)77E F
{88 	
foreach99 
(99 
var99 
logger99 
in99  "
this99# '
.99' (

GetLoggers99( 2
(992 3
)993 4
)994 5
{:: 
logger;; 
.;; 
Debug;; 
(;; 
template;; %
,;;% &
this;;' +
.;;+ ,
CreateProperties;;, <
(;;< =

properties;;= G
);;G H
);;H I
;;;I J
}<< 
}== 	
publicBB 
voidBB 
DisposeBB 
(BB 
)BB 
{CC 	
}DD 	
publicLL 
voidLL 
ErrorLL 
(LL 
	ExceptionLL #
	exceptionLL$ -
,LL- .
stringLL/ 5
templateLL6 >
,LL> ?
paramsLL@ F
objectLLG M
[LLM N
]LLN O

propertiesLLP Z
)LLZ [
{MM 	
foreachNN 
(NN 
varNN 
loggerNN 
inNN  "
thisNN# '
.NN' (

GetLoggersNN( 2
(NN2 3
)NN3 4
)NN4 5
{OO 
loggerPP 
.PP 
ErrorPP 
(PP 
	exceptionPP &
,PP& '
templatePP( 0
,PP0 1
thisPP2 6
.PP6 7
CreatePropertiesPP7 G
(PPG H

propertiesPPH R
)PPR S
)PPS T
;PPT U
}QQ 
}RR 	
publicYY 
voidYY 
ErrorYY 
(YY 
stringYY  
templateYY! )
,YY) *
paramsYY+ 1
objectYY2 8
[YY8 9
]YY9 :

propertiesYY; E
)YYE F
{ZZ 	
foreach[[ 
([[ 
var[[ 
logger[[ 
in[[  "
this[[# '
.[[' (

GetLoggers[[( 2
([[2 3
)[[3 4
)[[4 5
{\\ 
logger]] 
.]] 
Error]] 
(]] 
template]] %
,]]% &
this]]' +
.]]+ ,
CreateProperties]], <
(]]< =

properties]]= G
)]]G H
)]]H I
;]]I J
}^^ 
}__ 	
publicgg 
voidgg 
Fatalgg 
(gg 
	Exceptiongg #
	exceptiongg$ -
,gg- .
stringgg/ 5
templategg6 >
,gg> ?
paramsgg@ F
objectggG M
[ggM N
]ggN O

propertiesggP Z
)ggZ [
{hh 	
foreachii 
(ii 
varii 
loggerii 
inii  "
thisii# '
.ii' (

GetLoggersii( 2
(ii2 3
)ii3 4
)ii4 5
{jj 
loggerkk 
.kk 
Fatalkk 
(kk 
	exceptionkk &
,kk& '
templatekk( 0
,kk0 1
thiskk2 6
.kk6 7
CreatePropertieskk7 G
(kkG H

propertieskkH R
)kkR S
)kkS T
;kkT U
}ll 
}mm 	
publictt 
voidtt 
Fataltt 
(tt 
stringtt  
templatett! )
,tt) *
paramstt+ 1
objecttt2 8
[tt8 9
]tt9 :

propertiestt; E
)ttE F
{uu 	
foreachvv 
(vv 
varvv 
loggervv 
invv  "
thisvv# '
.vv' (

GetLoggersvv( 2
(vv2 3
)vv3 4
)vv4 5
{ww 
loggerxx 
.xx 
Fatalxx 
(xx 
templatexx %
,xx% &
thisxx' +
.xx+ ,
CreatePropertiesxx, <
(xx< =

propertiesxx= G
)xxG H
)xxH I
;xxI J
}yy 
}zz 	
public
ÇÇ 
void
ÇÇ 
Information
ÇÇ 
(
ÇÇ  
	Exception
ÇÇ  )
	exception
ÇÇ* 3
,
ÇÇ3 4
string
ÇÇ5 ;
template
ÇÇ< D
,
ÇÇD E
params
ÇÇF L
object
ÇÇM S
[
ÇÇS T
]
ÇÇT U

properties
ÇÇV `
)
ÇÇ` a
{
ÉÉ 	
foreach
ÑÑ 
(
ÑÑ 
var
ÑÑ 
logger
ÑÑ 
in
ÑÑ  "
this
ÑÑ# '
.
ÑÑ' (

GetLoggers
ÑÑ( 2
(
ÑÑ2 3
)
ÑÑ3 4
)
ÑÑ4 5
{
ÖÖ 
logger
ÜÜ 
.
ÜÜ 
Information
ÜÜ "
(
ÜÜ" #
	exception
ÜÜ# ,
,
ÜÜ, -
template
ÜÜ. 6
,
ÜÜ6 7
this
ÜÜ8 <
.
ÜÜ< =
CreateProperties
ÜÜ= M
(
ÜÜM N

properties
ÜÜN X
)
ÜÜX Y
)
ÜÜY Z
;
ÜÜZ [
}
áá 
}
àà 	
public
èè 
void
èè 
Information
èè 
(
èè  
string
èè  &
template
èè' /
,
èè/ 0
params
èè1 7
object
èè8 >
[
èè> ?
]
èè? @

properties
èèA K
)
èèK L
{
êê 	
foreach
ëë 
(
ëë 
var
ëë 
logger
ëë 
in
ëë  "
this
ëë# '
.
ëë' (

GetLoggers
ëë( 2
(
ëë2 3
)
ëë3 4
)
ëë4 5
{
íí 
logger
ìì 
.
ìì 
Information
ìì "
(
ìì" #
template
ìì# +
,
ìì+ ,
this
ìì- 1
.
ìì1 2
CreateProperties
ìì2 B
(
ììB C

properties
ììC M
)
ììM N
)
ììN O
;
ììO P
}
îî 
}
ïï 	
public
ùù 
void
ùù 
Verbose
ùù 
(
ùù 
	Exception
ùù %
	exception
ùù& /
,
ùù/ 0
string
ùù1 7
template
ùù8 @
,
ùù@ A
params
ùùB H
object
ùùI O
[
ùùO P
]
ùùP Q

properties
ùùR \
)
ùù\ ]
{
ûû 	
foreach
üü 
(
üü 
var
üü 
logger
üü 
in
üü  "
this
üü# '
.
üü' (

GetLoggers
üü( 2
(
üü2 3
)
üü3 4
)
üü4 5
{
†† 
logger
°° 
.
°° 
Verbose
°° 
(
°° 
	exception
°° (
,
°°( )
template
°°* 2
,
°°2 3
this
°°4 8
.
°°8 9
CreateProperties
°°9 I
(
°°I J

properties
°°J T
)
°°T U
)
°°U V
;
°°V W
}
¢¢ 
}
££ 	
public
™™ 
void
™™ 
Verbose
™™ 
(
™™ 
string
™™ "
template
™™# +
,
™™+ ,
params
™™- 3
object
™™4 :
[
™™: ;
]
™™; <

properties
™™= G
)
™™G H
{
´´ 	
foreach
¨¨ 
(
¨¨ 
var
¨¨ 
logger
¨¨ 
in
¨¨  "
this
¨¨# '
.
¨¨' (

GetLoggers
¨¨( 2
(
¨¨2 3
)
¨¨3 4
)
¨¨4 5
{
≠≠ 
logger
ÆÆ 
.
ÆÆ 
Verbose
ÆÆ 
(
ÆÆ 
template
ÆÆ '
,
ÆÆ' (
this
ÆÆ) -
.
ÆÆ- .
CreateProperties
ÆÆ. >
(
ÆÆ> ?

properties
ÆÆ? I
)
ÆÆI J
)
ÆÆJ K
;
ÆÆK L
}
ØØ 
}
∞∞ 	
public
∏∏ 
void
∏∏ 
Warning
∏∏ 
(
∏∏ 
	Exception
∏∏ %
	exception
∏∏& /
,
∏∏/ 0
string
∏∏1 7
template
∏∏8 @
,
∏∏@ A
params
∏∏B H
object
∏∏I O
[
∏∏O P
]
∏∏P Q

properties
∏∏R \
)
∏∏\ ]
{
ππ 	
foreach
∫∫ 
(
∫∫ 
var
∫∫ 
logger
∫∫ 
in
∫∫  "
this
∫∫# '
.
∫∫' (

GetLoggers
∫∫( 2
(
∫∫2 3
)
∫∫3 4
)
∫∫4 5
{
ªª 
logger
ºº 
.
ºº 
Warning
ºº 
(
ºº 
	exception
ºº (
,
ºº( )
template
ºº* 2
,
ºº2 3
this
ºº4 8
.
ºº8 9
CreateProperties
ºº9 I
(
ººI J

properties
ººJ T
)
ººT U
)
ººU V
;
ººV W
}
ΩΩ 
}
ææ 	
public
≈≈ 
void
≈≈ 
Warning
≈≈ 
(
≈≈ 
string
≈≈ "
template
≈≈# +
,
≈≈+ ,
params
≈≈- 3
object
≈≈4 :
[
≈≈: ;
]
≈≈; <

properties
≈≈= G
)
≈≈G H
{
∆∆ 	
foreach
«« 
(
«« 
var
«« 
logger
«« 
in
««  "
this
««# '
.
««' (

GetLoggers
««( 2
(
««2 3
)
««3 4
)
««4 5
{
»» 
logger
…… 
.
…… 
Warning
…… 
(
…… 
template
…… '
,
……' (
this
……) -
.
……- .
CreateProperties
……. >
(
……> ?

properties
……? I
)
……I J
)
……J K
;
……K L
}
   
}
ÀÀ 	
private
ÕÕ 
object
ÕÕ 
[
ÕÕ 
]
ÕÕ 
CreateProperties
ÕÕ )
(
ÕÕ) *
IEnumerable
ÕÕ* 5
<
ÕÕ5 6
object
ÕÕ6 <
>
ÕÕ< =
original
ÕÕ> F
)
ÕÕF G
{
ŒŒ 	
return
œœ 
original
œœ 
.
œœ 
Union
œœ !
(
œœ! "
new
œœ" %
[
œœ% &
]
œœ& '
{
–– 
_environment
——  
}
““ 
)
““ 
.
”” 
ToArray
”” 
(
”” 
)
”” 
;
”” 
}
‘‘ 	
private
÷÷ 
IEnumerable
÷÷ 
<
÷÷ 
ILogger
÷÷ #
>
÷÷# $

GetLoggers
÷÷% /
(
÷÷/ 0
)
÷÷0 1
{
◊◊ 	
return
ÿÿ 
_components
ÿÿ 
.
ÿÿ 

ResolveAll
ÿÿ )
<
ÿÿ) *
ILogger
ÿÿ* 1
>
ÿÿ1 2
(
ÿÿ2 3
)
ÿÿ3 4
.
ÿÿ4 5
ToList
ÿÿ5 ;
(
ÿÿ; <
)
ÿÿ< =
.
ÿÿ= >
Where
ÿÿ> C
(
ÿÿC D
e
ÿÿD E
=>
ÿÿF H
e
ÿÿI J
!=
ÿÿK M
this
ÿÿN R
)
ÿÿR S
;
ÿÿS T
}
ŸŸ 	
}
⁄⁄ 
}€€ Œ£
@C:\Source\Stacks\Core\src\Slalom.Stacks\Logging\ConsoleLogger.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Logging 
{ 
public 

class 
ConsoleLogger 
:  
ILogger! (
{ 
private 
static 
readonly 
string  &
	Separater' 0
=1 2
new3 6
string7 =
(= >
$char> A
,A B
$numC F
)F G
;G H
public 
void 
Dispose 
( 
) 
{ 	
} 	
public 
void 
Debug 
( 
	Exception #
	exception$ -
,- .
string/ 5
template6 >
,> ?
params@ F
objectG M
[M N
]N O

propertiesP Z
)Z [
{ 	
var 
builder 
= 
new 
StringBuilder +
(+ ,
), -
;- .
builder 
. 
AppendFormat  
(  !
$str! >
,> ?
$str@ G
,G H
DateTimeI Q
.Q R
UtcNowR X
,X Y
EnvironmentZ e
.e f"
CurrentManagedThreadIdf |
)| }
;} ~
builder   
.   

AppendLine   
(   
template   '
)  ' (
;  ( )
if!! 
(!! 
	exception!! 
!=!! 
null!! !
)!!! "
{"" 
builder## 
.## 

AppendLine## "
(##" #
$str### '
+##( )
	exception##* 3
)##3 4
;##4 5
}$$ 
builder%% 
.%% 

AppendLine%% 
(%% 
	Separater%% (
)%%( )
;%%) *
Console&& 
.&& 
ForegroundColor&& #
=&&$ %
ConsoleColor&&& 2
.&&2 3
Gray&&3 7
;&&7 8
Console'' 
.'' 
Write'' 
('' 
builder'' !
.''! "
ToString''" *
(''* +
)''+ ,
)'', -
;''- .
Console(( 
.(( 

ResetColor(( 
((( 
)((  
;((  !
})) 	
public,, 
void,, 
Debug,, 
(,, 
string,,  
template,,! )
,,,) *
params,,+ 1
object,,2 8
[,,8 9
],,9 :

properties,,; E
),,E F
{-- 	
var.. 
builder.. 
=.. 
new.. 
StringBuilder.. +
(..+ ,
).., -
;..- .
builder// 
.// 
AppendFormat//  
(//  !
$str//! >
,//> ?
$str//@ G
,//G H
DateTime//I Q
.//Q R
UtcNow//R X
,//X Y
Environment//Z e
.//e f"
CurrentManagedThreadId//f |
)//| }
;//} ~
builder00 
.00 

AppendLine00 
(00 
template00 '
)00' (
;00( )
builder11 
.11 

AppendLine11 
(11 
	Separater11 (
)11( )
;11) *
Console22 
.22 
ForegroundColor22 #
=22$ %
ConsoleColor22& 2
.222 3
Gray223 7
;227 8
Console33 
.33 
Write33 
(33 
builder33 !
.33! "
ToString33" *
(33* +
)33+ ,
)33, -
;33- .
Console44 
.44 

ResetColor44 
(44 
)44  
;44  !
}55 	
public88 
void88 
Error88 
(88 
	Exception88 #
	exception88$ -
,88- .
string88/ 5
template886 >
,88> ?
params88@ F
object88G M
[88M N
]88N O

properties88P Z
)88Z [
{99 	
var:: 
builder:: 
=:: 
new:: 
StringBuilder:: +
(::+ ,
)::, -
;::- .
builder;; 
.;; 
AppendFormat;;  
(;;  !
$str;;! >
,;;> ?
$str;;@ G
,;;G H
DateTime;;I Q
.;;Q R
UtcNow;;R X
,;;X Y
Environment;;Z e
.;;e f"
CurrentManagedThreadId;;f |
);;| }
;;;} ~
builder<< 
.<< 

AppendLine<< 
(<< 
template<< '
)<<' (
;<<( )
if== 
(== 
	exception== 
!=== 
null== !
)==! "
{>> 
builder?? 
.?? 

AppendLine?? "
(??" #
$str??# '
+??( )
	exception??* 3
)??3 4
;??4 5
}@@ 
builderAA 
.AA 

AppendLineAA 
(AA 
	SeparaterAA (
)AA( )
;AA) *
ConsoleBB 
.BB 
ForegroundColorBB #
=BB$ %
ConsoleColorBB& 2
.BB2 3
RedBB3 6
;BB6 7
ConsoleCC 
.CC 
WriteCC 
(CC 
builderCC !
.CC! "
ToStringCC" *
(CC* +
)CC+ ,
)CC, -
;CC- .
ConsoleDD 
.DD 

ResetColorDD 
(DD 
)DD  
;DD  !
}EE 	
publicHH 
voidHH 
ErrorHH 
(HH 
stringHH  
templateHH! )
,HH) *
paramsHH+ 1
objectHH2 8
[HH8 9
]HH9 :

propertiesHH; E
)HHE F
{II 	
varJJ 
builderJJ 
=JJ 
newJJ 
StringBuilderJJ +
(JJ+ ,
)JJ, -
;JJ- .
builderKK 
.KK 
AppendFormatKK  
(KK  !
$strKK! >
,KK> ?
$strKK@ G
,KKG H
DateTimeKKI Q
.KKQ R
UtcNowKKR X
,KKX Y
EnvironmentKKZ e
.KKe f"
CurrentManagedThreadIdKKf |
)KK| }
;KK} ~
builderLL 
.LL 

AppendLineLL 
(LL 
templateLL '
)LL' (
;LL( )
builderMM 
.MM 

AppendLineMM 
(MM 
	SeparaterMM (
)MM( )
;MM) *
ConsoleNN 
.NN 
ForegroundColorNN #
=NN$ %
ConsoleColorNN& 2
.NN2 3
RedNN3 6
;NN6 7
ConsoleOO 
.OO 
WriteOO 
(OO 
builderOO !
.OO! "
ToStringOO" *
(OO* +
)OO+ ,
)OO, -
;OO- .
ConsolePP 
.PP 

ResetColorPP 
(PP 
)PP  
;PP  !
}QQ 	
publicTT 
voidTT 
FatalTT 
(TT 
	ExceptionTT #
	exceptionTT$ -
,TT- .
stringTT/ 5
templateTT6 >
,TT> ?
paramsTT@ F
objectTTG M
[TTM N
]TTN O

propertiesTTP Z
)TTZ [
{UU 	
varVV 
builderVV 
=VV 
newVV 
StringBuilderVV +
(VV+ ,
)VV, -
;VV- .
builderWW 
.WW 
AppendFormatWW  
(WW  !
$strWW! >
,WW> ?
$strWW@ G
,WWG H
DateTimeWWI Q
.WWQ R
UtcNowWWR X
,WWX Y
EnvironmentWWZ e
.WWe f"
CurrentManagedThreadIdWWf |
)WW| }
;WW} ~
builderXX 
.XX 

AppendLineXX 
(XX 
templateXX '
)XX' (
;XX( )
ifYY 
(YY 
	exceptionYY 
!=YY 
nullYY !
)YY! "
{ZZ 
builder[[ 
.[[ 

AppendLine[[ "
([[" #
$str[[# '
+[[( )
	exception[[* 3
)[[3 4
;[[4 5
}\\ 
builder]] 
.]] 

AppendLine]] 
(]] 
	Separater]] (
)]]( )
;]]) *
Console^^ 
.^^ 
ForegroundColor^^ #
=^^$ %
ConsoleColor^^& 2
.^^2 3
Red^^3 6
;^^6 7
Console__ 
.__ 
Write__ 
(__ 
builder__ !
.__! "
ToString__" *
(__* +
)__+ ,
)__, -
;__- .
Console`` 
.`` 

ResetColor`` 
(`` 
)``  
;``  !
}aa 	
publicdd 
voiddd 
Fataldd 
(dd 
stringdd  
templatedd! )
,dd) *
paramsdd+ 1
objectdd2 8
[dd8 9
]dd9 :

propertiesdd; E
)ddE F
{ee 	
varff 
builderff 
=ff 
newff 
StringBuilderff +
(ff+ ,
)ff, -
;ff- .
buildergg 
.gg 
AppendFormatgg  
(gg  !
$strgg! >
,gg> ?
$strgg@ G
,ggG H
DateTimeggI Q
.ggQ R
UtcNowggR X
,ggX Y
EnvironmentggZ e
.gge f"
CurrentManagedThreadIdggf |
)gg| }
;gg} ~
builderhh 
.hh 

AppendLinehh 
(hh 
templatehh '
)hh' (
;hh( )
builderii 
.ii 

AppendLineii 
(ii 
	Separaterii (
)ii( )
;ii) *
Consolejj 
.jj 
ForegroundColorjj #
=jj$ %
ConsoleColorjj& 2
.jj2 3
Redjj3 6
;jj6 7
Consolekk 
.kk 
Writekk 
(kk 
builderkk !
.kk! "
ToStringkk" *
(kk* +
)kk+ ,
)kk, -
;kk- .
Consolell 
.ll 

ResetColorll 
(ll 
)ll  
;ll  !
}mm 	
publicpp 
voidpp 
Informationpp 
(pp  
	Exceptionpp  )
	exceptionpp* 3
,pp3 4
stringpp5 ;
templatepp< D
,ppD E
paramsppF L
objectppM S
[ppS T
]ppT U

propertiesppV `
)pp` a
{qq 	
varrr 
builderrr 
=rr 
newrr 
StringBuilderrr +
(rr+ ,
)rr, -
;rr- .
builderss 
.ss 
AppendFormatss  
(ss  !
$strss! >
,ss> ?
$strss@ F
,ssF G
DateTimessH P
.ssP Q
UtcNowssQ W
,ssW X
EnvironmentssY d
.ssd e"
CurrentManagedThreadIdsse {
)ss{ |
;ss| }
buildertt 
.tt 

AppendLinett 
(tt 
templatett '
)tt' (
;tt( )
ifuu 
(uu 
	exceptionuu 
!=uu 
nulluu !
)uu! "
{vv 
builderww 
.ww 

AppendLineww "
(ww" #
$strww# '
+ww( )
	exceptionww* 3
)ww3 4
;ww4 5
}xx 
builderyy 
.yy 

AppendLineyy 
(yy 
	Separateryy (
)yy( )
;yy) *
Consolezz 
.zz 
ForegroundColorzz #
=zz$ %
ConsoleColorzz& 2
.zz2 3
Whitezz3 8
;zz8 9
Console{{ 
.{{ 
Write{{ 
({{ 
builder{{ !
.{{! "
ToString{{" *
({{* +
){{+ ,
){{, -
;{{- .
Console|| 
.|| 

ResetColor|| 
(|| 
)||  
;||  !
}}} 	
public
ÄÄ 
void
ÄÄ 
Information
ÄÄ 
(
ÄÄ  
string
ÄÄ  &
template
ÄÄ' /
,
ÄÄ/ 0
params
ÄÄ1 7
object
ÄÄ8 >
[
ÄÄ> ?
]
ÄÄ? @

properties
ÄÄA K
)
ÄÄK L
{
ÅÅ 	
var
ÇÇ 
builder
ÇÇ 
=
ÇÇ 
new
ÇÇ 
StringBuilder
ÇÇ +
(
ÇÇ+ ,
)
ÇÇ, -
;
ÇÇ- .
builder
ÉÉ 
.
ÉÉ 
AppendFormat
ÉÉ  
(
ÉÉ  !
$str
ÉÉ! >
,
ÉÉ> ?
$str
ÉÉ@ F
,
ÉÉF G
DateTime
ÉÉH P
.
ÉÉP Q
UtcNow
ÉÉQ W
,
ÉÉW X
Environment
ÉÉY d
.
ÉÉd e$
CurrentManagedThreadId
ÉÉe {
)
ÉÉ{ |
;
ÉÉ| }
builder
ÑÑ 
.
ÑÑ 

AppendLine
ÑÑ 
(
ÑÑ 
template
ÑÑ '
)
ÑÑ' (
;
ÑÑ( )
builder
ÖÖ 
.
ÖÖ 

AppendLine
ÖÖ 
(
ÖÖ 
	Separater
ÖÖ (
)
ÖÖ( )
;
ÖÖ) *
Console
ÜÜ 
.
ÜÜ 
ForegroundColor
ÜÜ #
=
ÜÜ$ %
ConsoleColor
ÜÜ& 2
.
ÜÜ2 3
White
ÜÜ3 8
;
ÜÜ8 9
Console
áá 
.
áá 
Write
áá 
(
áá 
builder
áá !
.
áá! "
ToString
áá" *
(
áá* +
)
áá+ ,
)
áá, -
;
áá- .
Console
àà 
.
àà 

ResetColor
àà 
(
àà 
)
àà  
;
àà  !
}
ââ 	
public
åå 
void
åå 
Verbose
åå 
(
åå 
	Exception
åå %
	exception
åå& /
,
åå/ 0
string
åå1 7
template
åå8 @
,
åå@ A
params
ååB H
object
ååI O
[
ååO P
]
ååP Q

properties
ååR \
)
åå\ ]
{
çç 	
var
éé 
builder
éé 
=
éé 
new
éé 
StringBuilder
éé +
(
éé+ ,
)
éé, -
;
éé- .
builder
èè 
.
èè 
AppendFormat
èè  
(
èè  !
$str
èè! >
,
èè> ?
$str
èè@ I
,
èèI J
DateTime
èèK S
.
èèS T
UtcNow
èèT Z
,
èèZ [
Environment
èè\ g
.
èèg h$
CurrentManagedThreadId
èèh ~
)
èè~ 
;èè Ä
builder
êê 
.
êê 

AppendLine
êê 
(
êê 
template
êê '
)
êê' (
;
êê( )
if
ëë 
(
ëë 
	exception
ëë 
!=
ëë 
null
ëë !
)
ëë! "
{
íí 
builder
ìì 
.
ìì 

AppendLine
ìì "
(
ìì" #
$str
ìì# '
+
ìì( )
	exception
ìì* 3
)
ìì3 4
;
ìì4 5
}
îî 
builder
ïï 
.
ïï 

AppendLine
ïï 
(
ïï 
	Separater
ïï (
)
ïï( )
;
ïï) *
Console
ññ 
.
ññ 
ForegroundColor
ññ #
=
ññ$ %
ConsoleColor
ññ& 2
.
ññ2 3
DarkGray
ññ3 ;
;
ññ; <
Console
óó 
.
óó 
Write
óó 
(
óó 
builder
óó !
.
óó! "
ToString
óó" *
(
óó* +
)
óó+ ,
)
óó, -
;
óó- .
Console
òò 
.
òò 

ResetColor
òò 
(
òò 
)
òò  
;
òò  !
}
ôô 	
public
úú 
void
úú 
Verbose
úú 
(
úú 
string
úú "
template
úú# +
,
úú+ ,
params
úú- 3
object
úú4 :
[
úú: ;
]
úú; <

properties
úú= G
)
úúG H
{
ùù 	
var
ûû 
builder
ûû 
=
ûû 
new
ûû 
StringBuilder
ûû +
(
ûû+ ,
)
ûû, -
;
ûû- .
builder
üü 
.
üü 
AppendFormat
üü  
(
üü  !
$str
üü! >
,
üü> ?
$str
üü@ I
,
üüI J
DateTime
üüK S
.
üüS T
UtcNow
üüT Z
,
üüZ [
Environment
üü\ g
.
üüg h$
CurrentManagedThreadId
üüh ~
)
üü~ 
;üü Ä
builder
†† 
.
†† 

AppendLine
†† 
(
†† 
template
†† '
)
††' (
;
††( )
builder
°° 
.
°° 

AppendLine
°° 
(
°° 
	Separater
°° (
)
°°( )
;
°°) *
Console
¢¢ 
.
¢¢ 
ForegroundColor
¢¢ #
=
¢¢$ %
ConsoleColor
¢¢& 2
.
¢¢2 3
DarkGray
¢¢3 ;
;
¢¢; <
Console
££ 
.
££ 
Write
££ 
(
££ 
builder
££ !
.
££! "
ToString
££" *
(
££* +
)
££+ ,
)
££, -
;
££- .
Console
§§ 
.
§§ 

ResetColor
§§ 
(
§§ 
)
§§  
;
§§  !
}
•• 	
public
®® 
void
®® 
Warning
®® 
(
®® 
	Exception
®® %
	exception
®®& /
,
®®/ 0
string
®®1 7
template
®®8 @
,
®®@ A
params
®®B H
object
®®I O
[
®®O P
]
®®P Q

properties
®®R \
)
®®\ ]
{
©© 	
var
™™ 
builder
™™ 
=
™™ 
new
™™ 
StringBuilder
™™ +
(
™™+ ,
)
™™, -
;
™™- .
builder
´´ 
.
´´ 
AppendFormat
´´  
(
´´  !
$str
´´! >
,
´´> ?
$str
´´@ F
,
´´F G
DateTime
´´H P
.
´´P Q
UtcNow
´´Q W
,
´´W X
Environment
´´Y d
.
´´d e$
CurrentManagedThreadId
´´e {
)
´´{ |
;
´´| }
builder
¨¨ 
.
¨¨ 

AppendLine
¨¨ 
(
¨¨ 
template
¨¨ '
)
¨¨' (
;
¨¨( )
if
≠≠ 
(
≠≠ 
	exception
≠≠ 
!=
≠≠ 
null
≠≠ !
)
≠≠! "
{
ÆÆ 
builder
ØØ 
.
ØØ 

AppendLine
ØØ "
(
ØØ" #
$str
ØØ# '
+
ØØ( )
	exception
ØØ* 3
)
ØØ3 4
;
ØØ4 5
}
∞∞ 
builder
±± 
.
±± 

AppendLine
±± 
(
±± 
	Separater
±± (
)
±±( )
;
±±) *
Console
≤≤ 
.
≤≤ 
ForegroundColor
≤≤ #
=
≤≤$ %
ConsoleColor
≤≤& 2
.
≤≤2 3
Yellow
≤≤3 9
;
≤≤9 :
Console
≥≥ 
.
≥≥ 
Write
≥≥ 
(
≥≥ 
builder
≥≥ !
.
≥≥! "
ToString
≥≥" *
(
≥≥* +
)
≥≥+ ,
)
≥≥, -
;
≥≥- .
Console
¥¥ 
.
¥¥ 

ResetColor
¥¥ 
(
¥¥ 
)
¥¥  
;
¥¥  !
}
µµ 	
public
∏∏ 
void
∏∏ 
Warning
∏∏ 
(
∏∏ 
string
∏∏ "
template
∏∏# +
,
∏∏+ ,
params
∏∏- 3
object
∏∏4 :
[
∏∏: ;
]
∏∏; <

properties
∏∏= G
)
∏∏G H
{
ππ 	
var
∫∫ 
builder
∫∫ 
=
∫∫ 
new
∫∫ 
StringBuilder
∫∫ +
(
∫∫+ ,
)
∫∫, -
;
∫∫- .
builder
ªª 
.
ªª 
AppendFormat
ªª  
(
ªª  !
$str
ªª! >
,
ªª> ?
$str
ªª@ F
,
ªªF G
DateTime
ªªH P
.
ªªP Q
UtcNow
ªªQ W
,
ªªW X
Environment
ªªY d
.
ªªd e$
CurrentManagedThreadId
ªªe {
)
ªª{ |
;
ªª| }
builder
ºº 
.
ºº 

AppendLine
ºº 
(
ºº 
template
ºº '
)
ºº' (
;
ºº( )
builder
ΩΩ 
.
ΩΩ 

AppendLine
ΩΩ 
(
ΩΩ 
	Separater
ΩΩ (
)
ΩΩ( )
;
ΩΩ) *
Console
ææ 
.
ææ 
ForegroundColor
ææ #
=
ææ$ %
ConsoleColor
ææ& 2
.
ææ2 3
Yellow
ææ3 9
;
ææ9 :
Console
øø 
.
øø 
Write
øø 
(
øø 
builder
øø !
.
øø! "
ToString
øø" *
(
øø* +
)
øø+ ,
)
øø, -
;
øø- .
Console
¿¿ 
.
¿¿ 

ResetColor
¿¿ 
(
¿¿ 
)
¿¿  
;
¿¿  !
}
¡¡ 	
}
¬¬ 
}√√ ∫
:C:\Source\Stacks\Core\src\Slalom.Stacks\Logging\ILogger.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Logging

 
{ 
public 

	interface 
ILogger 
: 
IDisposable *
{ 
void 
Debug 
( 
	Exception 
	exception &
,& '
string( .
template/ 7
,7 8
params9 ?
object@ F
[F G
]G H

propertiesI S
)S T
;T U
void 
Debug 
( 
string 
template "
," #
params$ *
object+ 1
[1 2
]2 3

properties4 >
)> ?
;? @
void'' 
Error'' 
('' 
	Exception'' 
	exception'' &
,''& '
string''( .
template''/ 7
,''7 8
params''9 ?
object''@ F
[''F G
]''G H

properties''I S
)''S T
;''T U
void.. 
Error.. 
(.. 
string.. 
template.. "
,.." #
params..$ *
object..+ 1
[..1 2
]..2 3

properties..4 >
)..> ?
;..? @
void66 
Fatal66 
(66 
	Exception66 
	exception66 &
,66& '
string66( .
template66/ 7
,667 8
params669 ?
object66@ F
[66F G
]66G H

properties66I S
)66S T
;66T U
void== 
Fatal== 
(== 
string== 
template== "
,==" #
params==$ *
object==+ 1
[==1 2
]==2 3

properties==4 >
)==> ?
;==? @
voidEE 
InformationEE 
(EE 
	ExceptionEE "
	exceptionEE# ,
,EE, -
stringEE. 4
templateEE5 =
,EE= >
paramsEE? E
objectEEF L
[EEL M
]EEM N

propertiesEEO Y
)EEY Z
;EEZ [
voidLL 
InformationLL 
(LL 
stringLL 
templateLL  (
,LL( )
paramsLL* 0
objectLL1 7
[LL7 8
]LL8 9

propertiesLL: D
)LLD E
;LLE F
voidTT 
VerboseTT 
(TT 
	ExceptionTT 
	exceptionTT (
,TT( )
stringTT* 0
templateTT1 9
,TT9 :
paramsTT; A
objectTTB H
[TTH I
]TTI J

propertiesTTK U
)TTU V
;TTV W
void[[ 
Verbose[[ 
([[ 
string[[ 
template[[ $
,[[$ %
params[[& ,
object[[- 3
[[[3 4
][[4 5

properties[[6 @
)[[@ A
;[[A B
voidcc 
Warningcc 
(cc 
	Exceptioncc 
	exceptioncc (
,cc( )
stringcc* 0
templatecc1 9
,cc9 :
paramscc; A
objectccB H
[ccH I
]ccI J

propertiesccK U
)ccU V
;ccV W
voidjj 
Warningjj 
(jj 
stringjj 
templatejj $
,jj$ %
paramsjj& ,
objectjj- 3
[jj3 4
]jj4 5

propertiesjj6 @
)jj@ A
;jjA B
}kk 
}ll £
@C:\Source\Stacks\Core\src\Slalom.Stacks\Logging\LoggingModule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Logging

 
{ 
internal 
class 
LoggingModule  
:! "
Module# )
{ 
	protected 
override 
void 
Load  $
($ %
ContainerBuilder% 5
builder6 =
)= >
{ 	
base 
. 
Load 
( 
builder 
) 
; 
builder 
. 
RegisterType  
<  !
CompositeLogger! 0
>0 1
(1 2
)2 3
. #
AsImplementedInterfaces (
(( )
)) *
.   
SingleInstance   
(    
)    !
;  ! "
builder"" 
."" 
Register"" 
("" 
c"" 
=>"" !
new""" %
ConsoleLogger""& 3
(""3 4
)""4 5
)""5 6
.## $
PreserveExistingDefaults## )
(##) *
)##* +
.$$ 
SingleInstance$$ 
($$  
)$$  !
.%% 
As%% 
<%% 
ILogger%% 
>%% 
(%% 
)%% 
;%% 
}&& 	
}'' 
}(( ë
BC:\Source\Stacks\Core\src\Slalom.Stacks\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 
!
AssemblyConfiguration  
(  !
$str! #
)# $
]$ %
[		 
assembly		 	
:			 

AssemblyCompany		 
(		 
$str		 
)		 
]		 
[

 
assembly

 	
:

	 

AssemblyProduct

 
(

 
$str

 *
)

* +
]

+ ,
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8¢
>C:\Source\Stacks\Core\src\Slalom.Stacks\Reflection\Comments.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Reflection "
{ 
public 

class 
Comments 
{ 
public 
Comments 
( 
XNode 
node "
)" #
{ 	
this 
. 
ReadFromNode 
( 
node "
)" #
;# $
} 	
public 
Comments 
( 
string 
comments '
)' (
{   	
var!! 
node!! 
=!! 
XElement!! 
.!!  
Parse!!  %
(!!% &
comments!!& .
)!!. /
;!!/ 0
this"" 
."" 
ReadFromNode"" 
("" 
node"" "
)""" #
;""# $
}## 	
public)) 
string)) 
Summary)) 
{)) 
get))  #
;))# $
set))% (
;))( )
}))* +
public// 
string// 
Value// 
{// 
get// !
;//! "
set//# &
;//& '
}//( )
private11 
void11 
ReadFromNode11 !
(11! "
XNode11" '
node11( ,
)11, -
{22 	
this33 
.33 
Summary33 
=33 
node33 
.33  
XPathSelectElement33  2
(332 3
$str333 <
)33< =
?33= >
.33> ?
Value33? D
.33D E
Trim33E I
(33I J
)33J K
;33K L
this44 
.44 
Value44 
=44 
node44 
.44 
XPathSelectElement44 0
(440 1
$str441 8
)448 9
?449 :
.44: ;
Value44; @
.44@ A
Trim44A E
(44E F
)44F G
;44G H
}55 	
}66 
}77 ê-
FC:\Source\Stacks\Core\src\Slalom.Stacks\Reflection\DiscoveryService.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Reflection "
{ 
public 

class 
DiscoveryService !
:" #
IDiscoverTypes$ 2
{ 
private 
static 
readonly  
ConcurrentDictionary  4
<4 5
Type5 9
,9 :
List; ?
<? @
Type@ D
>D E
>E F
CacheG L
=M N
newO R 
ConcurrentDictionaryS g
<g h
Typeh l
,l m
Listn r
<r s
Types w
>w x
>x y
(y z
)z {
;{ |
internal 
static 
readonly  
string! '
[' (
]( )
Ignores* 1
=2 3
{4 5
$str5 <
,< =
$str> J
,J K
$strL Y
,Y Z
$str[ d
,d e
$strf m
}m n
;n o
private   
Lazy   
<   
List   
<   
Assembly   "
>  " #
>  # $
_assemblies  % 0
;  0 1
public&& 
DiscoveryService&& 
(&&  
ILogger&&  '
logger&&( .
)&&. /
{'' 	
Argument(( 
.(( 
NotNull(( 
((( 
logger(( #
,((# $
nameof((% +
(((+ ,
logger((, 2
)((2 3
)((3 4
;((4 5
this** 
.** !
CreateAssemblyFactory** &
(**& '
logger**' -
)**- .
;**. /
}++ 	
public22 
IEnumerable22 
<22 
Type22 
>22  
Find22! %
<22% &
TType22& +
>22+ ,
(22, -
)22- .
{33 	
return44 
Cache44 
.44 
GetOrAdd44 !
(44! "
typeof44" (
(44( )
TType44) .
)44. /
,44/ 0
t441 2
=>443 5
_assemblies446 A
.44A B
Value44B G
.44G H
SafelyGetTypes44H V
<44V W
TType44W \
>44\ ]
(44] ^
)44^ _
.44_ `
ToList44` f
(44f g
)44g h
)44h i
;44i j
}55 	
public;; 
IEnumerable;; 
<;; 
Type;; 
>;;  
Find;;! %
(;;% &
Type;;& *
type;;+ /
);;/ 0
{<< 	
return== 
Cache== 
.== 
GetOrAdd== !
(==! "
type==" &
,==& '
t==( )
=>==* ,
_assemblies==- 8
.==8 9
Value==9 >
.==> ?
SafelyGetTypes==? M
(==M N
type==N R
)==R S
.==S T
ToList==T Z
(==Z [
)==[ \
)==\ ]
;==] ^
}>> 	
publicDD 
IEnumerableDD 
<DD 
TypeDD 
>DD  
FindDD! %
(DD% &
)DD& '
{EE 	
returnFF 
_assembliesFF 
.FF 
ValueFF $
.FF$ %
SafelyGetTypesFF% 3
(FF3 4
)FF4 5
;FF5 6
}GG 	
privateII 
voidII !
CreateAssemblyFactoryII *
(II* +
ILoggerII+ 2
loggerII3 9
)II9 :
{JJ 	
_assembliesKK 
=KK 
newKK 
LazyKK "
<KK" #
ListKK# '
<KK' (
AssemblyKK( 0
>KK0 1
>KK1 2
(KK2 3
(KK3 4
)KK4 5
=>KK6 8
{LL 
varMM 

assembliesMM 
=MM  
newMM! $
ListMM% )
<MM) *
AssemblyMM* 2
>MM2 3
(MM3 4
)MM4 5
;MM5 6
varOO 
dependenciesOO  
=OO! "
DependencyContextOO# 4
.OO4 5
DefaultOO5 <
;OO< =
foreachPP 
(PP 
varPP 
compilationLibraryPP /
inPP0 2
dependenciesPP3 ?
.PP? @
RuntimeLibrariesPP@ P
)PPP Q
{QQ 
tryRR 
{SS 
ifTT 
(TT 
IgnoresTT #
.TT# $
AnyTT$ '
(TT' (
eTT( )
=>TT* ,
compilationLibraryTT- ?
.TT? @
NameTT@ D
.TTD E

StartsWithTTE O
(TTO P
eTTP Q
)TTQ R
)TTR S
)TTS T
{UU 
continueVV $
;VV$ %
}WW 
varYY 
assemblyNameYY (
=YY) *
newYY+ .
AssemblyNameYY/ ;
(YY; <
compilationLibraryYY< N
.YYN O
NameYYO S
)YYS T
;YYT U
var[[ 
assembly[[ $
=[[% &
Assembly[[' /
.[[/ 0
Load[[0 4
([[4 5
assemblyName[[5 A
)[[A B
;[[B C

assemblies]] "
.]]" #
Add]]# &
(]]& '
assembly]]' /
)]]/ 0
;]]0 1
}^^ 
catch__ 
{`` 
loggeraa 
?aa 
.aa  
Debugaa  %
(aa% &
$straa& X
,aaX Y
compilationLibraryaaZ l
.aal m
Nameaam q
)aaq r
;aar s
}bb 
}cc 
returnhh 

assemblieshh !
;hh! "
}ii 
)ii 
;ii 
}jj 	
}kk 
}ll ù
DC:\Source\Stacks\Core\src\Slalom.Stacks\Reflection\IDiscoverTypes.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Reflection "
{ 
public 

	interface 
IDiscoverTypes #
{ 
IEnumerable 
< 
Type 
> 
Find 
< 
TType $
>$ %
(% &
)& '
;' (
IEnumerable 
< 
Type 
> 
Find 
( 
Type #
type$ (
)( )
;) *
} 
} ¥
FC:\Source\Stacks\Core\src\Slalom.Stacks\Reflection\ReflectionModule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Reflection "
{ 
public 

class 
ReflectionModule !
:" #
Module$ *
{ 
private 
readonly 
Stack 
_stack %
;% &
public 
ReflectionModule 
(  
Stack  %
stack& +
)+ ,
{ 	
_stack 
= 
stack 
; 
} 	
	protected&& 
override&& 
void&& 
Load&&  $
(&&$ %
ContainerBuilder&&% 5
builder&&6 =
)&&= >
{'' 	
base(( 
.(( 
Load(( 
((( 
builder(( 
)(( 
;(( 
builder** 
.** 
Register** 
(** 
c** 
=>** !
new**" %
DiscoveryService**& 6
(**6 7
c**7 8
.**8 9
Resolve**9 @
<**@ A
ILogger**A H
>**H I
(**I J
)**J K
)**K L
)**L M
.**M N
AsSelf**N T
(**T U
)**U V
.**V W#
AsImplementedInterfaces**W n
(**n o
)**o p
;**p q
}++ 	
},, 
}-- Œç
DC:\Source\Stacks\Core\src\Slalom.Stacks\Reflection\TypeExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Reflection "
{ 
public 

static 
class 
TypeExtensions &
{ 
private 
static 
readonly  
ConcurrentDictionary  4
<4 5
Assembly5 =
,= >
Type? C
[C D
]D E
>E F
LoadedTypesG R
=S T
newU X 
ConcurrentDictionaryY m
<m n
Assemblyn v
,v w
Typex |
[| }
]} ~
>~ 
(	 Ä
)
Ä Å
;
Å Ç
private 
static 
readonly 
HashSet  '
<' (
Type( ,
>, -
PrimitiveTypes. <
== >
new? B
HashSetC J
<J K
TypeK O
>O P
{ 	
typeof 
( 
DateTime 
) 
, 
typeof 
( 
DateTimeOffset !
)! "
," #
typeof 
( 
TimeSpan 
) 
, 
typeof 
( 
string 
) 
, 
typeof 
( 
char 
) 
, 
typeof 
( 
decimal 
) 
, 
typeof 
( 
int 
) 
, 
typeof   
(   
uint   
)   
,   
typeof!! 
(!! 
short!! 
)!! 
,!! 
typeof"" 
("" 
ushort"" 
)"" 
,"" 
typeof## 
(## 
long## 
)## 
,## 
typeof$$ 
($$ 
ulong$$ 
)$$ 
,$$ 
typeof%% 
(%% 
Guid%% 
)%% 
}&& 	
;&&	 

public.. 
static.. 
IEnumerable.. !
<..! "
T.." #
>..# $
GetAllAttributes..% 5
<..5 6
T..6 7
>..7 8
(..8 9
this..9 =
Type..> B
type..C G
)..G H
where..I N
T..O P
:..Q R
	Attribute..S \
{// 	
var00 
target00 
=00 
new00 
List00 !
<00! "
T00" #
>00# $
(00$ %
)00% &
;00& '
do11 
{22 
target33 
.33 
AddRange33 
(33  
type33  $
.33$ %
GetTypeInfo33% 0
(330 1
)331 2
.332 3
GetCustomAttributes333 F
<33F G
T33G H
>33H I
(33I J
)33J K
)33K L
;33L M
target44 
.44 
AddRange44 
(44  
type44  $
.44$ %
GetInterfaces44% 2
(442 3
)443 4
.444 5

SelectMany445 ?
(44? @
e44@ A
=>44B D
e44E F
.44F G
GetAllAttributes44G W
<44W X
T44X Y
>44Y Z
(44Z [
)44[ \
)44\ ]
)44] ^
;44^ _
type55 
=55 
type55 
.55 
GetTypeInfo55 '
(55' (
)55( )
.55) *
BaseType55* 2
;552 3
}66 
while77 
(77 
type77 
!=77 
null77 
)77  
;77  !
return99 
target99 
.99 
AsEnumerable99 &
(99& '
)99' (
;99( )
}:: 	
publicBB 
staticBB 
IEnumerableBB !
<BB! "
TBB" #
>BB# $
GetAllAttributesBB% 5
<BB5 6
TBB6 7
>BB7 8
(BB8 9
thisBB9 =

MethodInfoBB> H
methodBBI O
)BBO P
whereBBQ V
TBBW X
:BBY Z
	AttributeBB[ d
{CC 	
varDD 
targetDD 
=DD 
newDD 
ListDD !
<DD! "
TDD" #
>DD# $
(DD$ %
)DD% &
;DD& '
targetEE 
.EE 
AddRangeEE 
(EE 
methodEE "
.EE" #
GetCustomAttributesEE# 6
<EE6 7
TEE7 8
>EE8 9
(EE9 :
)EE: ;
)EE; <
;EE< =
returnFF 
targetFF 
.FF 
AsEnumerableFF &
(FF& '
)FF' (
;FF( )
}GG 	
publicNN 
staticNN 
IEnumerableNN !
<NN! "
TypeNN" &
>NN& '#
GetBaseAndContractTypesNN( ?
(NN? @
thisNN@ D
TypeNNE I
typeNNJ N
)NNN O
{OO 	
returnPP 
typePP 
.PP 
GetBaseTypesPP $
(PP$ %
)PP% &
.PP& '
ConcatPP' -
(PP- .
typePP. 2
.PP2 3
GetInterfacesPP3 @
(PP@ A
)PPA B
)PPB C
.PPC D

SelectManyPPD N
(PPN O
GetTypeAndGenericPPO `
)PP` a
.PPa b
WherePPb g
(PPg h
tPPh i
=>PPj l
tPPm n
!=PPo q
typePPr v
&&PPw y
tPPz {
!=PP| ~
typeof	PP Ö
(
PPÖ Ü
object
PPÜ å
)
PPå ç
)
PPç é
;
PPé è
}QQ 	
publicXX 
staticXX 
IEnumerableXX !
<XX! "
TypeXX" &
>XX& '
GetBaseTypesXX( 4
(XX4 5
thisXX5 9
TypeXX: >
typeXX? C
)XXC D
{YY 	
varZZ 
currentTypeZZ 
=ZZ 
typeZZ "
;ZZ" #
while[[ 
([[ 
currentType[[ 
!=[[ !
null[[" &
)[[& '
{\\ 
yield]] 
return]] 
currentType]] (
;]]( )
currentType^^ 
=^^ 
currentType^^ )
.^^) *
GetTypeInfo^^* 5
(^^5 6
)^^6 7
.^^7 8
BaseType^^8 @
;^^@ A
}__ 
}`` 	
publicgg 
staticgg 
IEnumerablegg !
<gg! "
PropertyInfogg" .
>gg. /"
GetPropertiesRecursivegg0 F
(ggF G
thisggG K
TypeggL P
typeggQ U
)ggU V
{hh 	
varii 
	seenNamesii 
=ii 
newii 
HashSetii  '
<ii' (
stringii( .
>ii. /
(ii/ 0
)ii0 1
;ii1 2
varkk 
currentTypeInfokk 
=kk  !
typekk" &
.kk& '
GetTypeInfokk' 2
(kk2 3
)kk3 4
;kk4 5
whilemm 
(mm 
currentTypeInfomm "
.mm" #
AsTypemm# )
(mm) *
)mm* +
!=mm, .
typeofmm/ 5
(mm5 6
objectmm6 <
)mm< =
)mm= >
{nn 
varoo 
unseenPropertiesoo $
=oo% &
currentTypeInfooo' 6
.oo6 7
DeclaredPropertiesoo7 I
.ooI J
WhereooJ O
(ooO P
pooP Q
=>ooR T
pooU V
.ooV W
CanReadooW ^
&&oo_ a
pppU V
.ppV W
	GetMethodppW `
.pp` a
IsPublicppa i
&&ppj l
!qqU V
pqqV W
.qqW X
	GetMethodqqX a
.qqa b
IsStaticqqb j
&&qqk m
(rrU V
prrV W
.rrW X
NamerrX \
!=rr] _
$strrr` f
||rrg i
prrj k
.rrk l
GetIndexParametersrrl ~
(rr~ 
)	rr Ä
.
rrÄ Å
Length
rrÅ á
==
rrà ä
$num
rrã å
)
rrå ç
&&
rré ê
!ssU V
	seenNamesssV _
.ss_ `
Containsss` h
(ssh i
pssi j
.ssj k
Namessk o
)sso p
)ssp q
;ssq r
foreachuu 
(uu 
varuu 
propertyInfouu )
inuu* ,
unseenPropertiesuu- =
)uu= >
{vv 
	seenNamesww 
.ww 
Addww !
(ww! "
propertyInfoww" .
.ww. /
Nameww/ 3
)ww3 4
;ww4 5
yieldxx 
returnxx  
propertyInfoxx! -
;xx- .
}yy 
currentTypeInfo{{ 
={{  !
currentTypeInfo{{" 1
.{{1 2
BaseType{{2 :
.{{: ;
GetTypeInfo{{; F
({{F G
){{G H
;{{H I
}|| 
}}} 	
public
ÜÜ 
static
ÜÜ 
bool
ÜÜ 
IsDictionary
ÜÜ '
(
ÜÜ' (
this
ÜÜ( ,
Type
ÜÜ- 1
instance
ÜÜ2 :
)
ÜÜ: ;
{
áá 	
return
àà 
instance
àà 
.
àà 
GetInterfaces
àà )
(
àà) *
)
àà* +
.
àà+ ,
Any
àà, /
(
àà/ 0
e
àà0 1
=>
àà2 4
e
àà5 6
.
àà6 7
GetTypeInfo
àà7 B
(
ààB C
)
ààC D
.
ààD E
IsGenericType
ààE R
&&
ààS U
e
ààV W
.
ààW X&
GetGenericTypeDefinition
ààX p
(
ààp q
)
ààq r
==
ààs u
typeof
ààv |
(
àà| }
IDictionaryàà} à
<ààà â
,ààâ ä
>ààä ã
)ààã å
)ààå ç
;ààç é
}
ââ 	
public
íí 
static
íí 
bool
íí 

IsNullable
íí %
(
íí% &
this
íí& *
Type
íí+ /
instance
íí0 8
)
íí8 9
{
ìì 	
return
îî 
instance
îî 
.
îî 
GetTypeInfo
îî '
(
îî' (
)
îî( )
.
îî) *
IsGenericType
îî* 7
&&
îî8 :
instance
îî; C
.
îîC D&
GetGenericTypeDefinition
îîD \
(
îî\ ]
)
îî] ^
==
îî_ a
typeof
îîb h
(
îîh i
Nullable
îîi q
<
îîq r
>
îîr s
)
îîs t
;
îît u
}
ïï 	
public
ûû 
static
ûû 
bool
ûû 
IsPrimitive
ûû &
(
ûû& '
this
ûû' +
Type
ûû, 0
instance
ûû1 9
)
ûû9 :
{
üü 	
return
†† 
instance
†† 
.
†† 
GetTypeInfo
†† '
(
††' (
)
††( )
.
††) *
IsPrimitive
††* 5
||
††6 8
PrimitiveTypes
††9 G
.
††G H
Contains
††H P
(
††P Q
instance
††Q Y
)
††Y Z
;
††Z [
}
°° 	
public
®® 
static
®® 
Type
®® 
[
®® 
]
®® 
SafelyGetTypes
®® +
<
®®+ ,
T
®®, -
>
®®- .
(
®®. /
this
®®/ 3
IEnumerable
®®4 ?
<
®®? @
Assembly
®®@ H
>
®®H I

assemblies
®®J T
)
®®T U
{
©© 	
return
™™ 
SafelyGetTypes
™™ !
(
™™! "

assemblies
™™" ,
,
™™, -
typeof
™™. 4
(
™™4 5
T
™™5 6
)
™™6 7
)
™™7 8
;
™™8 9
}
´´ 	
public
≥≥ 
static
≥≥ 
Type
≥≥ 
[
≥≥ 
]
≥≥ 
SafelyGetTypes
≥≥ +
(
≥≥+ ,
this
≥≥, 0
IEnumerable
≥≥1 <
<
≥≥< =
Assembly
≥≥= E
>
≥≥E F

assemblies
≥≥G Q
,
≥≥Q R
Type
≥≥S W
type
≥≥X \
)
≥≥\ ]
{
¥¥ 	
if
µµ 
(
µµ 
type
µµ 
.
µµ 
GetTypeInfo
µµ  
(
µµ  !
)
µµ! "
.
µµ" #%
IsGenericTypeDefinition
µµ# :
)
µµ: ;
{
∂∂ 
return
∑∑ 

assemblies
∑∑ !
.
∑∑! "

SelectMany
∑∑" ,
(
∑∑, -
e
∑∑- .
=>
∑∑/ 1
e
∑∑2 3
.
∑∑3 4
SafelyGetTypes
∑∑4 B
(
∑∑B C
)
∑∑C D
)
∑∑D E
.
∑∑E F
Where
∑∑F K
(
∑∑K L
e
∑∑L M
=>
∑∑N P
e
∑∑Q R
.
∑∑R S%
GetBaseAndContractTypes
∑∑S j
(
∑∑j k
)
∑∑k l
.
∑∑l m
Any
∑∑m p
(
∑∑p q
x
∑∑q r
=>
∑∑s u
x
∑∑v w
.
∑∑w x
GetTypeInfo∑∑x É
(∑∑É Ñ
)∑∑Ñ Ö
.∑∑Ö Ü
IsGenericType∑∑Ü ì
&&∑∑î ñ
x∑∑ó ò
.∑∑ò ô(
GetGenericTypeDefinition∑∑ô ±
(∑∑± ≤
)∑∑≤ ≥
==∑∑¥ ∂
type∑∑∑ ª
)∑∑ª º
)∑∑º Ω
.∑∑Ω æ
ToArray∑∑æ ≈
(∑∑≈ ∆
)∑∑∆ «
;∑∑« »
}
∏∏ 
return
ππ 

assemblies
ππ 
.
ππ 

SelectMany
ππ (
(
ππ( )
e
ππ) *
=>
ππ+ -
e
ππ. /
.
ππ/ 0
SafelyGetTypes
ππ0 >
(
ππ> ?
)
ππ? @
)
ππ@ A
.
ππA B
Where
ππB G
(
ππG H
e
ππH I
=>
ππJ L
e
ππM N
!=
ππO Q
null
ππR V
&&
ππW Y
type
ππZ ^
.
ππ^ _
IsAssignableFrom
ππ_ o
(
ππo p
e
ππp q
)
ππq r
)
ππr s
.
ππs t
ToArray
ππt {
(
ππ{ |
)
ππ| }
;
ππ} ~
}
∫∫ 	
public
¡¡ 
static
¡¡ 
Type
¡¡ 
[
¡¡ 
]
¡¡ 
SafelyGetTypes
¡¡ +
(
¡¡+ ,
this
¡¡, 0
IEnumerable
¡¡1 <
<
¡¡< =
Assembly
¡¡= E
>
¡¡E F

assemblies
¡¡G Q
)
¡¡Q R
{
¬¬ 	
return
√√ 

assemblies
√√ 
.
√√ 

SelectMany
√√ (
(
√√( )
e
√√) *
=>
√√+ -
e
√√. /
.
√√/ 0
SafelyGetTypes
√√0 >
(
√√> ?
)
√√? @
)
√√@ A
.
√√A B
ToArray
√√B I
(
√√I J
)
√√J K
;
√√K L
}
ƒƒ 	
public
ÀÀ 
static
ÀÀ 
Type
ÀÀ 
[
ÀÀ 
]
ÀÀ 
SafelyGetTypes
ÀÀ +
(
ÀÀ+ ,
this
ÀÀ, 0
Assembly
ÀÀ1 9
assembly
ÀÀ: B
)
ÀÀB C
{
ÃÃ 	
return
ÕÕ 
LoadedTypes
ÕÕ 
.
ÕÕ 
GetOrAdd
ÕÕ '
(
ÕÕ' (
assembly
ÕÕ( 0
,
ÕÕ0 1
a
ÕÕ2 3
=>
ÕÕ4 6
{
ŒŒ 
try
œœ 
{
–– 
return
—— 
assembly
—— #
.
——# $
GetTypes
——$ ,
(
——, -
)
——- .
.
——. /
Where
——/ 4
(
——4 5
e
——5 6
=>
——7 9
e
——: ;
!=
——< >
null
——? C
)
——C D
.
——D E
ToArray
——E L
(
——L M
)
——M N
;
——N O
}
““ 
catch
”” 
(
”” )
ReflectionTypeLoadException
”” 2
	exception
””3 <
)
””< =
{
‘‘ 
return
’’ 
	exception
’’ $
.
’’$ %
Types
’’% *
.
’’* +
Where
’’+ 0
(
’’0 1
x
’’1 2
=>
’’3 5
x
’’6 7
!=
’’8 :
null
’’; ?
)
’’? @
.
’’@ A
ToArray
’’A H
(
’’H I
)
’’I J
;
’’J K
}
÷÷ 
catch
◊◊ 
{
ÿÿ 
return
ŸŸ 
new
ŸŸ 
Type
ŸŸ #
[
ŸŸ# $
$num
ŸŸ$ %
]
ŸŸ% &
;
ŸŸ& '
}
⁄⁄ 
}
€€ 
)
€€ 
;
€€ 
}
‹‹ 	
public
‰‰ 
static
‰‰ 
Type
‰‰ 
[
‰‰ 
]
‰‰ 
SafelyGetTypes
‰‰ +
(
‰‰+ ,
this
‰‰, 0
Assembly
‰‰1 9
assembly
‰‰: B
,
‰‰B C
Type
‰‰D H
type
‰‰I M
)
‰‰M N
{
ÂÂ 	
return
ÊÊ 
assembly
ÊÊ 
.
ÊÊ 
SafelyGetTypes
ÊÊ *
(
ÊÊ* +
)
ÊÊ+ ,
.
ÊÊ, -
Where
ÊÊ- 2
(
ÊÊ2 3
e
ÊÊ3 4
=>
ÊÊ5 7
e
ÊÊ8 9
!=
ÊÊ: <
null
ÊÊ= A
&&
ÊÊB D
type
ÊÊE I
.
ÊÊI J
IsAssignableFrom
ÊÊJ Z
(
ÊÊZ [
e
ÊÊ[ \
)
ÊÊ\ ]
)
ÊÊ] ^
.
ÊÊ^ _
ToArray
ÊÊ_ f
(
ÊÊf g
)
ÊÊg h
;
ÊÊh i
}
ÁÁ 	
private
ÈÈ 
static
ÈÈ 
IEnumerable
ÈÈ "
<
ÈÈ" #
Type
ÈÈ# '
>
ÈÈ' (
GetTypeAndGeneric
ÈÈ) :
(
ÈÈ: ;
Type
ÈÈ; ?
type
ÈÈ@ D
)
ÈÈD E
{
ÍÍ 	
yield
ÎÎ 
return
ÎÎ 
type
ÎÎ 
;
ÎÎ 
if
ÏÏ 
(
ÏÏ 
type
ÏÏ 
.
ÏÏ 
GetTypeInfo
ÏÏ  
(
ÏÏ  !
)
ÏÏ! "
.
ÏÏ" #
IsGenericType
ÏÏ# 0
&&
ÏÏ1 3
!
ÏÏ4 5
type
ÏÏ5 9
.
ÏÏ9 :
GetTypeInfo
ÏÏ: E
(
ÏÏE F
)
ÏÏF G
.
ÏÏG H'
ContainsGenericParameters
ÏÏH a
)
ÏÏa b
{
ÌÌ 
yield
ÓÓ 
return
ÓÓ 
type
ÓÓ !
.
ÓÓ! "&
GetGenericTypeDefinition
ÓÓ" :
(
ÓÓ: ;
)
ÓÓ; <
;
ÓÓ< =
}
ÔÔ 
}
 	
}
ÒÒ 
}ÚÚ Î
?C:\Source\Stacks\Core\src\Slalom.Stacks\Search\IEntityReader.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Search

 
{ 
public 

	interface 
IEntityReader "
<" #
TEntity# *
>* +
{ 

IQueryable 
< 
TEntity 
> 
Read  
(  !
)! "
;" #
} 
} ŸR
GC:\Source\Stacks\Core\src\Slalom.Stacks\Search\InMemorySearchContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

class !
InMemorySearchContext &
:' (
ISearchContext) 7
{ 
private 
readonly  
ReaderWriterLockSlim -

_cacheLock. 8
=9 :
new; > 
ReaderWriterLockSlim? S
(S T
)T U
;U V
private 
readonly 
List 
< 
ISearchResult +
>+ ,

_instances- 7
=8 9
new: =
List> B
<B C
ISearchResultC P
>P Q
(Q R
)R S
;S T
private 
int 
_index 
; 
public'' 
Task'' 
AddAsync'' 
<'' 
TSearchResult'' *
>''* +
(''+ ,
TSearchResult'', 9
[''9 :
]'': ;
	instances''< E
)''E F
where''G L
TSearchResult''M Z
:''[ \
class''] b
,''b c
ISearchResult''d q
{(( 	
Argument)) 
.)) 
NotNull)) 
()) 
	instances)) &
,))& '
nameof))( .
()). /
	instances))/ 8
)))8 9
)))9 :
;)): ;

_cacheLock++ 
.++ 
EnterWriteLock++ %
(++% &
)++& '
;++' (
try,, 
{-- 
foreach.. 
(.. 
var.. 
instance.. %
in..& (
	instances..) 2
)..2 3
{// 
instance00 
.00 
Id00 
=00  !
_index00" (
++00( *
;00* +

_instances11 
.11 
Add11 "
(11" #
instance11# +
)11+ ,
;11, -
}22 
}33 
finally44 
{55 

_cacheLock66 
.66 
ExitWriteLock66 (
(66( )
)66) *
;66* +
}77 
return88 
Task88 
.88 

FromResult88 "
(88" #
$num88# $
)88$ %
;88% &
}99 	
public@@ 
Task@@ 

ClearAsync@@ 
<@@ 
TSearchResult@@ ,
>@@, -
(@@- .
)@@. /
where@@0 5
TSearchResult@@6 C
:@@D E
class@@F K
,@@K L
ISearchResult@@M Z
{AA 	

_cacheLockBB 
.BB 
EnterWriteLockBB %
(BB% &
)BB& '
;BB' (
tryCC 
{DD 

_instancesEE 
.EE 
	RemoveAllEE $
(EE$ %
eEE% &
=>EE' )
eEE* +
isEE, .
TSearchResultEE/ <
)EE< =
;EE= >
}FF 
finallyGG 
{HH 

_cacheLockII 
.II 
ExitWriteLockII (
(II( )
)II) *
;II* +
}JJ 
returnKK 
TaskKK 
.KK 

FromResultKK "
(KK" #
$numKK# $
)KK$ %
;KK% &
}LL 	
publicTT 

IQueryableTT 
<TT 
TSearchResultTT '
>TT' (
SearchTT) /
<TT/ 0
TSearchResultTT0 =
>TT= >
(TT> ?
stringTT? E
textTTF J
=TTK L
nullTTM Q
)TTQ R
whereTTS X
TSearchResultTTY f
:TTg h
classTTi n
,TTn o
ISearchResultTTp }
{UU 	

_cacheLockVV 
.VV 
EnterReadLockVV $
(VV$ %
)VV% &
;VV& '
tryWW 
{XX 
ifYY 
(YY 
!YY 
stringYY 
.YY 
IsNullOrWhiteSpaceYY .
(YY. /
textYY/ 3
)YY3 4
)YY4 5
{ZZ 
return[[ 

_instances[[ %
.[[% &
OfType[[& ,
<[[, -
TSearchResult[[- :
>[[: ;
([[; <
)[[< =
.[[= >
AsQueryable[[> I
([[I J
)[[J K
.[[K L
Contains[[L T
([[T U
text[[U Y
)[[Y Z
;[[Z [
}\\ 
return]] 

_instances]] !
.]]! "
OfType]]" (
<]]( )
TSearchResult]]) 6
>]]6 7
(]]7 8
)]]8 9
.]]9 :
AsQueryable]]: E
(]]E F
)]]F G
;]]G H
}^^ 
finally__ 
{`` 

_cacheLockaa 
.aa 
ExitReadLockaa '
(aa' (
)aa( )
;aa) *
}bb 
}cc 	
publickk 
Taskkk 
RemoveAsynckk 
<kk  
TSearchResultkk  -
>kk- .
(kk. /
TSearchResultkk/ <
[kk< =
]kk= >
	instanceskk? H
)kkH I
wherekkJ O
TSearchResultkkP ]
:kk^ _
classkk` e
,kke f
ISearchResultkkg t
{ll 	

_cacheLockmm 
.mm 
EnterWriteLockmm %
(mm% &
)mm& '
;mm' (
trynn 
{oo 
varpp 
idspp 
=pp 
	instancespp #
.pp# $
Selectpp$ *
(pp* +
epp+ ,
=>pp- /
epp0 1
.pp1 2
Idpp2 4
)pp4 5
.pp5 6
ToListpp6 <
(pp< =
)pp= >
;pp> ?

_instancesqq 
.qq 
	RemoveAllqq $
(qq$ %
eqq% &
=>qq' )
idsqq* -
.qq- .
Containsqq. 6
(qq6 7
eqq7 8
.qq8 9
Idqq9 ;
)qq; <
)qq< =
;qq= >
}rr 
finallyss 
{tt 

_cacheLockuu 
.uu 
ExitWriteLockuu (
(uu( )
)uu) *
;uu* +
}vv 
returnww 
Taskww 
.ww 

FromResultww "
(ww" #
$numww# $
)ww$ %
;ww% &
}xx 	
public
ÄÄ 
Task
ÄÄ 
RemoveAsync
ÄÄ 
<
ÄÄ  
TSearchResult
ÄÄ  -
>
ÄÄ- .
(
ÄÄ. /

Expression
ÄÄ/ 9
<
ÄÄ9 :
Func
ÄÄ: >
<
ÄÄ> ?
TSearchResult
ÄÄ? L
,
ÄÄL M
bool
ÄÄN R
>
ÄÄR S
>
ÄÄS T
	predicate
ÄÄU ^
)
ÄÄ^ _
where
ÄÄ` e
TSearchResult
ÄÄf s
:
ÄÄt u
class
ÄÄv {
,
ÄÄ{ |
ISearchResultÄÄ} ä
{
ÅÅ 	

_cacheLock
ÇÇ 
.
ÇÇ 
EnterWriteLock
ÇÇ %
(
ÇÇ% &
)
ÇÇ& '
;
ÇÇ' (
try
ÉÉ 
{
ÑÑ 

_instances
ÖÖ 
.
ÖÖ 
	RemoveAll
ÖÖ $
(
ÖÖ$ %
e
ÖÖ% &
=>
ÖÖ' )
	predicate
ÖÖ* 3
.
ÖÖ3 4
Compile
ÖÖ4 ;
(
ÖÖ; <
)
ÖÖ< =
(
ÖÖ= >
(
ÖÖ> ?
TSearchResult
ÖÖ? L
)
ÖÖL M
e
ÖÖN O
)
ÖÖO P
)
ÖÖP Q
;
ÖÖQ R
}
ÜÜ 
finally
áá 
{
àà 

_cacheLock
ââ 
.
ââ 
ExitWriteLock
ââ (
(
ââ( )
)
ââ) *
;
ââ* +
}
ää 
return
ãã 
Task
ãã 
.
ãã 

FromResult
ãã "
(
ãã" #
$num
ãã# $
)
ãã$ %
;
ãã% &
}
åå 	
public
îî 
Task
îî 
<
îî 
TSearchResult
îî !
>
îî! "
	FindAsync
îî# ,
<
îî, -
TSearchResult
îî- :
>
îî: ;
(
îî; <
int
îî< ?
id
îî@ B
)
îîB C
where
îîD I
TSearchResult
îîJ W
:
îîX Y
class
îîZ _
,
îî_ `
ISearchResult
îîa n
{
ïï 	

_cacheLock
ññ 
.
ññ 
EnterReadLock
ññ $
(
ññ$ %
)
ññ% &
;
ññ& '
try
óó 
{
òò 
return
ôô 
Task
ôô 
.
ôô 

FromResult
ôô &
(
ôô& '
(
ôô' (
TSearchResult
ôô( 5
)
ôô5 6

_instances
ôô7 A
.
ôôA B
Find
ôôB F
(
ôôF G
e
ôôG H
=>
ôôI K
e
ôôL M
.
ôôM N
Id
ôôN P
==
ôôQ S
id
ôôT V
)
ôôV W
)
ôôW X
;
ôôX Y
}
öö 
finally
õõ 
{
úú 

_cacheLock
ùù 
.
ùù 
ExitReadLock
ùù '
(
ùù' (
)
ùù( )
;
ùù) *
}
ûû 
}
üü 	
public
¨¨ 
async
¨¨ 
Task
¨¨ 
UpdateAsync
¨¨ %
<
¨¨% &
TSearchResult
¨¨& 3
>
¨¨3 4
(
¨¨4 5
TSearchResult
¨¨5 B
[
¨¨B C
]
¨¨C D
	instances
¨¨E N
)
¨¨N O
where
¨¨P U
TSearchResult
¨¨V c
:
¨¨d e
class
¨¨f k
,
¨¨k l
ISearchResult
¨¨m z
{
≠≠ 	
await
ÆÆ 
this
ÆÆ 
.
ÆÆ 
RemoveAsync
ÆÆ "
(
ÆÆ" #
	instances
ÆÆ# ,
)
ÆÆ, -
;
ÆÆ- .
await
∞∞ 
this
∞∞ 
.
∞∞ 
AddAsync
∞∞ 
(
∞∞  
	instances
∞∞  )
)
∞∞) *
;
∞∞* +
}
±± 	
public
∫∫ 
Task
∫∫ 
UpdateAsync
∫∫ 
<
∫∫  
TSearchResult
∫∫  -
>
∫∫- .
(
∫∫. /

Expression
∫∫/ 9
<
∫∫9 :
Func
∫∫: >
<
∫∫> ?
TSearchResult
∫∫? L
,
∫∫L M
bool
∫∫N R
>
∫∫R S
>
∫∫S T
	predicate
∫∫U ^
,
∫∫^ _

Expression
∫∫` j
<
∫∫j k
Func
∫∫k o
<
∫∫o p
Type
∫∫p t
,
∫∫t u
Type
∫∫v z
>
∫∫z {
>
∫∫{ |

expression∫∫} á
)∫∫á à
where∫∫â é
TSearchResult∫∫è ú
:∫∫ù û
class∫∫ü §
,∫∫§ •
ISearchResult∫∫¶ ≥
{
ªª 	
throw
ºº 
new
ºº #
NotSupportedException
ºº +
(
ºº+ ,
)
ºº, -
;
ºº- .
}
ΩΩ 	
}
ææ 
}øø ò
EC:\Source\Stacks\Core\src\Slalom.Stacks\Search\IRebuildSearchIndex.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Search

 
{ 
public 

	interface 
IRebuildSearchIndex (
{ 
Task 
RebuildIndexAsync 
( 
)  
;  !
} 
} ì
@C:\Source\Stacks\Core\src\Slalom.Stacks\Search\ISearchContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

	interface 
ISearchContext #
{ 
Task   
AddAsync   
<   
TSearchResult   #
>  # $
(  $ %
TSearchResult  % 2
[  2 3
]  3 4
	instances  5 >
)  > ?
where  @ E
TSearchResult  F S
:  T U
class  V [
,  [ \
ISearchResult  ] j
;  j k
Task'' 

ClearAsync'' 
<'' 
TSearchResult'' %
>''% &
(''& '
)''' (
where'') .
TSearchResult''/ <
:''= >
class''? D
,''D E
ISearchResult''F S
;''S T
Task// 
<// 
TSearchResult// 
>// 
	FindAsync// %
<//% &
TSearchResult//& 3
>//3 4
(//4 5
int//5 8
id//9 ;
)//; <
where//= B
TSearchResult//C P
://Q R
class//S X
,//X Y
ISearchResult//Z g
;//g h
Task88 
RemoveAsync88 
<88 
TSearchResult88 &
>88& '
(88' (
TSearchResult88( 5
[885 6
]886 7
	instances888 A
)88A B
where88C H
TSearchResult88I V
:88W X
class88Y ^
,88^ _
ISearchResult88` m
;88m n
TaskAA 
RemoveAsyncAA 
<AA 
TSearchResultAA &
>AA& '
(AA' (

ExpressionAA( 2
<AA2 3
FuncAA3 7
<AA7 8
TSearchResultAA8 E
,AAE F
boolAAG K
>AAK L
>AAL M
	predicateAAN W
)AAW X
whereAAY ^
TSearchResultAA_ l
:AAm n
classAAo t
,AAt u
ISearchResult	AAv É
;
AAÉ Ñ

IQueryableII 
<II 
TSearchResultII  
>II  !
SearchII" (
<II( )
TSearchResultII) 6
>II6 7
(II7 8
stringII8 >
textII? C
=IID E
nullIIF J
)IIJ K
whereIIL Q
TSearchResultIIR _
:II` a
classIIb g
,IIg h
ISearchResultIIi v
;IIv w
TaskWW 
UpdateAsyncWW 
<WW 
TSearchResultWW &
>WW& '
(WW' (
TSearchResultWW( 5
[WW5 6
]WW6 7
	instancesWW8 A
)WWA B
whereWWC H
TSearchResultWWI V
:WWW X
classWWY ^
,WW^ _
ISearchResultWW` m
;WWm n
Taskbb 
UpdateAsyncbb 
<bb 
TSearchResultbb &
>bb& '
(bb' (

Expressionbb( 2
<bb2 3
Funcbb3 7
<bb7 8
TSearchResultbb8 E
,bbE F
boolbbG K
>bbK L
>bbL M
	predicatebbN W
,bbW X

ExpressionbbY c
<bbc d
Funcbbd h
<bbh i
Typebbi m
,bbm n
Typebbo s
>bbs t
>bbt u

expression	bbv Ä
)
bbÄ Å
where
bbÇ á
TSearchResult
bbà ï
:
bbñ ó
class
bbò ù
,
bbù û
ISearchResult
bbü ¨
;
bb¨ ≠
}cc 
}dd Á!
?C:\Source\Stacks\Core\src\Slalom.Stacks\Search\ISearchFacade.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

	interface 
ISearchFacade "
{ 
Task"" 
AddAsync"" 
<"" 
TSearchResult"" #
>""# $
(""$ %
params""% +
TSearchResult"", 9
[""9 :
]"": ;
	instances""< E
)""E F
where""G L
TSearchResult""M Z
:""[ \
class""] b
,""b c
ISearchResult""d q
;""q r
Task)) 

ClearAsync)) 
<)) 
TSearchResult)) %
>))% &
())& '
)))' (
where))) .
TSearchResult))/ <
:))= >
class))? D
,))D E
ISearchResult))F S
;))S T
Task22 
<22 
TSearchResult22 
>22 
	FindAsync22 %
<22% &
TSearchResult22& 3
>223 4
(224 5
int225 8
id229 ;
)22; <
where22= B
TSearchResult22C P
:22Q R
class22S X
,22X Y
ISearchResult22Z g
;22g h

IQueryable99 
<99 
TEntity99 
>99 
Read99  
<99  !
TEntity99! (
>99( )
(99) *
)99* +
;99+ ,
Task@@ 
RebuildIndexAsync@@ 
<@@ 
TSearchResult@@ ,
>@@, -
(@@- .
)@@. /
where@@0 5
TSearchResult@@6 C
:@@D E
class@@F K
,@@K L
ISearchResult@@M Z
;@@Z [
TaskII 
RemoveAsyncII 
<II 
TSearchResultII &
>II& '
(II' (
TSearchResultII( 5
[II5 6
]II6 7
	instancesII8 A
)IIA B
whereIIC H
TSearchResultIII V
:IIW X
classIIY ^
,II^ _
ISearchResultII` m
;IIm n
TaskRR 
RemoveAsyncRR 
<RR 
TSearchResultRR &
>RR& '
(RR' (

ExpressionRR( 2
<RR2 3
FuncRR3 7
<RR7 8
TSearchResultRR8 E
,RRE F
boolRRG K
>RRK L
>RRL M
	predicateRRN W
)RRW X
whereRRY ^
TSearchResultRR_ l
:RRm n
classRRo t
,RRt u
ISearchResult	RRv É
;
RRÉ Ñ

IQueryableZZ 
<ZZ 
TSearchResultZZ  
>ZZ  !
SearchZZ" (
<ZZ( )
TSearchResultZZ) 6
>ZZ6 7
(ZZ7 8
stringZZ8 >
textZZ? C
=ZZD E
nullZZF J
)ZZJ K
whereZZL Q
TSearchResultZZR _
:ZZ` a
classZZb g
,ZZg h
ISearchResultZZi v
;ZZv w
Taskhh 
UpdateAsynchh 
<hh 
TSearchResulthh &
>hh& '
(hh' (
TSearchResulthh( 5
[hh5 6
]hh6 7
	instanceshh8 A
)hhA B
wherehhC H
TSearchResulthhI V
:hhW X
classhhY ^
,hh^ _
ISearchResulthh` m
;hhm n
Taskss 
UpdateAsyncss 
<ss 
TSearchResultss &
>ss& '
(ss' (

Expressionss( 2
<ss2 3
Funcss3 7
<ss7 8
TSearchResultss8 E
,ssE F
boolssG K
>ssK L
>ssL M
	predicatessN W
,ssW X

ExpressionssY c
<ssc d
Funcssd h
<ssh i
TSearchResultssi v
,ssv w
TSearchResult	ssx Ö
>
ssÖ Ü
>
ssÜ á

expression
ssà í
)
ssí ì
where
ssî ô
TSearchResult
ssö ß
:
ss® ©
class
ss™ Ø
,
ssØ ∞
ISearchResult
ss± æ
;
ssæ ø
Taskzz 
Indexzz 
<zz 
TSearchResultzz  
>zz  !
(zz! "
paramszz" (
stringzz) /
[zz/ 0
]zz0 1
idszz2 5
)zz5 6
wherezz7 <
TSearchResultzz= J
:zzK L
classzzM R
,zzR S
ISearchResultzzT a
;zza b
}{{ 
}|| Ü
>C:\Source\Stacks\Core\src\Slalom.Stacks\Search\ISearchIndex.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

	interface 
ISearchIndex !
<! "
TSearchResult" /
>/ 0
:1 2
IRebuildSearchIndex3 F
whereG L
TSearchResultM Z
:[ \
class] b
,b c
ISearchResultd q
{ 
Task 
AddAsync 
( 
TSearchResult #
[# $
]$ %
	instances& /
)/ 0
;0 1
Task"" 

ClearAsync"" 
("" 
)"" 
;"" 
Task** 
<** 
TSearchResult** 
>** 
	FindAsync** %
(**% &
int**& )
id*** ,
)**, -
;**- .
Task11 
Index11 
(11 
params11 
string11  
[11  !
]11! "
ids11# &
)11& '
;11' (
Task88 
RemoveAsync88 
(88 
TSearchResult88 &
[88& '
]88' (
	instances88) 2
)882 3
;883 4
Task?? 
RemoveAsync?? 
(?? 

Expression?? #
<??# $
Func??$ (
<??( )
TSearchResult??) 6
,??6 7
bool??8 <
>??< =
>??= >
	predicate??? H
)??H I
;??I J

IQueryableFF 
<FF 
TSearchResultFF  
>FF  !
SearchFF" (
(FF( )
stringFF) /
textFF0 4
=FF5 6
nullFF7 ;
)FF; <
;FF< =
TaskRR 
UpdateAsyncRR 
(RR 
TSearchResultRR &
[RR& '
]RR' (
	instancesRR) 2
)RR2 3
;RR3 4
Task[[ 
UpdateAsync[[ 
([[ 

Expression[[ #
<[[# $
Func[[$ (
<[[( )
TSearchResult[[) 6
,[[6 7
bool[[8 <
>[[< =
>[[= >
	predicate[[? H
,[[H I

Expression[[J T
<[[T U
Func[[U Y
<[[Y Z
TSearchResult[[Z g
,[[g h
TSearchResult[[i v
>[[v w
>[[w x

expression	[[y É
)
[[É Ñ
;
[[Ñ Ö
}\\ 
}]] ≠
?C:\Source\Stacks\Core\src\Slalom.Stacks\Search\ISearchResult.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{		 
public 

	interface 
ISearchResult "
{ 
int 
Id 
{ 
get 
; 
set 
; 
} 
} 
} ú%
BC:\Source\Stacks\Core\src\Slalom.Stacks\Search\SearchExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

static 
class 
SearchExtensions (
{ 
public 
static 

IQueryable  
<  !
T! "
>" #
Contains$ ,
<, -
T- .
>. /
(/ 0
this0 4

IQueryable5 ?
<? @
T@ A
>A B
instanceC K
,K L
stringM S
textT X
)X Y
{ 	
Argument 
. 
NotNull 
( 
instance %
,% &
nameof' -
(- .
instance. 6
)6 7
)7 8
;8 9
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
text* .
). /
)/ 0
{   
return!! 
instance!! 
;!!  
}"" 
var$$ 
t$$ 
=$$ 

Expression$$ 
.$$ 
	Parameter$$ (
($$( )
typeof$$) /
($$/ 0
T$$0 1
)$$1 2
)$$2 3
;$$3 4

Expression%% 
body%% 
=%% 

Expression%% (
.%%( )
Constant%%) 1
(%%1 2
false%%2 7
)%%7 8
;%%8 9
var'' 
containsMethod'' 
=''  
typeof''! '
(''' (
string''( .
)''. /
.''/ 0
	GetMethod''0 9
(''9 :
$str'': D
,(( 
new(( 
[(( 
](( 
{(( 
typeof(( 
(((  
string((  &
)((& '
}((' (
)((( )
;(() *
var** 
toLowerMethod** 
=** 
typeof**  &
(**& '
string**' -
)**- .
.**. /
	GetMethod**/ 8
(**8 9
$str**9 B
,**B C
new**D G
Type**H L
[**L M
$num**M N
]**N O
)**O P
;**P Q
var,, 
toStringMethod,, 
=,,  
typeof,,! '
(,,' (
object,,( .
),,. /
.,,/ 0
	GetMethod,,0 9
(,,9 :
$str,,: D
),,D E
;,,E F
var.. 
stringProperties..  
=..! "
typeof..# )
(..) *
T..* +
)..+ ,
..., -
GetProperties..- :
(..: ;
)..; <
.// 
Where// 
(// 
property// 
=>//  "
property//# +
.//+ ,
PropertyType//, 8
==//9 ;
typeof//< B
(//B C
string//C I
)//I J
)//J K
;//K L
foreach11 
(11 
var11 
property11 !
in11" $
stringProperties11% 5
)115 6
{22 
var33 
stringValue33 
=33  !

Expression33" ,
.33, -
Call33- 1
(331 2

Expression332 <
.33< =
Property33= E
(33E F
t33F G
,33G H
property33I Q
.33Q R
Name33R V
)33V W
,33W X
toStringMethod44 "
)44" #
;44# $
var66 
updated66 
=66 

Expression66 (
.66( )
Call66) -
(66- .
stringValue66. 9
,669 :
toLowerMethod66; H
)66H I
;66I J
var88 
nextExpression88 "
=88# $

Expression88% /
.88/ 0
Call880 4
(884 5
updated885 <
,88< =
containsMethod99 "
,99" #

Expression:: 
.:: 
Constant:: '
(::' (
text::( ,
.::, -
ToLower::- 4
(::4 5
)::5 6
)::6 7
)::7 8
;::8 9
body<< 
=<< 

Expression<< !
.<<! "
OrElse<<" (
(<<( )
body<<) -
,<<- .
nextExpression<</ =
)<<= >
;<<> ?
}== 
return?? 
instance?? 
.?? 
Where?? !
(??! "

Expression??" ,
.??, -
Lambda??- 3
<??3 4
Func??4 8
<??8 9
T??9 :
,??: ;
bool??< @
>??@ A
>??A B
(??B C
body??C G
,??G H
t??I J
)??J K
)??K L
;??L M
}@@ 	
}AA 
}BB Üè
>C:\Source\Stacks\Core\src\Slalom.Stacks\Search\SearchFacade.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

class 
SearchFacade 
: 
ISearchFacade  -
{ 
private 
readonly 
IComponentContext *
_componentContext+ <
;< =
public 
SearchFacade 
( 
IComponentContext -
componentContext. >
)> ?
{   	
Argument!! 
.!! 
NotNull!! 
(!! 
componentContext!! -
,!!- .
nameof!!/ 5
(!!5 6
componentContext!!6 F
)!!F G
)!!G H
;!!H I
_componentContext## 
=## 
componentContext##  0
;##0 1
}$$ 	
public++ 

IQueryable++ 
<++ 
TEntity++ !
>++! "
Read++# '
<++' (
TEntity++( /
>++/ 0
(++0 1
)++1 2
{,, 	
var-- 
target-- 
=-- 
_componentContext-- *
.--* +
ResolveOptional--+ :
<--: ;
IEntityReader--; H
<--H I
TEntity--I P
>--P Q
>--Q R
(--R S
)--S T
;--T U
if// 
(// 
target// 
==// 
null// 
)// 
{00 
throw11 
new11 %
InvalidOperationException11 3
(113 4
$"114 63
'No reader has been registered for type 116 ]
{11] ^
typeof11^ d
(11d e
TEntity11e l
)11l m
}11m n
.11n o
"11o p
)11p q
;11q r
}22 
return44 
target44 
.44 
Read44 
(44 
)44  
;44  !
}55 	
publicCC 
TaskCC 
AddAsyncCC 
<CC 
TSearchResultCC *
>CC* +
(CC+ ,
paramsCC, 2
TSearchResultCC3 @
[CC@ A
]CCA B
	instancesCCC L
)CCL M
whereCCN S
TSearchResultCCT a
:CCb c
classCCd i
,CCi j
ISearchResultCCk x
{DD 	
ArgumentEE 
.EE 
NotNullEE 
(EE 
	instancesEE &
,EE& '
nameofEE( .
(EE. /
	instancesEE/ 8
)EE8 9
)EE9 :
;EE: ;
ifGG 
(GG 
!GG 
	instancesGG 
.GG 
AnyGG 
(GG 
)GG  
)GG  !
{HH 
returnII 
TaskII 
.II 

FromResultII &
(II& '
$numII' (
)II( )
;II) *
}JJ 
varLL 
storeLL 
=LL 
_componentContextLL )
.LL) *
ResolveOptionalLL* 9
<LL9 :
ISearchIndexLL: F
<LLF G
TSearchResultLLG T
>LLT U
>LLU V
(LLV W
)LLW X
;LLX Y
ifNN 
(NN 
storeNN 
==NN 
nullNN 
)NN 
{OO 
throwPP 
newPP %
InvalidOperationExceptionPP 3
(PP3 4
$"PP4 62
&No index has been registered for type PP6 \
{PP\ ]
typeofPP] c
(PPc d
TSearchResultPPd q
)PPq r
}PPr s
.PPs t
"PPt u
)PPu v
;PPv w
}QQ 
returnSS 
storeSS 
.SS 
AddAsyncSS !
(SS! "
	instancesSS" +
)SS+ ,
;SS, -
}TT 	
public[[ 
Task[[ 

ClearAsync[[ 
<[[ 
TSearchResult[[ ,
>[[, -
([[- .
)[[. /
where[[0 5
TSearchResult[[6 C
:[[D E
class[[F K
,[[K L
ISearchResult[[M Z
{\\ 	
var]] 
store]] 
=]] 
_componentContext]] )
.]]) *
Resolve]]* 1
<]]1 2
ISearchIndex]]2 >
<]]> ?
TSearchResult]]? L
>]]L M
>]]M N
(]]N O
)]]O P
;]]P Q
if__ 
(__ 
store__ 
==__ 
null__ 
)__ 
{`` 
throwaa 
newaa %
InvalidOperationExceptionaa 3
(aa3 4
$"aa4 62
&No index has been registered for type aa6 \
{aa\ ]
typeofaa] c
(aac d
TSearchResultaad q
)aaq r
}aar s
.aas t
"aat u
)aau v
;aav w
}bb 
returndd 
storedd 
.dd 

ClearAsyncdd #
(dd# $
)dd$ %
;dd% &
}ee 	
publicmm 
Taskmm 
<mm 
TSearchResultmm !
>mm! "
	FindAsyncmm# ,
<mm, -
TSearchResultmm- :
>mm: ;
(mm; <
intmm< ?
idmm@ B
)mmB C
wheremmD I
TSearchResultmmJ W
:mmX Y
classmmZ _
,mm_ `
ISearchResultmma n
{nn 	
varoo 
storeoo 
=oo 
_componentContextoo )
.oo) *
Resolveoo* 1
<oo1 2
ISearchIndexoo2 >
<oo> ?
TSearchResultoo? L
>ooL M
>ooM N
(ooN O
)ooO P
;ooP Q
ifpp 
(pp 
storepp 
==pp 
nullpp 
)pp 
{qq 
throwrr 
newrr %
InvalidOperationExceptionrr 3
(rr3 4
$"rr4 62
&No index has been registered for type rr6 \
{rr\ ]
typeofrr] c
(rrc d
TSearchResultrrd q
)rrq r
}rrr s
.rrs t
"rrt u
)rru v
;rrv w
}ss 
returntt 
storett 
.tt 
	FindAsynctt "
(tt" #
idtt# %
)tt% &
;tt& '
}uu 	
public}} 
Task}} 
RebuildIndexAsync}} %
<}}% &
TSearchResult}}& 3
>}}3 4
(}}4 5
)}}5 6
where}}7 <
TSearchResult}}= J
:}}K L
class}}M R
,}}R S
ISearchResult}}T a
{~~ 	
var 
store 
= 
_componentContext )
.) *
Resolve* 1
<1 2
ISearchIndex2 >
<> ?
TSearchResult? L
>L M
>M N
(N O
)O P
;P Q
if
ÄÄ 
(
ÄÄ 
store
ÄÄ 
==
ÄÄ 
null
ÄÄ 
)
ÄÄ 
{
ÅÅ 
throw
ÇÇ 
new
ÇÇ '
InvalidOperationException
ÇÇ 3
(
ÇÇ3 4
$"
ÇÇ4 64
&No index has been registered for type 
ÇÇ6 \
{
ÇÇ\ ]
typeof
ÇÇ] c
(
ÇÇc d
TSearchResult
ÇÇd q
)
ÇÇq r
}
ÇÇr s
.
ÇÇs t
"
ÇÇt u
)
ÇÇu v
;
ÇÇv w
}
ÉÉ 
return
ÑÑ 
store
ÑÑ 
.
ÑÑ 
RebuildIndexAsync
ÑÑ *
(
ÑÑ* +
)
ÑÑ+ ,
;
ÑÑ, -
}
ÖÖ 	
public
éé 
Task
éé 
RemoveAsync
éé 
<
éé  
TSearchResult
éé  -
>
éé- .
(
éé. /
TSearchResult
éé/ <
[
éé< =
]
éé= >
	instances
éé? H
)
ééH I
where
ééJ O
TSearchResult
ééP ]
:
éé^ _
class
éé` e
,
éée f
ISearchResult
éég t
{
èè 	
Argument
êê 
.
êê 
NotNull
êê 
(
êê 
	instances
êê &
,
êê& '
nameof
êê( .
(
êê. /
	instances
êê/ 8
)
êê8 9
)
êê9 :
;
êê: ;
if
íí 
(
íí 
!
íí 
	instances
íí 
.
íí 
Any
íí 
(
íí 
)
íí  
)
íí  !
{
ìì 
return
îî 
Task
îî 
.
îî 

FromResult
îî &
(
îî& '
$num
îî' (
)
îî( )
;
îî) *
}
ïï 
var
óó 
store
óó 
=
óó 
_componentContext
óó )
.
óó) *
Resolve
óó* 1
<
óó1 2
ISearchIndex
óó2 >
<
óó> ?
TSearchResult
óó? L
>
óóL M
>
óóM N
(
óóN O
)
óóO P
;
óóP Q
if
òò 
(
òò 
store
òò 
==
òò 
null
òò 
)
òò 
{
ôô 
throw
öö 
new
öö '
InvalidOperationException
öö 3
(
öö3 4
$"
öö4 64
&No index has been registered for type 
öö6 \
{
öö\ ]
typeof
öö] c
(
ööc d
TSearchResult
ööd q
)
ööq r
}
öör s
.
öös t
"
ööt u
)
ööu v
;
ööv w
}
õõ 
return
úú 
store
úú 
.
úú 
RemoveAsync
úú $
(
úú$ %
	instances
úú% .
)
úú. /
;
úú/ 0
}
ùù 	
public
¶¶ 
Task
¶¶ 
RemoveAsync
¶¶ 
<
¶¶  
TSearchResult
¶¶  -
>
¶¶- .
(
¶¶. /

Expression
¶¶/ 9
<
¶¶9 :
Func
¶¶: >
<
¶¶> ?
TSearchResult
¶¶? L
,
¶¶L M
bool
¶¶N R
>
¶¶R S
>
¶¶S T
	predicate
¶¶U ^
)
¶¶^ _
where
¶¶` e
TSearchResult
¶¶f s
:
¶¶t u
class
¶¶v {
,
¶¶{ |
ISearchResult¶¶} ä
{
ßß 	
Argument
®® 
.
®® 
NotNull
®® 
(
®® 
	predicate
®® &
,
®®& '
nameof
®®( .
(
®®. /
	predicate
®®/ 8
)
®®8 9
)
®®9 :
;
®®: ;
var
™™ 
store
™™ 
=
™™ 
_componentContext
™™ )
.
™™) *
Resolve
™™* 1
<
™™1 2
ISearchIndex
™™2 >
<
™™> ?
TSearchResult
™™? L
>
™™L M
>
™™M N
(
™™N O
)
™™O P
;
™™P Q
if
´´ 
(
´´ 
store
´´ 
==
´´ 
null
´´ 
)
´´ 
{
¨¨ 
throw
≠≠ 
new
≠≠ '
InvalidOperationException
≠≠ 3
(
≠≠3 4
$"
≠≠4 64
&No index has been registered for type 
≠≠6 \
{
≠≠\ ]
typeof
≠≠] c
(
≠≠c d
TSearchResult
≠≠d q
)
≠≠q r
}
≠≠r s
.
≠≠s t
"
≠≠t u
)
≠≠u v
;
≠≠v w
}
ÆÆ 
return
ØØ 
store
ØØ 
.
ØØ 
RemoveAsync
ØØ $
(
ØØ$ %
	predicate
ØØ% .
)
ØØ. /
;
ØØ/ 0
}
∞∞ 	
public
ππ 

IQueryable
ππ 
<
ππ 
TSearchResult
ππ '
>
ππ' (
Search
ππ) /
<
ππ/ 0
TSearchResult
ππ0 =
>
ππ= >
(
ππ> ?
string
ππ? E
text
ππF J
=
ππK L
null
ππM Q
)
ππQ R
where
ππS X
TSearchResult
ππY f
:
ππg h
class
ππi n
,
ππn o
ISearchResult
ππp }
{
∫∫ 	
var
ªª 
store
ªª 
=
ªª 
_componentContext
ªª )
.
ªª) *
Resolve
ªª* 1
<
ªª1 2
ISearchIndex
ªª2 >
<
ªª> ?
TSearchResult
ªª? L
>
ªªL M
>
ªªM N
(
ªªN O
)
ªªO P
;
ªªP Q
if
ºº 
(
ºº 
store
ºº 
==
ºº 
null
ºº 
)
ºº 
{
ΩΩ 
throw
ææ 
new
ææ '
InvalidOperationException
ææ 3
(
ææ3 4
$"
ææ4 6=
/No search indexer has been registered for type 
ææ6 e
{
ææe f
typeof
ææf l
(
ææl m
TSearchResult
ææm z
)
ææz {
}
ææ{ |
.
ææ| }
"
ææ} ~
)
ææ~ 
;ææ Ä
}
øø 
return
¿¿ 
store
¿¿ 
.
¿¿ 
Search
¿¿ 
(
¿¿  
text
¿¿  $
)
¿¿$ %
;
¿¿% &
}
¡¡ 	
public
œœ 
Task
œœ 
UpdateAsync
œœ 
<
œœ  
TSearchResult
œœ  -
>
œœ- .
(
œœ. /
TSearchResult
œœ/ <
[
œœ< =
]
œœ= >
	instances
œœ? H
)
œœH I
where
œœJ O
TSearchResult
œœP ]
:
œœ^ _
class
œœ` e
,
œœe f
ISearchResult
œœg t
{
–– 	
Argument
—— 
.
—— 
NotNull
—— 
(
—— 
	instances
—— &
,
——& '
nameof
——( .
(
——. /
	instances
——/ 8
)
——8 9
)
——9 :
;
——: ;
if
”” 
(
”” 
!
”” 
	instances
”” 
.
”” 
Any
”” 
(
”” 
)
””  
)
””  !
{
‘‘ 
return
’’ 
Task
’’ 
.
’’ 

FromResult
’’ &
(
’’& '
$num
’’' (
)
’’( )
;
’’) *
}
÷÷ 
var
ÿÿ 
store
ÿÿ 
=
ÿÿ 
_componentContext
ÿÿ )
.
ÿÿ) *
Resolve
ÿÿ* 1
<
ÿÿ1 2
ISearchIndex
ÿÿ2 >
<
ÿÿ> ?
TSearchResult
ÿÿ? L
>
ÿÿL M
>
ÿÿM N
(
ÿÿN O
)
ÿÿO P
;
ÿÿP Q
if
ŸŸ 
(
ŸŸ 
store
ŸŸ 
==
ŸŸ 
null
ŸŸ 
)
ŸŸ 
{
⁄⁄ 
throw
€€ 
new
€€ '
InvalidOperationException
€€ 3
(
€€3 4
$"
€€4 64
&No index has been registered for type 
€€6 \
{
€€\ ]
typeof
€€] c
(
€€c d
TSearchResult
€€d q
)
€€q r
}
€€r s
.
€€s t
"
€€t u
)
€€u v
;
€€v w
}
‹‹ 
return
›› 
store
›› 
.
›› 
UpdateAsync
›› $
(
››$ %
	instances
››% .
)
››. /
;
››/ 0
}
ﬁﬁ 	
public
ÈÈ 
Task
ÈÈ 
UpdateAsync
ÈÈ 
<
ÈÈ  
TSearchResult
ÈÈ  -
>
ÈÈ- .
(
ÈÈ. /

Expression
ÈÈ/ 9
<
ÈÈ9 :
Func
ÈÈ: >
<
ÈÈ> ?
TSearchResult
ÈÈ? L
,
ÈÈL M
bool
ÈÈN R
>
ÈÈR S
>
ÈÈS T
	predicate
ÈÈU ^
,
ÈÈ^ _

Expression
ÈÈ` j
<
ÈÈj k
Func
ÈÈk o
<
ÈÈo p
TSearchResult
ÈÈp }
,
ÈÈ} ~
TSearchResultÈÈ å
>ÈÈå ç
>ÈÈç é

expressionÈÈè ô
)ÈÈô ö
whereÈÈõ †
TSearchResultÈÈ° Æ
:ÈÈØ ∞
classÈÈ± ∂
,ÈÈ∂ ∑
ISearchResultÈÈ∏ ≈
{
ÍÍ 	
Argument
ÎÎ 
.
ÎÎ 
NotNull
ÎÎ 
(
ÎÎ 
	predicate
ÎÎ &
,
ÎÎ& '
nameof
ÎÎ( .
(
ÎÎ. /
	predicate
ÎÎ/ 8
)
ÎÎ8 9
)
ÎÎ9 :
;
ÎÎ: ;
Argument
ÏÏ 
.
ÏÏ 
NotNull
ÏÏ 
(
ÏÏ 

expression
ÏÏ '
,
ÏÏ' (
nameof
ÏÏ) /
(
ÏÏ/ 0

expression
ÏÏ0 :
)
ÏÏ: ;
)
ÏÏ; <
;
ÏÏ< =
var
ÓÓ 
store
ÓÓ 
=
ÓÓ 
_componentContext
ÓÓ )
.
ÓÓ) *
Resolve
ÓÓ* 1
<
ÓÓ1 2
ISearchIndex
ÓÓ2 >
<
ÓÓ> ?
TSearchResult
ÓÓ? L
>
ÓÓL M
>
ÓÓM N
(
ÓÓN O
)
ÓÓO P
;
ÓÓP Q
if
ÔÔ 
(
ÔÔ 
store
ÔÔ 
==
ÔÔ 
null
ÔÔ 
)
ÔÔ 
{
 
throw
ÒÒ 
new
ÒÒ '
InvalidOperationException
ÒÒ 3
(
ÒÒ3 4
$"
ÒÒ4 64
&No index has been registered for type 
ÒÒ6 \
{
ÒÒ\ ]
typeof
ÒÒ] c
(
ÒÒc d
TSearchResult
ÒÒd q
)
ÒÒq r
}
ÒÒr s
.
ÒÒs t
"
ÒÒt u
)
ÒÒu v
;
ÒÒv w
}
ÚÚ 
return
ÛÛ 
store
ÛÛ 
.
ÛÛ 
UpdateAsync
ÛÛ $
(
ÛÛ$ %
	predicate
ÛÛ% .
,
ÛÛ. /

expression
ÛÛ0 :
)
ÛÛ: ;
;
ÛÛ; <
}
ÙÙ 	
public
¸¸ 
Task
¸¸ 
Index
¸¸ 
<
¸¸ 
TSearchResult
¸¸ '
>
¸¸' (
(
¸¸( )
params
¸¸) /
string
¸¸0 6
[
¸¸6 7
]
¸¸7 8
ids
¸¸9 <
)
¸¸< =
where
¸¸> C
TSearchResult
¸¸D Q
:
¸¸R S
class
¸¸T Y
,
¸¸Y Z
ISearchResult
¸¸[ h
{
˝˝ 	
var
˛˛ 
store
˛˛ 
=
˛˛ 
_componentContext
˛˛ )
.
˛˛) *
Resolve
˛˛* 1
<
˛˛1 2
ISearchIndex
˛˛2 >
<
˛˛> ?
TSearchResult
˛˛? L
>
˛˛L M
>
˛˛M N
(
˛˛N O
)
˛˛O P
;
˛˛P Q
if
ˇˇ 
(
ˇˇ 
store
ˇˇ 
==
ˇˇ 
null
ˇˇ 
)
ˇˇ 
{
ÄÄ 
throw
ÅÅ 
new
ÅÅ '
InvalidOperationException
ÅÅ 3
(
ÅÅ3 4
$"
ÅÅ4 64
&No index has been registered for type 
ÅÅ6 \
{
ÅÅ\ ]
typeof
ÅÅ] c
(
ÅÅc d
TSearchResult
ÅÅd q
)
ÅÅq r
}
ÅÅr s
.
ÅÅs t
"
ÅÅt u
)
ÅÅu v
;
ÅÅv w
}
ÇÇ 
return
ÉÉ 
store
ÉÉ 
.
ÉÉ 
Index
ÉÉ 
(
ÉÉ 
ids
ÉÉ "
)
ÉÉ" #
;
ÉÉ# $
}
ÑÑ 	
}
ÖÖ 
}ÜÜ ¶T
=C:\Source\Stacks\Core\src\Slalom.Stacks\Search\SearchIndex.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
public 

class 
SearchIndex 
< 
TSearchResult *
>* +
:, -
ISearchIndex. :
<: ;
TSearchResult; H
>H I
whereJ O
TSearchResultP ]
:^ _
class` e
,e f
ISearchResultg t
{ 
private 
readonly 
ISearchContext '
_context( 0
;0 1
public 
SearchIndex 
( 
ISearchContext )
context* 1
)1 2
{   	
Argument!! 
.!! 
NotNull!! 
(!! 
context!! $
,!!$ %
nameof!!& ,
(!!, -
context!!- 4
)!!4 5
)!!5 6
;!!6 7
_context## 
=## 
context## 
;## 
}$$ 	
public** 
IDomainFacade** 
Domain** #
{**$ %
get**& )
;**) *
set**+ .
;**. /
}**0 1
public00 
ILogger00 
Logger00 
{00 
get00  #
;00# $
set00% (
;00( )
}00* +
public88 
virtual88 
Task88 
AddAsync88 $
(88$ %
params88% +
TSearchResult88, 9
[889 :
]88: ;
	instances88< E
)88E F
{99 	
Argument:: 
.:: 
NotNull:: 
(:: 
	instances:: &
,::& '
nameof::( .
(::. /
	instances::/ 8
)::8 9
)::9 :
;::: ;
this<< 
.<< 
Logger<< 
.<< 
Verbose<< 
(<<  
$"<<  "
Adding <<" )
{<<) *
	instances<<* 3
.<<3 4
Count<<4 9
(<<9 :
)<<: ;
}<<; <
 items of type <<< K
{<<K L
typeof<<L R
(<<R S
TSearchResult<<S `
)<<` a
}<<a b
 using <<b i
{<<i j
_context<<j r
.<<r s
GetType<<s z
(<<z {
)<<{ |
}<<| }
.<<} ~
"<<~ 
)	<< Ä
;
<<Ä Å
return>> 
_context>> 
.>> 
AddAsync>> $
(>>$ %
	instances>>% .
)>>. /
;>>/ 0
}?? 	
publicEE 
virtualEE 
TaskEE 

ClearAsyncEE &
(EE& '
)EE' (
{FF 	
thisGG 
.GG 
LoggerGG 
.GG 
VerboseGG 
(GG  
$"GG  "'
Clearing all items of type GG" =
{GG= >
typeofGG> D
(GGD E
TSearchResultGGE R
)GGR S
}GGS T
 using GGT [
{GG[ \
_contextGG\ d
.GGd e
GetTypeGGe l
(GGl m
)GGm n
}GGn o
.GGo p
"GGp q
)GGq r
;GGr s
returnII 
_contextII 
.II 

ClearAsyncII &
<II& '
TSearchResultII' 4
>II4 5
(II5 6
)II6 7
;II7 8
}JJ 	
publicQQ 
virtualQQ 

IQueryableQQ !
<QQ! "
TSearchResultQQ" /
>QQ/ 0
SearchQQ1 7
(QQ7 8
stringQQ8 >
textQQ? C
=QQD E
nullQQF J
)QQJ K
{RR 	
thisSS 
.SS 
LoggerSS 
.SS 
VerboseSS 
(SS  
$"SS  "%
Opening a query for type SS" ;
{SS; <
typeofSS< B
(SSB C
TSearchResultSSC P
)SSP Q
}SSQ R
 using SSR Y
{SSY Z
_contextSSZ b
.SSb c
GetTypeSSc j
(SSj k
)SSk l
}SSl m
.SSm n
"SSn o
)SSo p
;SSp q
returnUU 
_contextUU 
.UU 
SearchUU "
<UU" #
TSearchResultUU# 0
>UU0 1
(UU1 2
textUU2 6
)UU6 7
;UU7 8
}VV 	
public]] 
virtual]] 
Task]] 
RemoveAsync]] '
(]]' (

Expression]]( 2
<]]2 3
Func]]3 7
<]]7 8
TSearchResult]]8 E
,]]E F
bool]]G K
>]]K L
>]]L M
	predicate]]N W
)]]W X
{^^ 	
this__ 
.__ 
Logger__ 
.__ 
Verbose__ 
(__  
$"__  "#
Removing items of type __" 9
{__9 :
typeof__: @
(__@ A
TSearchResult__A N
)__N O
}__O P
 using __P W
{__W X
_context__X `
.__` a
GetType__a h
(__h i
)__i j
}__j k
.__k l
"__l m
)__m n
;__n o
returnaa 
_contextaa 
.aa 
RemoveAsyncaa '
(aa' (
	predicateaa( 1
)aa1 2
;aa2 3
}bb 	
publicee 
virtualee 
Taskee 
Indexee !
(ee! "
paramsee" (
stringee) /
[ee/ 0
]ee0 1
idsee2 5
)ee5 6
{ff 	
returngg 
Taskgg 
.gg 

FromResultgg "
(gg" #
$numgg# $
)gg$ %
;gg% &
}hh 	
publicoo 
virtualoo 
Taskoo 
RemoveAsyncoo '
(oo' (
paramsoo( .
TSearchResultoo/ <
[oo< =
]oo= >
	instancesoo? H
)ooH I
{pp 	
Argumentqq 
.qq 
NotNullqq 
(qq 
	instancesqq &
,qq& '
nameofqq( .
(qq. /
	instancesqq/ 8
)qq8 9
)qq9 :
;qq: ;
thisss 
.ss 
Loggerss 
.ss 
Verbosess 
(ss  
$"ss  "
	Removing ss" +
{ss+ ,
	instancesss, 5
.ss5 6
Countss6 ;
(ss; <
)ss< =
}ss= >
 items of type ss> M
{ssM N
typeofssN T
(ssT U
TSearchResultssU b
)ssb c
}ssc d
 using ssd k
{ssk l
_contextssl t
.sst u
GetTypessu |
(ss| }
)ss} ~
}ss~ 
.	ss Ä
"
ssÄ Å
)
ssÅ Ç
;
ssÇ É
returnuu 
_contextuu 
.uu 
RemoveAsyncuu '
(uu' (
	instancesuu( 1
)uu1 2
;uu2 3
}vv 	
public}} 
virtual}} 
Task}} 
<}} 
TSearchResult}} )
>}}) *
	FindAsync}}+ 4
(}}4 5
int}}5 8
id}}9 ;
)}}; <
{~~ 	
this 
. 
Logger 
. 
Verbose 
(  
$"  "!
Finding item of type " 7
{7 8
typeof8 >
(> ?
TSearchResult? L
)L M
}M N
	 with ID N W
{W X
idX Z
}Z [
 using [ b
{b c
_contextc k
.k l
GetTypel s
(s t
)t u
}u v
.v w
"w x
)x y
;y z
return
ÅÅ 
_context
ÅÅ 
.
ÅÅ 
	FindAsync
ÅÅ %
<
ÅÅ% &
TSearchResult
ÅÅ& 3
>
ÅÅ3 4
(
ÅÅ4 5
id
ÅÅ5 7
)
ÅÅ7 8
;
ÅÅ8 9
}
ÇÇ 	
public
àà 
virtual
àà 
Task
àà 
RebuildIndexAsync
àà -
(
àà- .
)
àà. /
{
ââ 	
this
ää 
.
ää 
Logger
ää 
.
ää 
Verbose
ää 
(
ää  
$"
ää  "1
#Rebuilding index for items of type 
ää" E
{
ääE F
typeof
ääF L
(
ääL M
TSearchResult
ääM Z
)
ääZ [
}
ää[ \
 using 
ää\ c
{
ääc d
_context
ääd l
.
ääl m
GetType
ääm t
(
äät u
)
ääu v
}
ääv w
.
ääw x
"
ääx y
)
ääy z
;
ääz {
return
åå 
Task
åå 
.
åå 

FromResult
åå "
(
åå" #
$num
åå# $
)
åå$ %
;
åå% &
}
çç 	
public
ïï 
virtual
ïï 
Task
ïï 
UpdateAsync
ïï '
(
ïï' (
params
ïï( .
TSearchResult
ïï/ <
[
ïï< =
]
ïï= >
	instances
ïï? H
)
ïïH I
{
ññ 	
Argument
óó 
.
óó 
NotNull
óó 
(
óó 
	instances
óó &
,
óó& '
nameof
óó( .
(
óó. /
	instances
óó/ 8
)
óó8 9
)
óó9 :
;
óó: ;
this
ôô 
.
ôô 
Logger
ôô 
.
ôô 
Verbose
ôô 
(
ôô  
$"
ôô  "
	Updating 
ôô" +
{
ôô+ ,
	instances
ôô, 5
.
ôô5 6
Count
ôô6 ;
(
ôô; <
)
ôô< =
}
ôô= >
 items of type 
ôô> M
{
ôôM N
typeof
ôôN T
(
ôôT U
TSearchResult
ôôU b
)
ôôb c
}
ôôc d
 using 
ôôd k
{
ôôk l
_context
ôôl t
.
ôôt u
GetType
ôôu |
(
ôô| }
)
ôô} ~
}
ôô~ 
.ôô Ä
"ôôÄ Å
)ôôÅ Ç
;ôôÇ É
return
õõ 
_context
õõ 
.
õõ 
UpdateAsync
õõ '
(
õõ' (
	instances
õõ( 1
)
õõ1 2
;
õõ2 3
}
úú 	
public
•• 
virtual
•• 
Task
•• 
UpdateAsync
•• '
(
••' (

Expression
••( 2
<
••2 3
Func
••3 7
<
••7 8
TSearchResult
••8 E
,
••E F
bool
••G K
>
••K L
>
••L M
	predicate
••N W
,
••W X

Expression
••Y c
<
••c d
Func
••d h
<
••h i
TSearchResult
••i v
,
••v w
TSearchResult••x Ö
>••Ö Ü
>••Ü á

expression••à í
)••í ì
{
¶¶ 	
throw
ßß 
new
ßß #
NotSupportedException
ßß +
(
ßß+ ,
)
ßß, -
;
ßß- .
}
®® 	
}
©© 
}™™ ß$
>C:\Source\Stacks\Core\src\Slalom.Stacks\Search\SearchModule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Search 
{ 
internal 
class 
SearchModule 
:  !
Module" (
{ 
private 
readonly 
Stack 
_stack %
;% &
public 
SearchModule 
( 
Stack !
stack" '
)' (
{ 	
_stack 
= 
stack 
; 
} 	
	protected)) 
override)) 
void)) 
Load))  $
())$ %
ContainerBuilder))% 5
builder))6 =
)))= >
{** 	
base++ 
.++ 
Load++ 
(++ 
builder++ 
)++ 
;++ 
builder-- 
.-- 
Register-- 
(-- 
c-- 
=>-- !
new--" %!
InMemorySearchContext--& ;
(--; <
)--< =
)--= >
... #
AsImplementedInterfaces.. (
(..( )
)..) *
.// 
AsSelf// 
(// 
)// 
.00 
SingleInstance00 
(00  
)00  !
;00! "
builder22 
.22 
Register22 
(22 
c22 
=>22 !
new22" %
SearchFacade22& 2
(222 3
c223 4
.224 5
Resolve225 <
<22< =
IComponentContext22= N
>22N O
(22O P
)22P Q
)22Q R
)22R S
.33 #
AsImplementedInterfaces33 (
(33( )
)33) *
.44 
AsSelf44 
(44 
)44 
.55 
PropertiesAutowired55 $
(55$ %
)55% &
.66 
SingleInstance66 
(66  
)66  !
;66! "
builder88 
.88 
RegisterGeneric88 #
(88# $
typeof88$ *
(88* +
SearchIndex88+ 6
<886 7
>887 8
)888 9
)889 :
.99 
As99 
(99 
typeof99 
(99 
ISearchIndex99 '
<99' (
>99( )
)99) *
)99* +
.:: 
PropertiesAutowired:: $
(::$ %
)::% &
.;; !
InstancePerDependency;; &
(;;& '
);;' (
;;;( )
builder== 
.== !
RegisterAssemblyTypes== )
(==) *
_stack==* 0
.==0 1

Assemblies==1 ;
.==; <
ToArray==< C
(==C D
)==D E
)==E F
.>> 
Where>> 
(>> 
e>> 
=>>> 
e>> 
.>> #
GetBaseAndContractTypes>> 5
(>>5 6
)>>6 7
.>>7 8
Any>>8 ;
(>>; <
x>>< =
=>>>> @
x>>A B
==>>C E
typeof>>F L
(>>L M
ISearchIndex>>M Y
<>>Y Z
>>>Z [
)>>[ \
)>>\ ]
)>>] ^
.?? 
As?? 
(?? 
instance?? 
=>?? 
{@@ 
varAA 

interfacesAA "
=AA# $
instanceAA% -
.AA- .
GetInterfacesAA. ;
(AA; <
)AA< =
.AA= >
WhereAA> C
(AAC D
eAAD E
=>AAF H
eAAI J
.AAJ K
GetTypeInfoAAK V
(AAV W
)AAW X
.AAX Y
IsGenericTypeAAY f
&&AAg i
eAAj k
.AAk l%
GetGenericTypeDefinition	AAl Ñ
(
AAÑ Ö
)
AAÖ Ü
==
AAá â
typeof
AAä ê
(
AAê ë
ISearchIndex
AAë ù
<
AAù û
>
AAû ü
)
AAü †
)
AA† °
;
AA° ¢
returnBB 

interfacesBB %
.BB% &
SelectBB& ,
(BB, -
eBB- .
=>BB/ 1
typeofBB2 8
(BB8 9
ISearchIndexBB9 E
<BBE F
>BBF G
)BBG H
.BBH I
MakeGenericTypeBBI X
(BBX Y
eBBY Z
.BBZ [
GetGenericArgumentsBB[ n
(BBn o
)BBo p
[BBp q
$numBBq r
]BBr s
)BBs t
)BBt u
;BBu v
}CC 
)CC 
.DD 
PropertiesAutowiredDD $
(DD$ %
)DD% &
.EE !
InstancePerDependencyEE &
(EE& '
)EE' (
;EE( )
}FF 	
}GG 
}HH Æ
EC:\Source\Stacks\Core\src\Slalom.Stacks\Security\AnonymousIdentity.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Security

  
{ 
public 

class 
AnonymousIdentity "
:# $
GenericIdentity% 4
{ 
public 
AnonymousIdentity  
(  !
)! "
: 
base 
( 
$str 
) 
{ 	
} 	
public 
override 
bool 
IsAuthenticated ,
=>- /
false0 5
;5 6
public%% 
override%% 
string%% 
Name%% #
=>%%$ &
null%%' +
;%%+ ,
}&& 
}'' ∆
FC:\Source\Stacks\Core\src\Slalom.Stacks\Security\AnonymousPrincipal.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Security

  
{ 
public 

class 
AnonymousPrincipal #
:$ %
GenericPrincipal& 6
{ 
public 
AnonymousPrincipal !
(! "
)" #
: 
base 
( 
new 
AnonymousIdentity (
(( )
)) *
,* +
new, /
string0 6
[6 7
$num7 8
]8 9
)9 :
{ 	
} 	
} 
} …Ã
>C:\Source\Stacks\Core\src\Slalom.Stacks\Security\Encryption.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Security  
{ 
public 

static 
class 

Encryption "
{ 
public!! 
static!! 
byte!! 
[!! 
]!! 
Decrypt!! $
<!!$ %
T!!% &
>!!& '
(!!' (
byte!!( ,
[!!, -
]!!- .
text!!/ 3
,!!3 4
string!!5 ;
key!!< ?
)!!? @
where!!A F
T!!G H
:!!I J
SymmetricAlgorithm!!K ]
,!!] ^
new!!_ b
(!!b c
)!!c d
{"" 	
Argument## 
.## 
NotNull## 
(## 
text## !
,##! "
nameof### )
(##) *
text##* .
)##. /
)##/ 0
;##0 1
using%% 
(%% 
var%% 
provider%% 
=%%  !
	Activator%%" +
.%%+ ,
CreateInstance%%, :
<%%: ;
T%%; <
>%%< =
(%%= >
)%%> ?
)%%? @
{&& 
return'' 
Decrypt'' 
('' 
text'' #
,''# $
key''% (
,''( )
provider''* 2
)''2 3
;''3 4
}(( 
})) 	
public55 
static55 
string55 
Decrypt55 $
<55$ %
T55% &
>55& '
(55' (
string55( .
text55/ 3
,553 4
string555 ;
key55< ?
)55? @
where55A F
T55G H
:55I J
SymmetricAlgorithm55K ]
,55] ^
new55_ b
(55b c
)55c d
{66 	
Argument77 
.77 
NotNull77 
(77 
text77 !
,77! "
nameof77# )
(77) *
text77* .
)77. /
)77/ 0
;770 1
using99 
(99 
var99 
provider99 
=99  !
	Activator99" +
.99+ ,
CreateInstance99, :
<99: ;
T99; <
>99< =
(99= >
)99> ?
)99? @
{:: 
return;; 
Decrypt;; 
(;; 
text;; #
,;;# $
key;;% (
,;;( )
provider;;* 2
);;2 3
;;;3 4
}<< 
}== 	
publicHH 
staticHH 
stringHH 
DecryptHH $
(HH$ %
stringHH% +
textHH, 0
,HH0 1
stringHH2 8
keyHH9 <
)HH< =
{II 	
ArgumentJJ 
.JJ 
NotNullJJ 
(JJ 
textJJ !
,JJ! "
nameofJJ# )
(JJ) *
textJJ* .
)JJ. /
)JJ/ 0
;JJ0 1
returnLL 
DecryptLL 
(LL 
textLL 
,LL  
keyLL! $
,LL$ %
nullLL& *
)LL* +
;LL+ ,
}MM 	
publicXX 
staticXX 
byteXX 
[XX 
]XX 
DecryptXX $
(XX$ %
byteXX% )
[XX) *
]XX* +
textXX, 0
,XX0 1
stringXX2 8
keyXX9 <
)XX< =
{YY 	
ArgumentZZ 
.ZZ 
NotNullZZ 
(ZZ 
textZZ !
,ZZ! "
nameofZZ# )
(ZZ) *
textZZ* .
)ZZ. /
)ZZ/ 0
;ZZ0 1
return\\ 
Decrypt\\ 
(\\ 
text\\ 
,\\  
key\\! $
,\\$ %
null\\& *
)\\* +
;\\+ ,
}]] 	
publichh 
statichh 
stringhh 
Decrypthh $
(hh$ %
stringhh% +
texthh, 0
)hh0 1
{ii 	
Argumentjj 
.jj 
NotNulljj 
(jj 
textjj !
,jj! "
nameofjj# )
(jj) *
textjj* .
)jj. /
)jj/ 0
;jj0 1
returnll 
Decryptll 
(ll 
textll 
,ll  
nullll! %
,ll% &
nullll' +
)ll+ ,
;ll, -
}mm 	
publicuu 
staticuu 
byteuu 
[uu 
]uu 
Decryptuu $
(uu$ %
byteuu% )
[uu) *
]uu* +
textuu, 0
)uu0 1
{vv 	
Argumentww 
.ww 
NotNullww 
(ww 
textww !
,ww! "
nameofww# )
(ww) *
textww* .
)ww. /
)ww/ 0
;ww0 1
returnyy 
Decryptyy 
(yy 
textyy 
,yy  
nullyy! %
,yy% &
nullyy' +
)yy+ ,
;yy, -
}zz 	
public
ÖÖ 
static
ÖÖ 
string
ÖÖ 
Encrypt
ÖÖ $
(
ÖÖ$ %
string
ÖÖ% +
text
ÖÖ, 0
,
ÖÖ0 1
string
ÖÖ2 8
key
ÖÖ9 <
)
ÖÖ< =
{
ÜÜ 	
Argument
áá 
.
áá 
NotNull
áá 
(
áá 
text
áá !
,
áá! "
nameof
áá# )
(
áá) *
text
áá* .
)
áá. /
)
áá/ 0
;
áá0 1
return
ââ 
Encrypt
ââ 
(
ââ 
text
ââ 
,
ââ  
null
ââ! %
,
ââ% &
key
ââ' *
)
ââ* +
;
ââ+ ,
}
ää 	
public
ïï 
static
ïï 
byte
ïï 
[
ïï 
]
ïï 
Encrypt
ïï $
(
ïï$ %
byte
ïï% )
[
ïï) *
]
ïï* +
text
ïï, 0
,
ïï0 1
string
ïï2 8
key
ïï9 <
)
ïï< =
{
ññ 	
return
óó 
Encrypt
óó 
(
óó 
text
óó 
,
óó  
null
óó! %
,
óó% &
key
óó' *
)
óó* +
;
óó+ ,
}
òò 	
public
§§ 
static
§§ 
string
§§ 
Encrypt
§§ $
<
§§$ %
T
§§% &
>
§§& '
(
§§' (
string
§§( .
text
§§/ 3
,
§§3 4
string
§§5 ;
key
§§< ?
)
§§? @
where
§§A F
T
§§G H
:
§§I J 
SymmetricAlgorithm
§§K ]
,
§§] ^
new
§§_ b
(
§§b c
)
§§c d
{
•• 	
Argument
¶¶ 
.
¶¶ 
NotNull
¶¶ 
(
¶¶ 
text
¶¶ !
,
¶¶! "
nameof
¶¶# )
(
¶¶) *
text
¶¶* .
)
¶¶. /
)
¶¶/ 0
;
¶¶0 1
using
®® 
(
®® 
var
®® 
provider
®® 
=
®®  !
	Activator
®®" +
.
®®+ ,
CreateInstance
®®, :
<
®®: ;
T
®®; <
>
®®< =
(
®®= >
)
®®> ?
)
®®? @
{
©© 
return
™™ 
Encrypt
™™ 
(
™™ 
text
™™ #
,
™™# $
provider
™™% -
,
™™- .
key
™™/ 2
)
™™2 3
;
™™3 4
}
´´ 
}
¨¨ 	
public
∏∏ 
static
∏∏ 
byte
∏∏ 
[
∏∏ 
]
∏∏ 
Encrypt
∏∏ $
<
∏∏$ %
T
∏∏% &
>
∏∏& '
(
∏∏' (
byte
∏∏( ,
[
∏∏, -
]
∏∏- .
text
∏∏/ 3
,
∏∏3 4
string
∏∏5 ;
key
∏∏< ?
)
∏∏? @
where
∏∏A F
T
∏∏G H
:
∏∏I J 
SymmetricAlgorithm
∏∏K ]
,
∏∏] ^
new
∏∏_ b
(
∏∏b c
)
∏∏c d
{
ππ 	
Argument
∫∫ 
.
∫∫ 
NotNull
∫∫ 
(
∫∫ 
text
∫∫ !
,
∫∫! "
nameof
∫∫# )
(
∫∫) *
text
∫∫* .
)
∫∫. /
)
∫∫/ 0
;
∫∫0 1
using
ºº 
(
ºº 
var
ºº 
provider
ºº 
=
ºº  !
	Activator
ºº" +
.
ºº+ ,
CreateInstance
ºº, :
<
ºº: ;
T
ºº; <
>
ºº< =
(
ºº= >
)
ºº> ?
)
ºº? @
{
ΩΩ 
return
ææ 
Encrypt
ææ 
(
ææ 
text
ææ #
,
ææ# $
provider
ææ% -
,
ææ- .
key
ææ/ 2
)
ææ2 3
;
ææ3 4
}
øø 
}
¿¿ 	
public
   
static
   
string
   
Encrypt
   $
(
  $ %
string
  % +
text
  , 0
)
  0 1
{
ÀÀ 	
Argument
ÃÃ 
.
ÃÃ 
NotNull
ÃÃ 
(
ÃÃ 
text
ÃÃ !
,
ÃÃ! "
nameof
ÃÃ# )
(
ÃÃ) *
text
ÃÃ* .
)
ÃÃ. /
)
ÃÃ/ 0
;
ÃÃ0 1
return
ŒŒ 
Encrypt
ŒŒ 
(
ŒŒ 
text
ŒŒ 
,
ŒŒ  
null
ŒŒ! %
,
ŒŒ% &
null
ŒŒ' +
)
ŒŒ+ ,
;
ŒŒ, -
}
œœ 	
public
ŸŸ 
static
ŸŸ 
byte
ŸŸ 
[
ŸŸ 
]
ŸŸ 
Encrypt
ŸŸ $
(
ŸŸ$ %
byte
ŸŸ% )
[
ŸŸ) *
]
ŸŸ* +
text
ŸŸ, 0
)
ŸŸ0 1
{
⁄⁄ 	
Argument
€€ 
.
€€ 
NotNull
€€ 
(
€€ 
text
€€ !
,
€€! "
nameof
€€# )
(
€€) *
text
€€* .
)
€€. /
)
€€/ 0
;
€€0 1
return
›› 
Encrypt
›› 
(
›› 
text
›› 
,
››  
null
››! %
,
››% &
null
››' +
)
››+ ,
;
››, -
}
ﬁﬁ 	
public
ËË 
static
ËË 
string
ËË 
Hash
ËË !
(
ËË! "
string
ËË" (
text
ËË) -
,
ËË- .
string
ËË/ 5
salt
ËË6 :
)
ËË: ;
{
ÈÈ 	
Argument
ÍÍ 
.
ÍÍ 
NotNull
ÍÍ 
(
ÍÍ 
text
ÍÍ !
,
ÍÍ! "
nameof
ÍÍ# )
(
ÍÍ) *
text
ÍÍ* .
)
ÍÍ. /
)
ÍÍ/ 0
;
ÍÍ0 1
Argument
ÎÎ 
.
ÎÎ 
NotNull
ÎÎ 
(
ÎÎ 
salt
ÎÎ !
,
ÎÎ! "
nameof
ÎÎ# )
(
ÎÎ) *
salt
ÎÎ* .
)
ÎÎ. /
)
ÎÎ/ 0
;
ÎÎ0 1
return
ÌÌ 
Hash
ÌÌ 
(
ÌÌ 
salt
ÌÌ 
+
ÌÌ 
Hash
ÌÌ #
(
ÌÌ# $
text
ÌÌ$ (
)
ÌÌ( )
)
ÌÌ) *
;
ÌÌ* +
}
ÓÓ 	
public
ˆˆ 
static
ˆˆ 
string
ˆˆ 
Hash
ˆˆ !
(
ˆˆ! "
string
ˆˆ" (
text
ˆˆ) -
)
ˆˆ- .
{
˜˜ 	
Argument
¯¯ 
.
¯¯ 
NotNull
¯¯ 
(
¯¯ 
text
¯¯ !
,
¯¯! "
nameof
¯¯# )
(
¯¯) *
text
¯¯* .
)
¯¯. /
)
¯¯/ 0
;
¯¯0 1
using
˙˙ 
(
˙˙ 
var
˙˙ 
provider
˙˙ 
=
˙˙  !
MD5
˙˙" %
.
˙˙% &
Create
˙˙& ,
(
˙˙, -
)
˙˙- .
)
˙˙. /
{
˚˚ 
var
¸¸ 
data
¸¸ 
=
¸¸ 
provider
¸¸ #
.
¸¸# $
ComputeHash
¸¸$ /
(
¸¸/ 0
Encoding
¸¸0 8
.
¸¸8 9
UTF8
¸¸9 =
.
¸¸= >
GetBytes
¸¸> F
(
¸¸F G
text
¸¸G K
)
¸¸K L
)
¸¸L M
;
¸¸M N
return
˝˝ 
Convert
˝˝ 
.
˝˝ 
ToBase64String
˝˝ -
(
˝˝- .
data
˝˝. 2
)
˝˝2 3
;
˝˝3 4
}
˛˛ 
}
ˇˇ 	
private
ÉÉ 
static
ÉÉ 
void
ÉÉ 
SetValidKey
ÉÉ '
(
ÉÉ' (
this
ÉÉ( , 
SymmetricAlgorithm
ÉÉ- ?
provider
ÉÉ@ H
,
ÉÉH I
string
ÉÉJ P
key
ÉÉQ T
)
ÉÉT U
{
ÑÑ 	
key
ÖÖ 
=
ÖÖ 
key
ÖÖ 
??
ÖÖ 
$str
ÖÖ 
;
ÖÖ 
provider
ÜÜ 
.
ÜÜ 
GenerateKey
ÜÜ  
(
ÜÜ  !
)
ÜÜ! "
;
ÜÜ" #
provider
áá 
.
áá 
Key
áá 
=
áá 
Encoding
áá #
.
áá# $
ASCII
áá$ )
.
áá) *
GetBytes
áá* 2
(
áá2 3
key
áá3 6
.
áá6 7
Resize
áá7 =
(
áá= >
provider
áá> F
.
ááF G
Key
ááG J
.
ááJ K
Length
ááK Q
,
ááQ R
$char
ááS V
)
ááV W
)
ááW X
;
ááX Y
}
àà 	
[
ää 	
SuppressMessage
ää	 
(
ää 
$str
ää $
,
ää$ %
$str
ää& :
)
ää: ;
]
ää; <
private
ãã 
static
ãã 
void
ãã 

SetValidIV
ãã &
(
ãã& '
this
ãã' + 
SymmetricAlgorithm
ãã, >
provider
ãã? G
,
ããG H
string
ããI O
key
ããP S
)
ããS T
{
åå 	
key
çç 
=
çç 
key
çç 
??
çç 
$str
çç 
;
çç 
provider
éé 
.
éé 

GenerateIV
éé 
(
éé  
)
éé  !
;
éé! "
provider
èè 
.
èè 
IV
èè 
=
èè 
Encoding
èè "
.
èè" #
ASCII
èè# (
.
èè( )
GetBytes
èè) 1
(
èè1 2
key
èè2 5
.
èè5 6
Resize
èè6 <
(
èè< =
provider
èè= E
.
èèE F
IV
èèF H
.
èèH I
Length
èèI O
,
èèO P
$char
èèQ T
)
èèT U
)
èèU V
;
èèV W
}
êê 	
private
íí 
static
íí 
byte
íí 
[
íí 
]
íí 
Encrypt
íí %
(
íí% &
byte
íí& *
[
íí* +
]
íí+ ,
text
íí- 1
,
íí1 2 
SymmetricAlgorithm
íí3 E
provider
ííF N
,
ííN O
string
ííP V
key
ííW Z
)
ííZ [
{
ìì 	
var
îî 
created
îî 
=
îî 
false
îî 
;
îî  
try
ïï 
{
ññ 
if
óó 
(
óó 
provider
óó 
==
óó 
null
óó  $
)
óó$ %
{
òò 
provider
ôô 
=
ôô 
Aes
ôô "
.
ôô" #
Create
ôô# )
(
ôô) *
)
ôô* +
;
ôô+ ,
created
öö 
=
öö 
true
öö "
;
öö" #
}
õõ 
provider
úú 
.
úú 
SetValidKey
úú $
(
úú$ %
key
úú% (
)
úú( )
;
úú) *
provider
ùù 
.
ùù 

SetValidIV
ùù #
(
ùù# $
key
ùù$ '
)
ùù' (
;
ùù( )
using
†† 
(
†† 
var
†† 
memoryStream
†† '
=
††( )
new
††* -
MemoryStream
††. :
(
††: ;
)
††; <
)
††< =
{
°° 
var
¢¢ 
cryptoStream
¢¢ $
=
¢¢% &
new
¢¢' *
CryptoStream
¢¢+ 7
(
¢¢7 8
memoryStream
¢¢8 D
,
¢¢D E
provider
¢¢F N
.
¢¢N O
CreateEncryptor
¢¢O ^
(
¢¢^ _
)
¢¢_ `
,
¢¢` a
CryptoStreamMode
¢¢b r
.
¢¢r s
Write
¢¢s x
)
¢¢x y
;
¢¢y z
cryptoStream
££  
.
££  !
Write
££! &
(
££& '
text
££' +
,
££+ ,
$num
££- .
,
££. /
text
££0 4
.
££4 5
Length
££5 ;
)
££; <
;
££< =
cryptoStream
§§  
.
§§  !
FlushFinalBlock
§§! 0
(
§§0 1
)
§§1 2
;
§§2 3
return
•• 
memoryStream
•• '
.
••' (
ToArray
••( /
(
••/ 0
)
••0 1
;
••1 2
}
¶¶ 
}
ßß 
finally
®® 
{
©© 
if
™™ 
(
™™ 
created
™™ 
)
™™ 
{
´´ 
provider
¨¨ 
.
¨¨ 
Dispose
¨¨ $
(
¨¨$ %
)
¨¨% &
;
¨¨& '
}
≠≠ 
}
ÆÆ 
}
ØØ 	
private
±± 
static
±± 
string
±± 
Encrypt
±± %
(
±±% &
string
±±& ,
text
±±- 1
,
±±1 2 
SymmetricAlgorithm
±±3 E
provider
±±F N
,
±±N O
string
±±P V
key
±±W Z
)
±±Z [
{
≤≤ 	
string
≥≥ 
target
≥≥ 
=
≥≥ 
null
≥≥  
;
≥≥  !
var
¥¥ 
created
¥¥ 
=
¥¥ 
false
¥¥ 
;
¥¥  
try
µµ 
{
∂∂ 
if
∑∑ 
(
∑∑ 
provider
∑∑ 
==
∑∑ 
null
∑∑  $
)
∑∑$ %
{
∏∏ 
provider
ππ 
=
ππ 
Aes
ππ "
.
ππ" #
Create
ππ# )
(
ππ) *
)
ππ* +
;
ππ+ ,
created
∫∫ 
=
∫∫ 
true
∫∫ "
;
∫∫" #
}
ªª 
provider
ºº 
.
ºº 
SetValidKey
ºº $
(
ºº$ %
key
ºº% (
)
ºº( )
;
ºº) *
provider
ΩΩ 
.
ΩΩ 

SetValidIV
ΩΩ #
(
ΩΩ# $
key
ΩΩ$ '
)
ΩΩ' (
;
ΩΩ( )
using
¿¿ 
(
¿¿ 
var
¿¿ 
memoryStream
¿¿ '
=
¿¿( )
new
¿¿* -
MemoryStream
¿¿. :
(
¿¿: ;
)
¿¿; <
)
¿¿< =
{
¡¡ 
var
¬¬ 
content
¬¬ 
=
¬¬  !
Encoding
¬¬" *
.
¬¬* +
ASCII
¬¬+ 0
.
¬¬0 1
GetBytes
¬¬1 9
(
¬¬9 :
text
¬¬: >
)
¬¬> ?
;
¬¬? @
var
√√ 
cryptoStream
√√ $
=
√√% &
new
√√' *
CryptoStream
√√+ 7
(
√√7 8
memoryStream
√√8 D
,
√√D E
provider
√√F N
.
√√N O
CreateEncryptor
√√O ^
(
√√^ _
)
√√_ `
,
√√` a
CryptoStreamMode
√√b r
.
√√r s
Write
√√s x
)
√√x y
;
√√y z
cryptoStream
ƒƒ  
.
ƒƒ  !
Write
ƒƒ! &
(
ƒƒ& '
content
ƒƒ' .
,
ƒƒ. /
$num
ƒƒ0 1
,
ƒƒ1 2
content
ƒƒ3 :
.
ƒƒ: ;
Length
ƒƒ; A
)
ƒƒA B
;
ƒƒB C
cryptoStream
≈≈  
.
≈≈  !
FlushFinalBlock
≈≈! 0
(
≈≈0 1
)
≈≈1 2
;
≈≈2 3
target
∆∆ 
=
∆∆ 
Convert
∆∆ $
.
∆∆$ %
ToBase64String
∆∆% 3
(
∆∆3 4
memoryStream
∆∆4 @
.
∆∆@ A
ToArray
∆∆A H
(
∆∆H I
)
∆∆I J
)
∆∆J K
;
∆∆K L
}
«« 
}
»» 
finally
…… 
{
   
if
ÀÀ 
(
ÀÀ 
created
ÀÀ 
)
ÀÀ 
{
ÃÃ 
provider
ÕÕ 
.
ÕÕ 
Dispose
ÕÕ $
(
ÕÕ$ %
)
ÕÕ% &
;
ÕÕ& '
}
ŒŒ 
}
œœ 
return
–– 
target
–– 
;
–– 
}
—— 	
private
”” 
static
”” 
string
”” 
Decrypt
”” %
(
””% &
string
””& ,
text
””- 1
,
””1 2
string
””3 9
key
””: =
,
””= > 
SymmetricAlgorithm
””? Q
provider
””R Z
)
””Z [
{
‘‘ 	
string
’’ 
target
’’ 
=
’’ 
null
’’  
;
’’  !
var
÷÷ 
created
÷÷ 
=
÷÷ 
false
÷÷ 
;
÷÷  
try
◊◊ 
{
ÿÿ 
if
ŸŸ 
(
ŸŸ 
provider
ŸŸ 
==
ŸŸ 
null
ŸŸ  $
)
ŸŸ$ %
{
⁄⁄ 
provider
€€ 
=
€€ 
Aes
€€ "
.
€€" #
Create
€€# )
(
€€) *
)
€€* +
;
€€+ ,
created
‹‹ 
=
‹‹ 
true
‹‹ "
;
‹‹" #
}
›› 
provider
ﬁﬁ 
.
ﬁﬁ 
SetValidKey
ﬁﬁ $
(
ﬁﬁ$ %
key
ﬁﬁ% (
)
ﬁﬁ( )
;
ﬁﬁ) *
provider
ﬂﬂ 
.
ﬂﬂ 

SetValidIV
ﬂﬂ #
(
ﬂﬂ# $
key
ﬂﬂ$ '
)
ﬂﬂ' (
;
ﬂﬂ( )
var
·· 
content
·· 
=
·· 
Convert
·· %
.
··% &
FromBase64String
··& 6
(
··6 7
text
··7 ;
)
··; <
;
··< =
using
„„ 
(
„„ 
var
„„ 
memoryStream
„„ '
=
„„( )
new
„„* -
MemoryStream
„„. :
(
„„: ;
content
„„; B
,
„„B C
$num
„„D E
,
„„E F
content
„„G N
.
„„N O
Length
„„O U
)
„„U V
)
„„V W
{
‰‰ 
var
ÂÂ 
cryptoStream
ÂÂ $
=
ÂÂ% &
new
ÂÂ' *
CryptoStream
ÂÂ+ 7
(
ÂÂ7 8
memoryStream
ÂÂ8 D
,
ÂÂD E
provider
ÂÂF N
?
ÂÂN O
.
ÂÂO P
CreateDecryptor
ÂÂP _
(
ÂÂ_ `
)
ÂÂ` a
,
ÂÂa b
CryptoStreamMode
ÂÂc s
.
ÂÂs t
Read
ÂÂt x
)
ÂÂx y
;
ÂÂy z
using
ÊÊ 
(
ÊÊ 
var
ÊÊ 
reader
ÊÊ %
=
ÊÊ& '
new
ÊÊ( +
StreamReader
ÊÊ, 8
(
ÊÊ8 9
cryptoStream
ÊÊ9 E
)
ÊÊE F
)
ÊÊF G
{
ÁÁ 
target
ËË 
=
ËË  
reader
ËË! '
.
ËË' (
	ReadToEnd
ËË( 1
(
ËË1 2
)
ËË2 3
;
ËË3 4
}
ÈÈ 
}
ÍÍ 
}
ÎÎ 
catch
ÏÏ 
(
ÏÏ 
	Exception
ÏÏ 
	exception
ÏÏ &
)
ÏÏ& '
{
ÌÌ 
throw
ÓÓ 
new
ÓÓ '
InvalidOperationException
ÓÓ 3
(
ÓÓ3 4
$str
ÓÓ4 S
,
ÓÓS T
	exception
ÓÓU ^
)
ÓÓ^ _
;
ÓÓ_ `
}
ÔÔ 
finally
 
{
ÒÒ 
if
ÚÚ 
(
ÚÚ 
created
ÚÚ 
)
ÚÚ 
{
ÛÛ 
provider
ÙÙ 
?
ÙÙ 
.
ÙÙ 
Dispose
ÙÙ %
(
ÙÙ% &
)
ÙÙ& '
;
ÙÙ' (
}
ıı 
}
ˆˆ 
return
˜˜ 
target
˜˜ 
;
˜˜ 
}
¯¯ 	
private
˙˙ 
static
˙˙ 
byte
˙˙ 
[
˙˙ 
]
˙˙ 
Decrypt
˙˙ %
(
˙˙% &
byte
˙˙& *
[
˙˙* +
]
˙˙+ ,
text
˙˙- 1
,
˙˙1 2
string
˙˙3 9
key
˙˙: =
,
˙˙= > 
SymmetricAlgorithm
˙˙? Q
provider
˙˙R Z
)
˙˙Z [
{
˚˚ 	
var
¸¸ 
created
¸¸ 
=
¸¸ 
false
¸¸ 
;
¸¸  
try
˝˝ 
{
˛˛ 
if
ˇˇ 
(
ˇˇ 
provider
ˇˇ 
==
ˇˇ 
null
ˇˇ  $
)
ˇˇ$ %
{
ÄÄ 
provider
ÅÅ 
=
ÅÅ 
Aes
ÅÅ "
.
ÅÅ" #
Create
ÅÅ# )
(
ÅÅ) *
)
ÅÅ* +
;
ÅÅ+ ,
created
ÇÇ 
=
ÇÇ 
true
ÇÇ "
;
ÇÇ" #
}
ÉÉ 
provider
ÑÑ 
.
ÑÑ 
SetValidKey
ÑÑ $
(
ÑÑ$ %
key
ÑÑ% (
)
ÑÑ( )
;
ÑÑ) *
provider
ÖÖ 
.
ÖÖ 

SetValidIV
ÖÖ #
(
ÖÖ# $
key
ÖÖ$ '
)
ÖÖ' (
;
ÖÖ( )
using
áá 
(
áá 
var
áá 
memoryStream
áá '
=
áá( )
new
áá* -
MemoryStream
áá. :
(
áá: ;
)
áá; <
)
áá< =
{
àà 
using
ââ 
(
ââ 
var
ââ 
cryptoStream
ââ +
=
ââ, -
new
ââ. 1
CryptoStream
ââ2 >
(
ââ> ?
memoryStream
ââ? K
,
ââK L
provider
ââM U
?
ââU V
.
ââV W
CreateDecryptor
ââW f
(
ââf g
)
ââg h
,
ââh i
CryptoStreamMode
ââj z
.
ââz {
Writeââ{ Ä
)ââÄ Å
)ââÅ Ç
{
ää 
cryptoStream
ãã $
.
ãã$ %
Write
ãã% *
(
ãã* +
text
ãã+ /
,
ãã/ 0
$num
ãã1 2
,
ãã2 3
text
ãã4 8
.
ãã8 9
Length
ãã9 ?
)
ãã? @
;
ãã@ A
cryptoStream
åå $
.
åå$ %
FlushFinalBlock
åå% 4
(
åå4 5
)
åå5 6
;
åå6 7
return
çç 
memoryStream
çç +
.
çç+ ,
ToArray
çç, 3
(
çç3 4
)
çç4 5
;
çç5 6
}
éé 
}
èè 
}
êê 
catch
ëë 
(
ëë 
	Exception
ëë 
	exception
ëë &
)
ëë& '
{
íí 
throw
ìì 
new
ìì '
InvalidOperationException
ìì 3
(
ìì3 4
$str
ìì4 R
,
ììR S
	exception
ììT ]
)
ìì] ^
;
ìì^ _
}
îî 
finally
ïï 
{
ññ 
if
óó 
(
óó 
created
óó 
)
óó 
{
òò 
provider
ôô 
?
ôô 
.
ôô 
Dispose
ôô %
(
ôô% &
)
ôô& '
;
ôô' (
}
öö 
}
õõ 
}
úú 	
}
üü 
}†† ã
MC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\BaseContractResolver.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Serialization %
{ 
public 

class  
BaseContractResolver %
:& '2
&CamelCasePropertyNamesContractResolver( N
{ 
	protected 
override 
JsonProperty '
CreateProperty( 6
(6 7

MemberInfo7 A
memberB H
,H I
MemberSerializationJ ]
memberSerialization^ q
)q r
{ 	
var 
prop 
= 
base 
. 
CreateProperty *
(* +
member+ 1
,1 2
memberSerialization3 F
)F G
;G H
if 
( 
( 
member 
as 
PropertyInfo '
)' (
.( )
GetCustomAttributes) <
<< =
IgnoreAttribute= L
>L M
(M N
)N O
.O P
AnyP S
(S T
)T U
)U V
{   
prop!! 
.!! 
Ignored!! 
=!! 
true!! #
;!!# $
return"" 
prop"" 
;"" 
}## 
if$$ 
($$ 
($$ 
member$$ 
as$$ 
PropertyInfo$$ '
)$$' (
?$$( )
.$$) *
GetCustomAttributes$$* =
<$$= >
SecureAttribute$$> M
>$$M N
($$N O
)$$O P
.$$P Q
Any$$Q T
($$T U
)$$U V
??$$W Y
false$$Z _
)$$_ `
{%% 
prop&& 
.&& 
	Converter&& 
=&&  
new&&! $
SecureJsonConverter&&% 8
(&&8 9
)&&9 :
;&&: ;
}'' 
return(( 
prop(( 
;(( 
})) 	
}** 
}++ —
QC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\ClaimsPrincipalConverter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Serialization %
{ 
public 

class $
ClaimsPrincipalConverter )
:* +
JsonConverter, 9
{ 
public 
override 
bool 

CanConvert '
(' (
Type( ,

objectType- 7
)7 8
{ 	
return 
typeof 
( 
ClaimsPrincipal )
)) *
==+ -

objectType. 8
;8 9
} 	
public(( 
override(( 
object(( 
ReadJson(( '
(((' (

JsonReader((( 2
reader((3 9
,((9 :
Type((; ?

objectType((@ J
,((J K
object((L R
existingValue((S `
,((` a
JsonSerializer((b p

serializer((q {
)(({ |
{)) 	
var** 
source** 
=** 

serializer** #
.**# $
Deserialize**$ /
<**/ 0!
ClaimsPrincipalHolder**0 E
>**E F
(**F G
reader**G M
)**M N
;**N O
if++ 
(++ 
source++ 
==++ 
null++ 
)++ 
{,, 
return-- 
null-- 
;-- 
}.. 
var00 
claims00 
=00 
source00 
.00  
Claims00  &
.00& '
Select00' -
(00- .
x00. /
=>000 2
new003 6
Claim007 <
(00< =
x00= >
.00> ?
Type00? C
,00C D
x00E F
.00F G
Value00G L
)00L M
)00M N
;00N O
var11 
id11 
=11 
new11 
ClaimsIdentity11 '
(11' (
claims11( .
,11. /
source110 6
.116 7
AuthenticationType117 I
)11I J
;11J K
var22 
target22 
=22 
new22 
ClaimsPrincipal22 ,
(22, -
id22- /
)22/ 0
;220 1
return33 
target33 
;33 
}44 	
public<< 
override<< 
void<< 
	WriteJson<< &
(<<& '

JsonWriter<<' 1
writer<<2 8
,<<8 9
object<<: @
value<<A F
,<<F G
JsonSerializer<<H V

serializer<<W a
)<<a b
{== 	
var>> 
source>> 
=>> 
(>> 
ClaimsPrincipal>> )
)>>) *
value>>+ 0
;>>0 1
var@@ 
target@@ 
=@@ 
new@@ !
ClaimsPrincipalHolder@@ 2
(@@2 3
source@@3 9
)@@9 :
;@@: ;

serializerBB 
.BB 
	SerializeBB  
(BB  !
writerBB! '
,BB' (
targetBB) /
)BB/ 0
;BB0 1
}CC 	
}DD 
}EE €$
PC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\DefaultContractResolver.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Serialization %
{ 
public 

class #
DefaultContractResolver (
:) * 
BaseContractResolver+ ?
{ 
	protected 
override 
JsonProperty '
CreateProperty( 6
(6 7

MemberInfo7 A
memberB H
,H I
MemberSerializationJ ]
memberSerialization^ q
)q r
{ 	
var 
prop 
= 
base 
. 
CreateProperty *
(* +
member+ 1
,1 2
memberSerialization3 F
)F G
;G H
var   
property   
=   
member   !
as  " $
PropertyInfo  % 1
;  1 2
if!! 
(!! 
property!! 
!=!! 
null!!  
)!!  !
{"" 
if## 
(## 
!## 
prop## 
.## 
Writable## "
)##" #
{$$ 
var%% 
hasPrivateSetter%% (
=%%) *
property%%+ 3
.%%3 4
GetSetMethod%%4 @
(%%@ A
true%%A E
)%%E F
!=%%G I
null%%J N
;%%N O
prop&& 
.&& 
Writable&& !
=&&" #
hasPrivateSetter&&$ 4
;&&4 5
}'' 
if(( 
((( 
prop(( 
.(( 
PropertyType(( %
==((& (
typeof(() /
(((/ 0
ClaimsPrincipal((0 ?
)((? @
)((@ A
{)) 
prop** 
.** 
	Converter** "
=**# $
new**% ($
ClaimsPrincipalConverter**) A
(**A B
)**B C
;**C D
}++ 
var,, !
isDefaultValueIgnored,, )
=,,* +
(,,, -
(,,- .
prop,,. 2
.,,2 3 
DefaultValueHandling,,3 G
??,,H J 
DefaultValueHandling,,K _
.,,_ `
Ignore,,` f
),,f g
&,,h i 
DefaultValueHandling,,j ~
.,,~ 
Ignore	,, Ö
)
,,Ö Ü
!=
,,á â
$num
,,ä ã
;
,,ã å
if-- 
(-- !
isDefaultValueIgnored-- )
)--) *
{.. 
	Predicate// 
<// 
object// $
>//$ %
newShouldSerialize//& 8
=//9 :
obj//; >
=>//? A
{00 
var11 
value11 !
=11" #
prop11$ (
.11( )
ValueProvider11) 6
.116 7
GetValue117 ?
(11? @
obj11@ C
)11C D
;11D E
if22 
(22 
value22 !
==22" $
null22% )
)22) *
{33 
return44 "
false44# (
;44( )
}55 
if66 
(66 
!66 
typeof66 #
(66# $
string66$ *
)66* +
.66+ ,
IsAssignableFrom66, <
(66< =
property66= E
.66E F
PropertyType66F R
)66R S
&&66T V
typeof66W ]
(66] ^
IEnumerable66^ i
)66i j
.66j k
IsAssignableFrom66k {
(66{ |
property	66| Ñ
.
66Ñ Ö
PropertyType
66Ö ë
)
66ë í
)
66í ì
{77 
var88 

collection88  *
=88+ ,
value88- 2
as883 5
ICollection886 A
;88A B
return99 "

collection99# -
==99. 0
null991 5
||996 8

collection999 C
.99C D
Count99D I
!=99J L
$num99M N
;99N O
}:: 
return;; 
true;; #
;;;# $
}<< 
;<< 
var>> 
oldShouldSerialize>> *
=>>+ ,
prop>>- 1
.>>1 2
ShouldSerialize>>2 A
;>>A B
prop?? 
.?? 
ShouldSerialize?? (
=??) *
oldShouldSerialize??+ =
!=??> @
null??A E
???F G
o??H I
=>??J L
oldShouldSerialize??M _
(??_ `
o??` a
)??a b
&&??c e
newShouldSerialize??f x
(??x y
o??y z
)??z {
:??| }
newShouldSerialize	??~ ê
;
??ê ë
}@@ 
}AA 
returnCC 
propCC 
;CC 
}DD 	
}EE 
}FF ≈
UC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\DefaultSerializationSettings.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Serialization

 %
{ 
public 

class (
DefaultSerializationSettings -
:. /"
JsonSerializerSettings0 F
{ 
public (
DefaultSerializationSettings +
(+ ,
), -
{ 	
this 
. 

Formatting 
= 

Formatting (
.( )
Indented) 1
;1 2
this 
. 
NullValueHandling "
=# $
NullValueHandling% 6
.6 7
Ignore7 =
;= >
this 
. !
ReferenceLoopHandling &
=' (!
ReferenceLoopHandling) >
.> ?
Ignore? E
;E F
this 
. 
ContractResolver !
=" #
new$ '#
DefaultContractResolver( ?
(? @
)@ A
;A B
this 
. 

Converters 
. 
Add 
(  
new  #$
ClaimsPrincipalConverter$ <
(< =
)= >
)> ?
;? @
} 	
public"" 
static"" "
JsonSerializerSettings"" ,
Instance""- 5
=>""6 8
new""9 <(
DefaultSerializationSettings""= Y
(""Y Z
)""Z [
;""[ \
}## 
}$$ ë
HC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\IgnoreAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Serialization

 %
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

sealed 
class 
IgnoreAttribute '
:( )
	Attribute* 3
{ 
} 
} ç
JC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\Model\ClaimHolder.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Serialization

 %
.

% &
Model

& +
{ 
public 

class 
ClaimHolder 
{ 
public 
string 
Type 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Value 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ®
TC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\Model\ClaimsPrincipalHolder.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Serialization %
.% &
Model& +
{ 
public 

class !
ClaimsPrincipalHolder &
{ 
public !
ClaimsPrincipalHolder $
($ %
)% &
{ 	
} 	
public !
ClaimsPrincipalHolder $
($ %
ClaimsPrincipal% 4
source5 ;
); <
{   	
Argument!! 
.!! 
NotNull!! 
(!! 
source!! #
,!!# $
nameof!!% +
(!!+ ,
source!!, 2
)!!2 3
)!!3 4
;!!4 5
this## 
.## 
AuthenticationType## #
=##$ %
source##& ,
.##, -
Identity##- 5
.##5 6
AuthenticationType##6 H
;##H I
this$$ 
.$$ 
Claims$$ 
=$$ 
source$$  
.$$  !
Claims$$! '
.$$' (
Select$$( .
($$. /
x$$/ 0
=>$$1 3
new$$4 7
ClaimHolder$$8 C
{$$D E
Type$$E I
=$$J K
x$$L M
.$$M N
Type$$N R
,$$R S
Value$$T Y
=$$Z [
x$$\ ]
.$$] ^
Value$$^ c
}$$c d
)$$d e
.$$e f
ToArray$$f m
($$m n
)$$n o
;$$o p
}%% 	
public++ 
string++ 
AuthenticationType++ (
{++) *
get+++ .
;++. /
set++0 3
;++3 4
}++5 6
public11 
ClaimHolder11 
[11 
]11 
Claims11 #
{11$ %
get11& )
;11) *
set11+ .
;11. /
}110 1
}22 
}33 ú
HC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\SecureAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Serialization

 %
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

sealed 
class 
SecureAttribute '
:( )
	Attribute* 3
{ 
public 
const 
string 
DefaultDisplayText .
=/ 0
$str1 ;
;; <
} 
} ≥
LC:\Source\Stacks\Core\src\Slalom.Stacks\Serialization\SecureJsonConverter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Serialization %
{ 
internal 
class 
SecureJsonConverter &
:' (
JsonConverter) 6
{ 
public 
override 
bool 

CanConvert '
(' (
Type( ,

objectType- 7
)7 8
{ 	
return 
true 
; 
} 	
public&& 
override&& 
object&& 
ReadJson&& '
(&&' (

JsonReader&&( 2
reader&&3 9
,&&9 :
Type&&; ?

objectType&&@ J
,&&J K
object&&L R
existingValue&&S `
,&&` a
JsonSerializer&&b p

serializer&&q {
)&&{ |
{'' 	
throw(( 
new(( #
NotImplementedException(( -
(((- .
)((. /
;((/ 0
})) 	
public11 
override11 
void11 
	WriteJson11 &
(11& '

JsonWriter11' 1
writer112 8
,118 9
object11: @
value11A F
,11F G
JsonSerializer11H V

serializer11W a
)11a b
{22 	
writer33 
.33 

WriteValue33 
(33 
SecureAttribute33 -
.33- .
DefaultDisplayText33. @
)33@ A
;33A B
}44 	
}55 
}66 ®
MC:\Source\Stacks\Core\src\Slalom.Stacks\Services\DependencyFailedException.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

class %
DependencyFailedException *
:+ ,
	Exception- 6
{ 
public %
DependencyFailedException (
(( )
Request) 0
request1 8
,8 9
MessageResult: G

dependencyH R
)R S
: 
base 
( 
$" '
Failed to complete request  0
{0 1
request1 8
.8 9
Message9 @
.@ A
IdA C
}C D3
' because of a failed dependent request D k
{k l

dependencyl v
.v w
	RequestId	w Ä
}
Ä Å
.
Å Ç
"
Ç É
,
É Ñ

dependency
Ö è
.
è ê
RaisedException
ê ü
??
† ¢
new
£ ¶!
ValidationException
ß ∫
(
∫ ª

dependency
ª ≈
.
≈ ∆
ValidationErrors
∆ ÷
.
÷ ◊
ToArray
◊ ﬁ
(
ﬁ ﬂ
)
ﬂ ‡
)
‡ ·
)
· ‚
{ 	
this 
. 
Request 
= 
request "
;" #
this 
. 

Dependency 
= 

dependency (
;( )
} 	
public%% 
MessageResult%% 

Dependency%% '
{%%( )
get%%* -
;%%- .
}%%/ 0
public++ 
Request++ 
Request++ 
{++  
get++! $
;++$ %
}++& '
},, 
}-- †m
<C:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoint.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

abstract 
class 
BaseEndPoint &
:' (
	IEndPoint) 2
{ 
public 
IDomainFacade 
Domain #
=>$ &
this' +
.+ ,

Components, 6
.6 7
Resolve7 >
<> ?
IDomainFacade? L
>L M
(M N
)N O
;O P
public$$ 
ISearchFacade$$ 
Search$$ #
=>$$$ &
this$$' +
.$$+ ,

Components$$, 6
.$$6 7
Resolve$$7 >
<$$> ?
ISearchFacade$$? L
>$$L M
($$M N
)$$N O
;$$O P
	protected** 
internal** 
ExecutionContext** +
Context**, 3
=>**4 6
(**7 8
(**8 9
	IEndPoint**9 B
)**B C
this**D H
)**H I
.**I J
Context**J Q
;**Q R
internal00 
IComponentContext00 "

Components00# -
{00. /
get000 3
;003 4
set005 8
;008 9
}00: ;
ExecutionContext22 
	IEndPoint22 "
.22" #
Context22# *
{22+ ,
get22- 0
;220 1
set222 5
;225 6
}227 8
public88 
Request88 
Request88 
=>88 !
this88" &
.88& '
Context88' .
.88. /
Request88/ 6
;886 7
public== 
virtual== 
void== 
OnStart== #
(==# $
)==$ %
{>> 	
}?? 	
publicEE 
voidEE 
AddRaisedEventEE "
(EE" #
EventEE# (
instanceEE) 1
)EE1 2
{FF 	
thisGG 
.GG 
ContextGG 
.GG 
AddRaisedEventGG '
(GG' (
instanceGG( 0
)GG0 1
;GG1 2
}HH 	
publicOO 
TaskOO 
<OO 
MessageResultOO !
>OO! "
SendOO# '
(OO' (
objectOO( .
messageOO/ 6
)OO6 7
{PP 	
varQQ 
	attributeQQ 
=QQ 
messageQQ #
.QQ# $
GetTypeQQ$ +
(QQ+ ,
)QQ, -
.QQ- .
GetAllAttributesQQ. >
<QQ> ?
RequestAttributeQQ? O
>QQO P
(QQP Q
)QQQ R
.QQR S
FirstOrDefaultQQS a
(QQa b
)QQb c
;QQc d
ifRR 
(RR 
	attributeRR 
!=RR 
nullRR !
)RR! "
{SS 
returnTT 
thisTT 
.TT 

ComponentsTT &
.TT& '
ResolveTT' .
<TT. /
IMessageGatewayTT/ >
>TT> ?
(TT? @
)TT@ A
.TTA B
SendTTB F
(TTF G
	attributeTTG P
.TTP Q
PathTTQ U
,TTU V
messageTTW ^
,TT^ _
thisTT` d
.TTd e
ContextTTe l
)TTl m
;TTm n
}UU 
returnVV 
thisVV 
.VV 

ComponentsVV "
.VV" #
ResolveVV# *
<VV* +
IMessageGatewayVV+ :
>VV: ;
(VV; <
)VV< =
.VV= >
SendVV> B
(VVB C
messageVVC J
,VVJ K
thisVVL P
.VVP Q
ContextVVQ X
)VVX Y
;VVY Z
}WW 	
public^^ 
async^^ 
Task^^ 
<^^ 
MessageResult^^ '
<^^' (
T^^( )
>^^) *
>^^* +
Send^^, 0
<^^0 1
T^^1 2
>^^2 3
(^^3 4
object^^4 :
message^^; B
)^^B C
{__ 	
var`` 
result`` 
=`` 
await`` 
this`` #
.``# $
Send``$ (
(``( )
message``) 0
)``0 1
;``1 2
returnbb 
newbb 
MessageResultbb $
<bb$ %
Tbb% &
>bb& '
(bb' (
resultbb( .
)bb. /
;bb/ 0
}cc 	
}dd 
publicjj 

abstractjj 
classjj 
EndPointjj "
:jj# $
BaseEndPointjj% 1
,jj1 2
	IEndPointjj3 <
<jj< =
objectjj= C
>jjC D
{kk 
asyncll 
Taskll 
	IEndPointll 
<ll 
objectll #
>ll# $
.ll$ %
Receivell% ,
(ll, -
objectll- 3
instancell4 <
)ll< =
{mm 	
awaitnn 
thisnn 
.nn 

Componentsnn !
.nn! "
Resolvenn" )
<nn) *
ValidateMessagenn* 9
>nn9 :
(nn: ;
)nn; <
.nn< =
Executenn= D
(nnD E
thisnnE I
.nnI J
ContextnnJ Q
)nnQ R
;nnR S
ifpp 
(pp 
!pp 
thispp 
.pp 
Contextpp 
.pp 
ValidationErrorspp .
.pp. /
Anypp/ 2
(pp2 3
)pp3 4
)pp4 5
{qq 
tryrr 
{ss 
iftt 
(tt 
!tt 
thistt 
.tt 
Contexttt %
.tt% &
CancellationTokentt& 7
.tt7 8#
IsCancellationRequestedtt8 O
)ttO P
{uu 
awaitvv 
thisvv "
.vv" #
ReceiveAsyncvv# /
(vv/ 0
)vv0 1
;vv1 2
}ww 
}xx 
catchyy 
(yy 
	Exceptionyy  
	exceptionyy! *
)yy* +
{zz 
this{{ 
.{{ 
Context{{  
.{{  !
SetException{{! -
({{- .
	exception{{. 7
){{7 8
;{{8 9
}|| 
}}} 
}~~ 	
public
ÉÉ 
virtual
ÉÉ 
void
ÉÉ 
Receive
ÉÉ #
(
ÉÉ# $
)
ÉÉ$ %
{
ÑÑ 	
throw
ÖÖ 
new
ÖÖ %
NotImplementedException
ÖÖ -
(
ÖÖ- .
)
ÖÖ. /
;
ÖÖ/ 0
}
ÜÜ 	
public
ãã 
virtual
ãã 
Task
ãã 
ReceiveAsync
ãã (
(
ãã( )
)
ãã) *
{
åå 	
this
çç 
.
çç 
Receive
çç 
(
çç 
)
çç 
;
çç 
return
éé 
Task
éé 
.
éé 

FromResult
éé "
(
éé" #
$num
éé# $
)
éé$ %
;
éé% &
}
èè 	
	protected
ïï 
void
ïï 
Respond
ïï 
(
ïï 
object
ïï %
instance
ïï& .
)
ïï. /
{
ññ 	
this
óó 
.
óó 
Context
óó 
.
óó 
Response
óó !
=
óó" #
instance
óó$ ,
;
óó, -
}
òò 	
}
ôô 
public
üü 

abstract
üü 
class
üü 
EndPoint
üü "
<
üü" #
TMessage
üü# +
>
üü+ ,
:
üü- .
BaseEndPoint
üü/ ;
,
üü; <
	IEndPoint
üü= F
<
üüF G
TMessage
üüG O
>
üüO P
{
†† 
async
°° 
Task
°° 
	IEndPoint
°° 
<
°° 
TMessage
°° %
>
°°% &
.
°°& '
Receive
°°' .
(
°°. /
TMessage
°°/ 7
instance
°°8 @
)
°°@ A
{
¢¢ 	
await
££ 
this
££ 
.
££ 

Components
££ !
.
££! "
Resolve
££" )
<
££) *
ValidateMessage
££* 9
>
££9 :
(
££: ;
)
££; <
.
££< =
Execute
££= D
(
££D E
this
££E I
.
££I J
Context
££J Q
)
££Q R
;
££R S
if
•• 
(
•• 
!
•• 
this
•• 
.
•• 
Context
•• 
.
•• 
ValidationErrors
•• .
.
••. /
Any
••/ 2
(
••2 3
)
••3 4
)
••4 5
{
¶¶ 
try
ßß 
{
®® 
if
©© 
(
©© 
!
©© 
this
©© 
.
©© 
Context
©© %
.
©©% &
CancellationToken
©©& 7
.
©©7 8%
IsCancellationRequested
©©8 O
)
©©O P
{
™™ 
await
´´ 
this
´´ "
.
´´" #
ReceiveAsync
´´# /
(
´´/ 0
instance
´´0 8
)
´´8 9
;
´´9 :
}
¨¨ 
}
≠≠ 
catch
ÆÆ 
(
ÆÆ 
	Exception
ÆÆ  
	exception
ÆÆ! *
)
ÆÆ* +
{
ØØ 
this
∞∞ 
.
∞∞ 
Context
∞∞  
.
∞∞  !
SetException
∞∞! -
(
∞∞- .
	exception
∞∞. 7
)
∞∞7 8
;
∞∞8 9
}
±± 
}
≤≤ 
}
≥≥ 	
public
∏∏ 
virtual
∏∏ 
void
∏∏ 
Receive
∏∏ #
(
∏∏# $
TMessage
∏∏$ ,
instance
∏∏- 5
)
∏∏5 6
{
ππ 	
throw
∫∫ 
new
∫∫ %
NotImplementedException
∫∫ -
(
∫∫- .
)
∫∫. /
;
∫∫/ 0
}
ªª 	
public
¿¿ 
virtual
¿¿ 
Task
¿¿ 
ReceiveAsync
¿¿ (
(
¿¿( )
TMessage
¿¿) 1
instance
¿¿2 :
)
¿¿: ;
{
¡¡ 	
this
¬¬ 
.
¬¬ 
Receive
¬¬ 
(
¬¬ 
instance
¬¬ !
)
¬¬! "
;
¬¬" #
return
ƒƒ 
Task
ƒƒ 
.
ƒƒ 

FromResult
ƒƒ "
(
ƒƒ" #
$num
ƒƒ# $
)
ƒƒ$ %
;
ƒƒ% &
}
≈≈ 	
}
∆∆ 
public
ÃÃ 

abstract
ÃÃ 
class
ÃÃ 
EndPoint
ÃÃ "
<
ÃÃ" #
TMessage
ÃÃ# +
,
ÃÃ+ ,
	TResponse
ÃÃ- 6
>
ÃÃ6 7
:
ÃÃ8 9
BaseEndPoint
ÃÃ: F
,
ÃÃF G
	IEndPoint
ÃÃH Q
<
ÃÃQ R
TMessage
ÃÃR Z
,
ÃÃZ [
	TResponse
ÃÃ\ e
>
ÃÃe f
{
ÕÕ 
async
ŒŒ 
Task
ŒŒ 
<
ŒŒ 
	TResponse
ŒŒ 
>
ŒŒ 
	IEndPoint
ŒŒ '
<
ŒŒ' (
TMessage
ŒŒ( 0
,
ŒŒ0 1
	TResponse
ŒŒ2 ;
>
ŒŒ; <
.
ŒŒ< =
Receive
ŒŒ= D
(
ŒŒD E
TMessage
ŒŒE M
instance
ŒŒN V
)
ŒŒV W
{
œœ 	
await
–– 
this
–– 
.
–– 

Components
–– !
.
––! "
Resolve
––" )
<
––) *
ValidateMessage
––* 9
>
––9 :
(
––: ;
)
––; <
.
––< =
Execute
––= D
(
––D E
this
––E I
.
––I J
Context
––J Q
)
––Q R
;
––R S
var
““ 
result
““ 
=
““ 
default
““  
(
““  !
	TResponse
““! *
)
““* +
;
““+ ,
if
”” 
(
”” 
!
”” 
this
”” 
.
”” 
Context
”” 
.
”” 
ValidationErrors
”” .
.
””. /
Any
””/ 2
(
””2 3
)
””3 4
)
””4 5
{
‘‘ 
try
’’ 
{
÷÷ 
if
◊◊ 
(
◊◊ 
!
◊◊ 
this
◊◊ 
.
◊◊ 
Context
◊◊ %
.
◊◊% &
CancellationToken
◊◊& 7
.
◊◊7 8%
IsCancellationRequested
◊◊8 O
)
◊◊O P
{
ÿÿ 
result
ŸŸ 
=
ŸŸ  
await
ŸŸ! &
this
ŸŸ' +
.
ŸŸ+ ,
ReceiveAsync
ŸŸ, 8
(
ŸŸ8 9
instance
ŸŸ9 A
)
ŸŸA B
;
ŸŸB C
this
€€ 
.
€€ 
Context
€€ $
.
€€$ %
Response
€€% -
=
€€. /
result
€€0 6
;
€€6 7
if
›› 
(
›› 
result
›› "
is
››# %
Event
››& +
)
››+ ,
{
ﬁﬁ 
this
ﬂﬂ  
.
ﬂﬂ  !
AddRaisedEvent
ﬂﬂ! /
(
ﬂﬂ/ 0
result
ﬂﬂ0 6
as
ﬂﬂ7 9
Event
ﬂﬂ: ?
)
ﬂﬂ? @
;
ﬂﬂ@ A
}
‡‡ 
}
·· 
}
‚‚ 
catch
„„ 
(
„„ 
	Exception
„„  
	exception
„„! *
)
„„* +
{
‰‰ 
this
ÂÂ 
.
ÂÂ 
Context
ÂÂ  
.
ÂÂ  !
SetException
ÂÂ! -
(
ÂÂ- .
	exception
ÂÂ. 7
)
ÂÂ7 8
;
ÂÂ8 9
}
ÊÊ 
}
ÁÁ 
return
ËË 
result
ËË 
;
ËË 
}
ÈÈ 	
public
 
virtual
 
	TResponse
  
Receive
! (
(
( )
TMessage
) 1
instance
2 :
)
: ;
{
ÒÒ 	
throw
ÚÚ 
new
ÚÚ %
NotImplementedException
ÚÚ -
(
ÚÚ- .
)
ÚÚ. /
;
ÚÚ/ 0
}
ÛÛ 	
public
˙˙ 
virtual
˙˙ 
Task
˙˙ 
<
˙˙ 
	TResponse
˙˙ %
>
˙˙% &
ReceiveAsync
˙˙' 3
(
˙˙3 4
TMessage
˙˙4 <
instance
˙˙= E
)
˙˙E F
{
˚˚ 	
return
¸¸ 
Task
¸¸ 
.
¸¸ 

FromResult
¸¸ "
(
¸¸" #
this
¸¸# '
.
¸¸' (
Receive
¸¸( /
(
¸¸/ 0
instance
¸¸0 8
)
¸¸8 9
)
¸¸9 :
;
¸¸: ;
}
˝˝ 	
}
˛˛ 
}ˇˇ ﬂ
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPointAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
)* +
]+ ,
public 

class 
EndPointAttribute "
:# $
	Attribute% .
{ 
public 
EndPointAttribute  
(  !
string! '
path( ,
), -
{ 	
this 
. 
Path 
= 
path 
; 
} 	
public"" 
string"" 
Method"" 
{"" 
get"" "
;""" #
set""$ '
;""' (
}"") *
=""+ ,
$str""- 3
;""3 4
public(( 
string(( 
Name(( 
{(( 
get((  
;((  !
set((" %
;((% &
}((' (
public.. 
string.. 
Path.. 
{.. 
get..  
;..  !
set.." %
;..% &
}..' (
public44 
bool44 
Public44 
{44 
get44  
;44  !
set44" %
;44% &
}44' (
=44) *
true44+ /
;44/ 0
public;; 
bool;; 
Secure;; 
{;; 
get;;  
;;;  !
set;;" %
;;;% &
};;' (
publicCC 
stringCC 
[CC 
]CC 
TagsCC 
{CC 
getCC "
;CC" #
setCC$ '
;CC' (
}CC) *
publicII 
doubleII 
TimeoutII 
{II 
getII  #
;II# $
setII% (
;II( )
}II* +
publicOO 
intOO 
VersionOO 
{OO 
getOO  
;OO  !
setOO" %
;OO% &
}OO' (
=OO) *
$numOO+ ,
;OO, -
}PP 
}QQ ¥
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\CheckHealth.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{		 
[ 
EndPoint 
( 
$str 
, 
Method  &
=' (
$str) .
). /
]/ 0
public 

class 
CheckHealth 
: 
EndPoint '
{ 
public 
override 
void 
Receive $
($ %
)% &
{ 	
} 	
} 
} ¸
GC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetEvents.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{ 
[ 
EndPoint 
( 
$str 
, 
Method  &
=' (
$str) .
). /
]/ 0
public 

class 
	GetEvents 
: 
EndPoint %
<% &
GetEventsRequest& 6
,6 7
IEnumerable8 C
<C D

EventEntryD N
>N O
>O P
{ 
private 
readonly 
IEventStore $
_events% ,
;, -
public 
	GetEvents 
( 
IEventStore $
events% +
)+ ,
{ 	
Argument 
. 
NotNull 
( 
events #
,# $
nameof% +
(+ ,
events, 2
)2 3
)3 4
;4 5
_events 
= 
events 
; 
}   	
public'' 
override'' 
Task'' 
<'' 
IEnumerable'' (
<''( )

EventEntry'') 3
>''3 4
>''4 5
ReceiveAsync''6 B
(''B C
GetEventsRequest''C S
instance''T \
)''\ ]
{(( 	
return)) 
_events)) 
.)) 
	GetEvents)) $
())$ %
instance))% -
.))- .
Start)). 3
,))3 4
instance))5 =
.))= >
End))> A
)))A B
;))B C
}** 	
}++ 
},, ¬
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetEventsRequest.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	EndPoints

! *
{ 
public 

class 
GetEventsRequest !
{ 
public 
DateTimeOffset 
? 
End "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DateTimeOffset 
? 
Start $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ˚
HC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetOpenApi.cs
	namespace		 	
Slalom		
 
.		 
Stacks		 
.		 
Services		  
.		  !
	EndPoints		! *
{

 
[ 
EndPoint 
( 
$str 
, 
Method #
=$ %
$str& +
,+ ,
Name- 1
=2 3
$str4 L
,L M
PublicN T
=U V
falseW \
)\ ]
]] ^
public 

class 

GetOpenApi 
: 
EndPoint &
<& '
GetOpenApiRequest' 8
,8 9
OpenApiDocument: I
>I J
{ 
private 
readonly 
ServiceInventory )
	_services* 3
;3 4
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
public 

GetOpenApi 
( 
ServiceInventory *
services+ 3
,3 4
IConfiguration5 C
configurationD Q
)Q R
{ 	
	_services 
= 
services  
;  !
_configuration 
= 
configuration *
;* +
} 	
public   
override   
OpenApiDocument   '
Receive  ( /
(  / 0
GetOpenApiRequest  0 A
instance  B J
)  J K
{!! 	
var"" 
document"" 
="" 
new"" 
OpenApiDocument"" .
("". /
)""/ 0
;""0 1
document## 
.## 
Load## 
(## 
	_services## #
,### $
instance##% -
.##- .
All##. 1
)##1 2
;##2 3
document$$ 
.$$ 
Host$$ 
=$$ 
instance$$ $
.$$$ %
Host$$% )
;$$) *
if&& 
(&& 
!&& 
String&& 
.&& 
IsNullOrWhiteSpace&& *
(&&* +
instance&&+ 3
.&&3 4
BasePath&&4 <
)&&< =
)&&= >
{'' 
document(( 
.(( 
BasePath(( !
=((" #
instance(($ ,
.((, -
BasePath((- 5
;((5 6
})) 
var++ 
externalDocs++ 
=++ 
new++ "
ExternalDocs++# /
(++/ 0
)++0 1
;++1 2
_configuration,, 
.,, 

GetSection,, %
(,,% &
$str,,& ;
),,; <
.,,< =
Bind,,= A
(,,A B
externalDocs,,B N
),,N O
;,,O P
if-- 
(-- 
!-- 
String-- 
.-- 
IsNullOrWhiteSpace-- *
(--* +
externalDocs--+ 7
.--7 8
Url--8 ;
)--; <
)--< =
{.. 
document// 
.// 
ExternalDocs// %
=//& '
externalDocs//( 4
;//4 5
}00 
var22 
tags22 
=22 
new22 
List22 
<22  
Tag22  #
>22# $
(22$ %
)22% &
;22& '
_configuration33 
.33 

GetSection33 %
(33% &
$str33& 3
)333 4
.334 5
Bind335 9
(339 :
tags33: >
)33> ?
;33? @
if44 
(44 
tags44 
.44 
Any44 
(44 
)44 
)44 
{55 
document66 
.66 
Tags66 
.66 
AddRange66 &
(66& '
tags66' +
)66+ ,
;66, -
}77 
return99 
document99 
;99 
}:: 	
};; 
}<< ¡

OC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetOpenApiRequest.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{ 
public 

class 
GetOpenApiRequest "
{		 
public 
GetOpenApiRequest  
(  !
string! '
host( ,
,, -
string. 4
basePath5 =
=> ?
null@ D
,D E
boolF J
allK N
=O P
falseQ V
)V W
{ 	
this 
. 
Host 
= 
host 
; 
this 
. 
All 
= 
all 
; 
this 
. 
BasePath 
= 
basePath $
;$ %
} 	
public 
bool 
All 
{ 
get 
; 
}  
public%% 
string%% 
BasePath%% 
{%%  
get%%! $
;%%$ %
}%%& '
public-- 
string-- 
Host-- 
{-- 
get--  
;--  !
}--" #
}.. 
}// Ó
PC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetRemoteEndPoints.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{ 
[ 
EndPoint 
( 
$str (
,( )
Method* 0
=1 2
$str3 8
,8 9
Public: @
=A B
falseC H
)H I
]I J
public 

class 
GetRemoteEndPoints #
:$ %
EndPoint& .
{ 
private 
readonly "
RemoteServiceInventory /

_inventory0 :
;: ;
public 
GetRemoteEndPoints !
(! ""
RemoteServiceInventory" 8
	inventory9 B
)B C
{ 	
Argument 
. 
NotNull 
( 
	inventory &
,& '
nameof( .
(. /
	inventory/ 8
)8 9
)9 :
;: ;

_inventory 
= 
	inventory "
;" #
} 	
public"" 
override"" 
void"" 
Receive"" $
(""$ %
)""% &
{## 	
this$$ 
.$$ 
Respond$$ 
($$ 

_inventory$$ #
.$$# $
Services$$$ ,
.$$, -
Select$$- 3
($$3 4
e$$4 5
=>$$6 8
new$$9 <
{%% 
e&& 
.&& 
Path&& 
,&& 
e'' 
.'' 
	EndPoints'' 
}(( 
)(( 
)(( 
;(( 
})) 	
}** 
}++ ﬂ
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetRequests.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{ 
[ 
EndPoint 
( 
$str  
,  !
Method" (
=) *
$str+ 0
)0 1
]1 2
public 

class 
GetRequests 
: 
EndPoint '
<' (
GetRequestsRequest( :
,: ;
IEnumerable< G
<G H
RequestEntryH T
>T U
>U V
{ 
private 
readonly 
IRequestLog $
_source% ,
;, -
public 
GetRequests 
( 
IRequestLog &
source' -
)- .
{ 	
Argument 
. 
NotNull 
( 
source #
,# $
nameof% +
(+ ,
source, 2
)2 3
)3 4
;4 5
_source   
=   
source   
;   
}!! 	
public(( 
override(( 
async(( 
Task(( "
<((" #
IEnumerable((# .
<((. /
RequestEntry((/ ;
>((; <
>((< =
ReceiveAsync((> J
(((J K
GetRequestsRequest((K ]
instance((^ f
)((f g
{)) 	
var** 
result** 
=** 
await** 
_source** &
.**& '

GetEntries**' 1
(**1 2
instance**2 :
.**: ;
Start**; @
,**@ A
instance**B J
.**J K
End**K N
)**N O
;**O P
return,, 
result,, 
.,, 
Where,, 
(,,  
e,,  !
=>,," $
e,,% &
.,,& '
Path,,' +
==,,, .
null,,/ 3
||,,4 6
!,,7 8
e,,8 9
.,,9 :
Path,,: >
.,,> ?

StartsWith,,? I
(,,I J
$str,,J M
),,M N
),,N O
;,,O P
}-- 	
}.. 
}// ∆
PC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetRequestsRequest.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	EndPoints

! *
{ 
public 

class 
GetRequestsRequest #
{ 
public 
DateTimeOffset 
? 
End "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DateTimeOffset 
? 
Start $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ÷
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetResponses.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	EndPoints! *
{ 
[ 
EndPoint 
( 
$str !
,! "
Method# )
=* +
$str, 1
)1 2
]2 3
public 

class 
GetResponses 
: 
EndPoint  (
<( )
GetResponsesRequest) <
,< =
IEnumerable> I
<I J
ResponseEntryJ W
>W X
>X Y
{ 
private 
readonly 
IResponseLog %
_source& -
;- .
public 
GetResponses 
( 
IResponseLog (
source) /
)/ 0
{ 	
Argument 
. 
NotNull 
( 
source #
,# $
nameof% +
(+ ,
source, 2
)2 3
)3 4
;4 5
_source   
=   
source   
;   
}!! 	
public(( 
override(( 
async(( 
Task(( "
<((" #
IEnumerable((# .
<((. /
ResponseEntry((/ <
>((< =
>((= >
ReceiveAsync((? K
(((K L
GetResponsesRequest((L _
instance((` h
)((h i
{)) 	
var** 
result** 
=** 
await** 
_source** &
.**& '

GetEntries**' 1
(**1 2
instance**2 :
.**: ;
Start**; @
,**@ A
instance**B J
.**J K
End**K N
)**N O
;**O P
return,, 
result,, 
.,, 
Where,, 
(,,  
e,,  !
=>,," $
e,,% &
.,,& '
Path,,' +
==,,, .
null,,/ 3
||,,4 6
!,,7 8
e,,8 9
.,,9 :
Path,,: >
.,,> ?

StartsWith,,? I
(,,I J
$str,,J M
),,M N
||,,O Q
e,,R S
.,,S T
ValidationErrors,,T d
!=,,e g
null,,h l
),,l m
;,,m n
}-- 	
}.. 
}// »
QC:\Source\Stacks\Core\src\Slalom.Stacks\Services\EndPoints\GetResponsesRequest.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	EndPoints

! *
{ 
public 

class 
GetResponsesRequest $
{ 
public 
DateTimeOffset 
? 
End "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DateTimeOffset 
? 
Start $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ∆

=C:\Source\Stacks\Core\src\Slalom.Stacks\Services\IEndPoint.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

	interface 
	IEndPoint 
{ 
ExecutionContext 
Context  
{! "
get# &
;& '
set( +
;+ ,
}- .
Request 
Request 
{ 
get 
; 
}  
void"" 
OnStart"" 
("" 
)"" 
;"" 
}## 
public** 

	interface** 
	IEndPoint** 
<** 
TMessage** '
>**' (
:**) *
	IEndPoint**+ 4
{++ 
Task00 
Receive00 
(00 
TMessage00 
instance00 &
)00& '
;00' (
}11 
public99 

	interface99 
	IEndPoint99 
<99 
TRequest99 '
,99' (
	TResponse99) 2
>992 3
:994 5
	IEndPoint996 ?
{:: 
Task@@ 
<@@ 
	TResponse@@ 
>@@ 
Receive@@ 
(@@  
TRequest@@  (
instance@@) 1
)@@1 2
;@@2 3
}AA 
}BB ¶Q
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\EndPointMetaData.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class 
EndPointMetaData !
{ 
public 
Type 
EndPointType  
{! "
get# &
;& '
set( +
;+ ,
}- .
public!! 

MethodInfo!! 
InvokeMethod!! &
{!!' (
get!!) ,
;!!, -
set!!. 1
;!!1 2
}!!3 4
public)) 
bool)) 
IsVersioned)) 
{))  !
get))" %
;))% &
set))' *
;))* +
})), -
public11 
string11 
Method11 
{11 
get11 "
;11" #
set11$ '
;11' (
}11) *
public99 
string99 
Name99 
{99 
get99  
;99  !
set99" %
;99% &
}99' (
public?? 
string?? 
Path?? 
{?? 
get??  
;??  !
set??" %
;??% &
}??' (
publicEE 
boolEE 
PublicEE 
{EE 
getEE  
;EE  !
setEE" %
;EE% &
}EE' (
publicKK 
TypeKK 
RequestTypeKK 
{KK  !
getKK" %
;KK% &
setKK' *
;KK* +
}KK, -
publicQQ 
TypeQQ 
ResponseTypeQQ  
{QQ! "
getQQ# &
;QQ& '
setQQ( +
;QQ+ ,
}QQ- .
publicWW 
ListWW 
<WW 
EndPointRuleWW  
>WW  !
RulesWW" '
{WW( )
getWW* -
;WW- .
setWW/ 2
;WW2 3
}WW4 5
public]] 
bool]] 
Secure]] 
{]] 
get]]  
;]]  !
set]]" %
;]]% &
}]]' (
publiccc 
stringcc 
Summarycc 
{cc 
getcc  #
;cc# $
setcc% (
;cc( )
}cc* +
publickk 
stringkk 
[kk 
]kk 
Tagskk 
{kk 
getkk "
;kk" #
setkk$ '
;kk' (
}kk) *
publicqq 
TimeSpanqq 
?qq 
Timeoutqq  
{qq! "
getqq# &
;qq& '
setqq( +
;qq+ ,
}qq- .
publicww 
intww 
Versionww 
{ww 
getww  
;ww  !
setww" %
;ww% &
}ww' (
public~~ 
static~~ 
IEnumerable~~ !
<~~! "
EndPointMetaData~~" 2
>~~2 3
Create~~4 :
(~~: ;
Type~~; ?
service~~@ G
)~~G H
{ 	
var
ÄÄ 

interfaces
ÄÄ 
=
ÄÄ 
service
ÄÄ $
.
ÄÄ$ %
GetInterfaces
ÄÄ% 2
(
ÄÄ2 3
)
ÄÄ3 4
.
ÄÄ4 5
Where
ÄÄ5 :
(
ÄÄ: ;
e
ÄÄ; <
=>
ÄÄ= ?
e
ÄÄ@ A
.
ÄÄA B
GetTypeInfo
ÄÄB M
(
ÄÄM N
)
ÄÄN O
.
ÄÄO P
IsGenericType
ÄÄP ]
&&
ÄÄ^ `
(
ÄÄa b
e
ÄÄb c
.
ÄÄc d&
GetGenericTypeDefinition
ÄÄd |
(
ÄÄ| }
)
ÄÄ} ~
==ÄÄ Å
typeofÄÄÇ à
(ÄÄà â
	IEndPointÄÄâ í
<ÄÄí ì
>ÄÄì î
)ÄÄî ï
||ÄÄñ ò
eÄÄô ö
.ÄÄö õ(
GetGenericTypeDefinitionÄÄõ ≥
(ÄÄ≥ ¥
)ÄÄ¥ µ
==ÄÄ∂ ∏
typeofÄÄπ ø
(ÄÄø ¿
	IEndPointÄÄ¿ …
<ÄÄ…  
,ÄÄ  À
>ÄÄÀ Ã
)ÄÄÃ Õ
)ÄÄÕ Œ
)ÄÄŒ œ
.ÄÄœ –
ToListÄÄ– ÷
(ÄÄ÷ ◊
)ÄÄ◊ ÿ
;ÄÄÿ Ÿ
if
ÅÅ 
(
ÅÅ 

interfaces
ÅÅ 
.
ÅÅ 
Any
ÅÅ 
(
ÅÅ 
)
ÅÅ  
)
ÅÅ  !
{
ÇÇ 
var
ÉÉ 
path
ÉÉ 
=
ÉÉ 
service
ÉÉ "
.
ÉÉ" #
GetPath
ÉÉ# *
(
ÉÉ* +
)
ÉÉ+ ,
;
ÉÉ, -
var
ÑÑ 
version
ÑÑ 
=
ÑÑ 
service
ÑÑ %
.
ÑÑ% &

GetVersion
ÑÑ& 0
(
ÑÑ0 1
)
ÑÑ1 2
;
ÑÑ2 3
var
ÖÖ 
summary
ÖÖ 
=
ÖÖ 
service
ÖÖ %
.
ÖÖ% &
GetComments
ÖÖ& 1
(
ÖÖ1 2
)
ÖÖ2 3
;
ÖÖ3 4
var
ÜÜ 
timeout
ÜÜ 
=
ÜÜ 
service
ÜÜ %
.
ÜÜ% &

GetTimeout
ÜÜ& 0
(
ÜÜ0 1
)
ÜÜ1 2
;
ÜÜ2 3
foreach
àà 
(
àà 
var
àà 
item
àà !
in
àà" $

interfaces
àà% /
)
àà/ 0
{
ââ 
var
ää 
method
ää 
=
ää  
item
ää! %
.
ää% &
	GetMethod
ää& /
(
ää/ 0
$str
ää0 9
)
ää9 :
;
ää: ;
if
ãã 
(
ãã 
method
ãã 
.
ãã 
DeclaringType
ãã ,
!=
ãã- /
null
ãã0 4
)
ãã4 5
{
åå 
var
çç 
	attribute
çç %
=
çç& '
service
çç( /
.
çç/ 0
GetAllAttributes
çç0 @
<
çç@ A
EndPointAttribute
ççA R
>
ççR S
(
ççS T
)
ççT U
.
ççU V
FirstOrDefault
ççV d
(
ççd e
)
ççe f
;
ççf g
var
èè 
requestType
èè '
=
èè( )
method
èè* 0
.
èè0 1
GetParameters
èè1 >
(
èè> ?
)
èè? @
.
èè@ A
FirstOrDefault
èèA O
(
èèO P
)
èèP Q
?
èèQ R
.
èèR S
ParameterType
èèS `
;
èè` a
var
êê 
endPoint
êê $
=
êê% &
new
êê' *
EndPointMetaData
êê+ ;
{
ëë 
Name
íí  
=
íí! "
	attribute
íí# ,
?
íí, -
.
íí- .
Name
íí. 2
??
íí3 5
service
íí6 =
.
íí= >
Name
íí> B
.
ííB C
ToTitle
ííC J
(
ííJ K
)
ííK L
,
ííL M
Path
ìì  
=
ìì! "
path
ìì# '
,
ìì' (
Method
îî "
=
îî# $
	attribute
îî% .
?
îî. /
.
îî/ 0
Method
îî0 6
??
îî7 9
$str
îî: @
,
îî@ A
EndPointType
ïï (
=
ïï) *
service
ïï+ 2
,
ïï2 3
RequestType
ññ '
=
ññ( )
requestType
ññ* 5
,
ññ5 6
Tags
óó  
=
óó! "
	attribute
óó# ,
?
óó, -
.
óó- .
Tags
óó. 2
,
óó2 3
ResponseType
òò (
=
òò) *
GetResponseType
òò+ :
(
òò: ;
method
òò; A
)
òòA B
,
òòB C
Rules
ôô !
=
ôô" #
requestType
ôô$ /
?
ôô/ 0
.
ôô0 1
GetRules
ôô1 9
(
ôô9 :
)
ôô: ;
.
ôô; <
Select
ôô< B
(
ôôB C
e
ôôC D
=>
ôôE G
new
ôôH K
EndPointRule
ôôL X
(
ôôX Y
e
ôôY Z
)
ôôZ [
)
ôô[ \
.
ôô\ ]
ToList
ôô] c
(
ôôc d
)
ôôd e
,
ôôe f
Version
öö #
=
öö$ %
version
öö& -
,
öö- .
Summary
õõ #
=
õõ$ %
summary
õõ& -
?
õõ- .
.
õõ. /
Summary
õõ/ 6
,
õõ6 7
Timeout
úú #
=
úú$ %
timeout
úú& -
,
úú- .
InvokeMethod
ùù (
=
ùù) *
method
ùù+ 1
,
ùù1 2
Public
ûû "
=
ûû# $
service
ûû% ,
.
ûû, -
GetAllAttributes
ûû- =
<
ûû= >
EndPointAttribute
ûû> O
>
ûûO P
(
ûûP Q
)
ûûQ R
.
ûûR S
FirstOrDefault
ûûS a
(
ûûa b
)
ûûb c
?
ûûc d
.
ûûd e
Public
ûûe k
??
ûûl n
true
ûûo s
,
ûûs t
Secure
üü "
=
üü# $
	attribute
üü% .
?
üü. /
.
üü/ 0
Secure
üü0 6
??
üü7 9
false
üü: ?
}
†† 
;
†† 
yield
°° 
return
°° $
endPoint
°°% -
;
°°- .
}
¢¢ 
}
££ 
}
§§ 
}
•• 	
private
ßß 
static
ßß 
Type
ßß 
GetResponseType
ßß +
(
ßß+ ,

MethodInfo
ßß, 6
method
ßß7 =
)
ßß= >
{
®® 	
if
©© 
(
©© 
method
©© 
.
©© 

ReturnType
©© !
==
©©" $
typeof
©©% +
(
©©+ ,
Task
©©, 0
)
©©0 1
)
©©1 2
{
™™ 
return
´´ 
null
´´ 
;
´´ 
}
¨¨ 
if
≠≠ 
(
≠≠ 
(
≠≠ 
bool
≠≠ 
)
≠≠ 
method
≠≠ 
.
≠≠ 

ReturnType
≠≠ '
?
≠≠' (
.
≠≠( )
GetTypeInfo
≠≠) 4
(
≠≠4 5
)
≠≠5 6
.
≠≠6 7
IsGenericType
≠≠7 D
&&
≠≠E G
method
≠≠H N
.
≠≠N O

ReturnType
≠≠O Y
.
≠≠Y Z&
GetGenericTypeDefinition
≠≠Z r
(
≠≠r s
)
≠≠s t
==
≠≠u w
typeof
≠≠x ~
(
≠≠~ 
Task≠≠ É
<≠≠É Ñ
>≠≠Ñ Ö
)≠≠Ö Ü
)≠≠Ü á
{
ÆÆ 
return
ØØ 
method
ØØ 
.
ØØ 

ReturnType
ØØ (
.
ØØ( )!
GetGenericArguments
ØØ) <
(
ØØ< =
)
ØØ= >
[
ØØ> ?
$num
ØØ? @
]
ØØ@ A
;
ØØA B
}
∞∞ 
return
±± 
method
±± 
.
±± 

ReturnType
±± $
;
±±$ %
}
≤≤ 	
}
≥≥ 
}¥¥ Ì*
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\EndPointPath.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class 
EndPointPath 
: 
IComparable  +
{ 
public 
bool 
IsSystemPath  
=>! #
this$ (
.( )
Path) -
?- .
.. /

StartsWith/ 9
(9 :
$str: =
)= >
==? A
trueB F
;F G
public

 
string

 
Path

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
int 
Version 
{ 
get  
;  !
set" %
;% &
}' (
public 
bool 
IsVersioned 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Value 
{ 
get !
;! "
set# &
;& '
}( )
private 
static 
Regex 
Regex "
=# $
new% (
Regex) .
(. /
$str/ F
)F G
;G H
public 
EndPointPath 
( 
string "
value# (
)( )
{ 	
this 
. 
Value 
= 
this 
. 
Path "
=# $
value% *
?* +
.+ ,
Trim, 0
(0 1
)1 2
.2 3
Trim3 7
(7 8
$char8 ;
); <
;< =
if 
( 
! 
String 
. 
IsNullOrWhiteSpace *
(* +
this+ /
./ 0
Value0 5
)5 6
)6 7
{ 
if 
( 
Regex 
. 
IsMatch !
(! "
value" '
)' (
)( )
{ 
var 
match 
= 
Regex  %
.% &
Match& +
(+ ,
this, 0
.0 1
Value1 6
)6 7
;7 8
this 
. 
Version  
=! "
Convert# *
.* +
ToInt32+ 2
(2 3
match3 8
.8 9
Groups9 ?
[? @
$num@ A
]A B
.B C
ValueC H
)H I
;I J
this 
. 
Path 
= 
match  %
.% &
Groups& ,
[, -
$num- .
]. /
./ 0
Value0 5
;5 6
this 
. 
IsVersioned $
=% &
true' +
;+ ,
} 
}   
}!! 	
public## 
int## 
	CompareTo## 
(## 
object## #
obj##$ '
)##' (
{$$ 	
var%% 
source%% 
=%% 
obj%% 
as%% 
EndPointPath%%  ,
;%%, -
if&& 
(&& 
source&& 
==&& 
null&& 
)&& 
{'' 
return(( 
-(( 
$num(( 
;(( 
})) 
if** 
(** 
this** 
.** 
IsSystemPath** !
&&**" $
!**% &
source**& ,
.**, -
IsSystemPath**- 9
)**9 :
{++ 
return,, 
$num,, 
;,, 
}-- 
if.. 
(.. 
source.. 
... 
IsSystemPath.. #
&&..$ &
!..' (
this..( ,
..., -
IsSystemPath..- 9
)..9 :
{// 
return00 
-00 
$num00 
;00 
}11 
if22 
(22 
this22 
.22 
IsVersioned22  
&&22! #
!22$ %
source22% +
.22+ ,
IsVersioned22, 7
)227 8
{33 
return44 
$num44 
;44 
}55 
if66 
(66 
source66 
.66 
IsVersioned66 "
&&66# %
!66& '
this66' +
.66+ ,
IsVersioned66, 7
)667 8
{77 
return88 
-88 
$num88 
;88 
}99 
if:: 
(:: 
this:: 
.:: 
Path:: 
==:: 
source:: #
.::# $
Path::$ (
)::( )
{;; 
return<< 
this<< 
.<< 
Version<< #
.<<# $
	CompareTo<<$ -
(<<- .
source<<. 4
.<<4 5
Version<<5 <
)<<< =
;<<= >
}== 
return>> 
String>> 
.>> 
CompareOrdinal>> (
(>>( )
this>>) -
.>>- .
Value>>. 3
,>>3 4
source>>5 ;
.>>; <
Value>>< A
)>>A B
;>>B C
}?? 	
}@@ 
}AA P
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\EndPointProperty.csÆ
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\EndPointRule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class 
EndPointRule 
{ 
public 
EndPointRule 
( 
Type  
type! %
)% &
{ 	
this 
. 
Name 
= 
type 
. 
Name !
.! "

ToSentence" ,
(, -
)- .
;. /
var 
baseType 
= 
type 
.  
GetTypeInfo  +
(+ ,
), -
.- .
BaseType. 6
?6 7
.7 8$
GetGenericTypeDefinition8 P
(P Q
)Q R
;R S
if 
( 
baseType 
== 
typeof "
(" #
BusinessRule# /
</ 0
>0 1
)1 2
)2 3
{ 
this   
.   
RuleType   
=   
ValidationType    .
.  . /
Business  / 7
;  7 8
}!! 
if"" 
("" 
baseType"" 
=="" 
typeof"" "
(""" #
SecurityRule""# /
<""/ 0
>""0 1
)""1 2
)""2 3
{## 
this$$ 
.$$ 
RuleType$$ 
=$$ 
ValidationType$$  .
.$$. /
Security$$/ 7
;$$7 8
}%% 
if&& 
(&& 
baseType&& 
==&& 
typeof&& "
(&&" #
	InputRule&&# ,
<&&, -
>&&- .
)&&. /
)&&/ 0
{'' 
this(( 
.(( 
RuleType(( 
=(( 
ValidationType((  .
.((. /
Input((/ 4
;((4 5
})) 
this** 
.** 
Comments** 
=** 
type**  
.**  !
GetComments**! ,
(**, -
)**- .
;**. /
}++ 	
public11 
Comments11 
Comments11  
{11! "
get11# &
;11& '
set11( +
;11+ ,
}11- .
public77 
string77 
Name77 
{77 
get77  
;77  !
set77" %
;77% &
}77' (
public== 
ValidationType== 
RuleType== &
{==' (
get==) ,
;==, -
set==. 1
;==1 2
}==3 4
}>> 
}?? ¸

LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\RemoteEndPoint.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{		 
public 

class 
RemoteEndPoint 
{ 
public 
RemoteEndPoint 
( 
string $
path% )
,) *
string+ 1
fullPath2 :
,: ;
string< B
methodC I
=J K
nullL P
)P Q
{ 	
this 
. 
Path 
= 
path 
; 
this 
. 
FullPath 
= 
fullPath $
;$ %
this 
. 
Method 
= 
method  
;  !
} 	
public   
string   
FullPath   
{    
get  ! $
;  $ %
internal  & .
set  / 2
;  2 3
}  4 5
public&& 
string&& 
Method&& 
{&& 
get&& "
;&&" #
set&&$ '
;&&' (
}&&) *
public,, 
string,, 
Path,, 
{,, 
get,,  
;,,  !
},," #
}-- 
}.. ‰
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\RemoteService.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Inventory

! *
{ 
public 

class 
RemoteService 
{ 
public 
List 
< 
RemoteEndPoint "
>" #
	EndPoints$ -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
=< =
new> A
ListB F
<F G
RemoteEndPointG U
>U V
(V W
)W X
;X Y
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ˆ
TC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\RemoteServiceInventory.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class "
RemoteServiceInventory '
{ 
private 
readonly  
ConcurrentDictionary -
<- .
string. 4
,4 5
RemoteService6 C
>C D
	_servicesE N
=O P
newQ T 
ConcurrentDictionaryU i
<i j
stringj p
,p q
RemoteServicer 
>	 Ä
(
Ä Å
)
Å Ç
;
Ç É
public 
IEnumerable 
< 
RemoteService (
>( )
Services* 2
=>3 5
	_services6 ?
.? @
Values@ F
;F G
public 
IEnumerable 
< 
RemoteEndPoint )
>) *
	EndPoints+ 4
=>5 7
	_services8 A
.A B
ValuesB H
.H I

SelectManyI S
(S T
eT U
=>V X
eY Z
.Z [
	EndPoints[ d
)d e
.e f
AsEnumerablef r
(r s
)s t
;t u
public%% 
void%% 
Add%% 
(%% 
RemoteService%% %
service%%& -
)%%- .
{&& 	
	_services'' 
.'' 
AddOrUpdate'' !
(''! "
service''" )
.'') *
Path''* .
,''. /
service''0 7
,''7 8
(''9 :
a'': ;
,''; <
b''= >
)''> ?
=>''@ B
service''C J
)''J K
;''K L
}(( 	
})) 
}** Õ@
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\ServiceInventory.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class 
ServiceInventory !
{ 
public 
ServiceInventory 
(  
Application  +
application, 7
)7 8
{ 	
this 
. 
Application 
= 
application *
;* +
} 	
public%% 
Application%% 
Application%% &
{%%' (
get%%) ,
;%%, -
}%%. /
public-- 
List-- 
<-- 
EndPointMetaData-- $
>--$ %
	EndPoints--& /
{--0 1
get--2 5
;--5 6
set--7 :
;--: ;
}--< =
=--> ?
new--@ C
List--D H
<--H I
EndPointMetaData--I Y
>--Y Z
(--Z [
)--[ \
;--\ ]
public44 
IEnumerable44 
<44 
EndPointMetaData44 +
>44+ ,
Find44- 1
(441 2
object442 8
message449 @
)44@ A
{55 	
if66 
(66 
message66 
==66 
null66 
)66  
{77 
return88 

Enumerable88 !
.88! "
Empty88" '
<88' (
EndPointMetaData88( 8
>888 9
(889 :
)88: ;
;88; <
}99 
return:: 
this:: 
.:: 
	EndPoints:: !
.::! "
Where::" '
(::' (
e::( )
=>::* ,
e::- .
.::. /
RequestType::/ :
==::; =
message::> E
.::E F
GetType::F M
(::M N
)::N O
)::O P
;::P Q
};; 	
publicBB 
EndPointMetaDataBB 
FindBB  $
(BB$ %
stringBB% +
pathBB, 0
)BB0 1
{CC 	
ifDD 
(DD 
stringDD 
.DD 
IsNullOrWhiteSpaceDD )
(DD) *
pathDD* .
)DD. /
)DD/ 0
{EE 
returnFF 
nullFF 
;FF 
}GG 
pathII 
=II 
pathII 
.II 
TrimII 
(II 
$charII  
)II  !
;II! "
returnKK 
thisKK 
.KK 
	EndPointsKK !
.KK! "
FirstOrDefaultKK" 0
(KK0 1
eKK1 2
=>KK3 5
eKK6 7
.KK7 8
PathKK8 <
==KK= ?
pathKK@ D
)KKD E
;KKE F
}LL 	
publicSS 
IEnumerableSS 
<SS 
EndPointMetaDataSS +
>SS+ ,
FindSS- 1
(SS1 2
EventMessageSS2 >
messageSS? F
)SSF G
{TT 	
returnUU 
thisUU 
.UU 
	EndPointsUU !
.UU! "
WhereUU" '
(UU' (
eUU( )
=>UU* ,
eUU- .
.UU. /
RequestTypeUU/ :
==UU; =
messageUU> E
.UUE F
MessageTypeUUF Q
||UUR T
eUUU V
.UUV W
EndPointTypeUUW c
.UUc d
GetAllAttributesUUd t
<UUt u
SubscribeAttribute	UUu á
>
UUá à
(
UUà â
)
UUâ ä
.
UUä ã
Any
UUã é
(
UUé è
)
UUè ê
)
UUê ë
;
UUë í
}VV 	
public^^ 
EndPointMetaData^^ 
Find^^  $
(^^$ %
string^^% +
path^^, 0
,^^0 1
object^^2 8
message^^9 @
)^^@ A
{__ 	
var`` 
target`` 
=`` 
this`` 
.`` 
Find`` "
(``" #
path``# '
)``' (
;``( )
ifaa 
(aa 
messageaa 
!=aa 
nullaa 
)aa  
{bb 
ifcc 
(cc 
targetcc 
==cc 
nullcc "
)cc" #
{dd 
targetee 
=ee 
thisee !
.ee! "
Findee" &
(ee& '
messageee' .
)ee. /
.ee/ 0
FirstOrDefaultee0 >
(ee> ?
)ee? @
;ee@ A
}ff 
ifgg 
(gg 
targetgg 
==gg 
nullgg "
)gg" #
{hh 
varii 
	attributeii !
=ii" #
messageii$ +
.ii+ ,
GetTypeii, 3
(ii3 4
)ii4 5
.ii5 6
GetAllAttributesii6 F
<iiF G
RequestAttributeiiG W
>iiW X
(iiX Y
)iiY Z
.iiZ [
FirstOrDefaultii[ i
(iii j
)iij k
;iik l
ifjj 
(jj 
	attributejj !
!=jj" $
nulljj% )
)jj) *
{kk 
targetll 
=ll  
thisll! %
.ll% &
Findll& *
(ll* +
	attributell+ 4
.ll4 5
Pathll5 9
)ll9 :
;ll: ;
}mm 
}nn 
}oo 
returnpp 
targetpp 
;pp 
}qq 	
publicww 
voidww 
Loadww 
(ww 
paramsww 
Assemblyww  (
[ww( )
]ww) *

assembliesww+ 5
)ww5 6
{xx 	
foreachyy 
(yy 
varyy 
serviceyy  
inyy! #

assembliesyy$ .
.yy. /
SafelyGetTypesyy/ =
(yy= >
typeofyy> D
(yyD E
	IEndPointyyE N
)yyN O
)yyO P
.yyP Q
DistinctyyQ Y
(yyY Z
)yyZ [
)yy[ \
{zz 
if{{ 
({{ 
!{{ 
service{{ 
.{{ 
GetTypeInfo{{ (
({{( )
){{) *
.{{* +
IsGenericType{{+ 8
&&{{9 ;
!{{< =
service{{= D
.{{D E
	IsDynamic{{E N
({{N O
){{O P
&&{{Q S
!{{T U
service{{U \
.{{\ ]
GetTypeInfo{{] h
({{h i
){{i j
.{{j k

IsAbstract{{k u
){{u v
{|| 
this}} 
.}} 
	EndPoints}} "
.}}" #
AddRange}}# +
(}}+ ,
EndPointMetaData}}, <
.}}< =
Create}}= C
(}}C D
service}}D K
)}}K L
)}}L M
;}}M N
}~~ 
} 
foreach
ÄÄ 
(
ÄÄ 
var
ÄÄ 
group
ÄÄ 
in
ÄÄ !
this
ÄÄ" &
.
ÄÄ& '
	EndPoints
ÄÄ' 0
.
ÄÄ0 1
GroupBy
ÄÄ1 8
(
ÄÄ8 9
e
ÄÄ9 :
=>
ÄÄ; =
e
ÄÄ> ?
.
ÄÄ? @
Path
ÄÄ@ D
)
ÄÄD E
)
ÄÄE F
{
ÅÅ 
var
ÇÇ 
current
ÇÇ 
=
ÇÇ 
group
ÇÇ #
.
ÇÇ# $
Max
ÇÇ$ '
(
ÇÇ' (
e
ÇÇ( )
=>
ÇÇ* ,
e
ÇÇ- .
.
ÇÇ. /
Version
ÇÇ/ 6
)
ÇÇ6 7
;
ÇÇ7 8
foreach
ÉÉ 
(
ÉÉ 
var
ÉÉ 
previous
ÉÉ %
in
ÉÉ& (
group
ÉÉ) .
.
ÉÉ. /
Where
ÉÉ/ 4
(
ÉÉ4 5
e
ÉÉ5 6
=>
ÉÉ7 9
e
ÉÉ: ;
.
ÉÉ; <
Version
ÉÉ< C
!=
ÉÉD F
current
ÉÉG N
)
ÉÉN O
)
ÉÉO P
{
ÑÑ 
previous
ÖÖ 
.
ÖÖ 
Path
ÖÖ !
=
ÖÖ" #
$"
ÖÖ$ &
v
ÖÖ& '
{
ÖÖ' (
previous
ÖÖ( 0
.
ÖÖ0 1
Version
ÖÖ1 8
}
ÖÖ8 9
/
ÖÖ9 :
{
ÖÖ: ;
previous
ÖÖ; C
.
ÖÖC D
Path
ÖÖD H
}
ÖÖH I
"
ÖÖI J
;
ÖÖJ K
previous
ÜÜ 
.
ÜÜ 
IsVersioned
ÜÜ (
=
ÜÜ) *
true
ÜÜ+ /
;
ÜÜ/ 0
}
áá 
}
àà 
}
ââ 	
}
ää 
}ãã Ü
MC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Inventory\ServiceMetaData.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Inventory! *
{ 
public 

class 
ServiceMetaData  
{ 
public 
ServiceMetaData 
( 
Type #
service$ +
,+ ,
string- 3
rootPath4 <
)< =
{ 	
this 
. 
Path 
= 
service 
.  
GetPath  '
(' (
)( )
;) *
this 
. 
RootPath 
= 
rootPath $
;$ %
this 
. 
	EndPoints 
= 
EndPointMetaData -
.- .
Create. 4
(4 5
service5 <
)< =
.= >
ToList> D
(D E
)E F
;F G
this 
. 
ServiceType 
= 
service &
;& '
this 
. 
Name 
= 
service 
.  
Name  $
;$ %
var   
	attribute   
=   
service   #
.  # $
GetAllAttributes  $ 4
<  4 5
EndPointAttribute  5 F
>  F G
(  G H
)  H I
.  I J
FirstOrDefault  J X
(  X Y
)  Y Z
;  Z [
if!! 
(!! 
	attribute!! 
!=!! 
null!! !
)!!! "
{"" 
this## 
.## 
Name## 
=## 
	attribute## %
.##% &
Name##& *
??##+ -
this##. 2
.##2 3
Name##3 7
;##7 8
}$$ 
}%% 	
public** 
ServiceMetaData** 
(** 
)**  
{++ 	
},, 	
public22 
List22 
<22 
EndPointMetaData22 $
>22$ %
	EndPoints22& /
{220 1
get222 5
;225 6
set227 :
;22: ;
}22< =
=22> ?
new22@ C
List22D H
<22H I
EndPointMetaData22I Y
>22Y Z
(22Z [
)22[ \
;22\ ]
public88 
string88 
Name88 
{88 
get88  
;88  !
set88" %
;88% &
}88' (
public>> 
string>> 
Path>> 
{>> 
get>>  
;>>  !
set>>" %
;>>% &
}>>' (
publicDD 
stringDD 
RootPathDD 
{DD  
getDD! $
;DD$ %
setDD& )
;DD) *
}DD+ ,
publicJJ 
TypeJJ 
ServiceTypeJJ 
{JJ  !
getJJ" %
;JJ% &
setJJ' *
;JJ* +
}JJ, -
}KK 
}LL ‡
AC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\Event.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{		 
public 

abstract 
class 
Event 
{ 
} 
} ó
FC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\EventEntry.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

class 

EventEntry 
{ 
public 

EventEntry 
( 
) 
{ 	
} 	
public   

EventEntry   
(   
EventMessage   &
instance  ' /
,  / 0
Application  1 <
environment  = H
)  H I
{!! 	
this"" 
."" 
	RequestId"" 
="" 
instance"" %
.""% &
	RequestId""& /
;""/ 0
this## 
.## 
ApplicationName##  
=##! "
environment### .
.##. /
Title##/ 4
;##4 5
try$$ 
{%% 
this&& 
.&& 
Body&& 
=&& 
JsonConvert&& '
.&&' (
SerializeObject&&( 7
(&&7 8
instance&&8 @
.&&@ A
Body&&A E
)&&E F
;&&F G
}'' 
catch(( 
{)) 
this** 
.** 
Body** 
=** 
$str** G
;**G H
}++ 
this,, 
.,, 
Id,, 
=,, 
instance,, 
.,, 
Id,, !
;,,! "
this-- 
.-- 
MessageType-- 
=-- 
instance-- '
.--' (
MessageType--( 3
.--3 4
FullName--4 <
;--< =
this.. 
... 
Name.. 
=.. 
instance..  
...  !
Name..! %
;..% &
this// 
.// 
EnvironmentName//  
=//! "
environment//# .
.//. /
Environment/// :
;//: ;
}00 	
public66 
string66 
ApplicationName66 %
{66& '
get66( +
;66+ ,
set66- 0
;660 1
}662 3
public<< 
string<< 
Body<< 
{<< 
get<<  
;<<  !
set<<" %
;<<% &
}<<' (
publicDD 
stringDD 
EnvironmentNameDD %
{DD& '
getDD( +
;DD+ ,
setDD- 0
;DD0 1
}DD2 3
publicJJ 
stringJJ 
IdJJ 
{JJ 
getJJ 
;JJ 
setJJ  #
;JJ# $
}JJ% &
publicPP 
stringPP 
MessageTypePP !
{PP" #
getPP$ '
;PP' (
setPP) ,
;PP, -
}PP. /
publicVV 
stringVV 
NameVV 
{VV 
getVV  
;VV  !
setVV" %
;VV% &
}VV' (
public\\ 
string\\ 
	RequestId\\ 
{\\  !
get\\" %
;\\% &
set\\' *
;\\* +
}\\, -
publicbb 
DateTimeOffsetbb 
	TimeStampbb '
{bb( )
getbb* -
;bb- .
setbb/ 2
;bb2 3
}bb4 5
=bb6 7
DateTimeOffsetbb8 F
.bbF G
NowbbG J
;bbJ K
}cc 
}dd Î
GC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\IEventStore.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

	interface 
IEventStore  
{ 
Task 
Append 
( 
EventMessage  
instance! )
)) *
;* +
Task!! 
<!! 
IEnumerable!! 
<!! 

EventEntry!! #
>!!# $
>!!$ %
	GetEvents!!& /
(!!/ 0
DateTimeOffset!!0 >
?!!> ?
start!!@ E
=!!F G
null!!H L
,!!L M
DateTimeOffset!!N \
?!!\ ]
end!!^ a
=!!b c
null!!d h
)!!h i
;!!i j
}"" 
}## á
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\InMemoryEventStore.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
InMemoryEventStore %
:& '
IEventStore( 3
{ 
private 
readonly 
Application $
_environment% 1
;1 2
	protected 
readonly  
ReaderWriterLockSlim /
	CacheLock0 9
=: ;
new< ? 
ReaderWriterLockSlim@ T
(T U
)U V
;V W
	protected 
readonly 
List 
<  

EventEntry  *
>* +
	Instances, 5
=6 7
new8 ;
List< @
<@ A

EventEntryA K
>K L
(L M
)M N
;N O
public%% 
InMemoryEventStore%% !
(%%! "
Application%%" -
environment%%. 9
)%%9 :
{&& 	
_environment'' 
='' 
environment'' &
;''& '
}(( 	
public** 
Task** 
<** 
IEnumerable** 
<**  

EventEntry**  *
>*** +
>**+ ,
	GetEvents**- 6
(**6 7
DateTimeOffset**7 E
?**E F
start**G L
,**L M
DateTimeOffset**N \
?**\ ]
end**^ a
)**a b
{++ 	
	CacheLock,, 
.,, 
EnterReadLock,, #
(,,# $
),,$ %
;,,% &
try-- 
{.. 
start// 
=// 
start// 
??//  
DateTimeOffset//! /
./// 0
Now//0 3
.//3 4
LocalDateTime//4 A
.//A B
AddDays//B I
(//I J
-//J K
$num//K L
)//L M
;//M N
end00 
=00 
end00 
??00 
DateTimeOffset00 +
.00+ ,
Now00, /
.00/ 0
LocalDateTime000 =
;00= >
return11 
Task11 
.11 

FromResult11 &
(11& '
	Instances11' 0
.110 1
Where111 6
(116 7
e117 8
=>119 ;
e11< =
.11= >
	TimeStamp11> G
>=11H J
start11K P
&&11Q S
e11T U
.11U V
	TimeStamp11V _
<=11` b
end11c f
)11f g
.11g h
AsEnumerable11h t
(11t u
)11u v
)11v w
;11w x
}22 
finally33 
{44 
	CacheLock55 
.55 
ExitReadLock55 &
(55& '
)55' (
;55( )
}66 
}77 	
public99 
Task99 
Append99 
(99 
EventMessage99 '
instance99( 0
)990 1
{:: 	
Argument;; 
.;; 
NotNull;; 
(;; 
instance;; %
,;;% &
nameof;;' -
(;;- .
instance;;. 6
);;6 7
);;7 8
;;;8 9
	CacheLock== 
.== 
EnterWriteLock== $
(==$ %
)==% &
;==& '
try>> 
{?? 
	Instances@@ 
.@@ 
Add@@ 
(@@ 
new@@ !

EventEntry@@" ,
(@@, -
instance@@- 5
,@@5 6
_environment@@7 C
)@@C D
)@@D E
;@@E F
}AA 
finallyBB 
{CC 
	CacheLockDD 
.DD 
ExitWriteLockDD '
(DD' (
)DD( )
;DD) *
}EE 
returnFF 
TaskFF 
.FF 

FromResultFF "
(FF" #
$numFF# $
)FF$ %
;FF% &
}GG 	
}HH 
}II ˇ
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\InMemoryRequestLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
InMemoryRequestLog %
:& '
IRequestLog( 3
{ 
private 
readonly 
Application $
_environment% 1
;1 2
	protected 
readonly  
ReaderWriterLockSlim /
	CacheLock0 9
=: ;
new< ? 
ReaderWriterLockSlim@ T
(T U
)U V
;V W
	protected 
readonly 
List 
<  
RequestEntry  ,
>, -
	Instances. 7
=8 9
new: =
List> B
<B C
RequestEntryC O
>O P
(P Q
)Q R
;R S
public!! 
InMemoryRequestLog!! !
(!!! "
Application!!" -
environment!!. 9
)!!9 :
{"" 	
_environment## 
=## 
environment## &
;##& '
}$$ 	
public&& 
Task&& 
Append&& 
(&& 
Request&& "
entry&&# (
)&&( )
{'' 	
Argument(( 
.(( 
NotNull(( 
((( 
entry(( "
,((" #
nameof(($ *
(((* +
entry((+ 0
)((0 1
)((1 2
;((2 3
	CacheLock** 
.** 
EnterWriteLock** $
(**$ %
)**% &
;**& '
try++ 
{,, 
	Instances-- 
.-- 
Add-- 
(-- 
new-- !
RequestEntry--" .
(--. /
entry--/ 4
,--4 5
_environment--6 B
)--B C
)--C D
;--D E
}.. 
finally// 
{00 
	CacheLock11 
.11 
ExitWriteLock11 '
(11' (
)11( )
;11) *
}22 
return33 
Task33 
.33 

FromResult33 "
(33" #
$num33# $
)33$ %
;33% &
}44 	
public66 
Task66 
<66 
IEnumerable66 
<66  
RequestEntry66  ,
>66, -
>66- .

GetEntries66/ 9
(669 :
DateTimeOffset66: H
?66H I
start66J O
,66O P
DateTimeOffset66Q _
?66_ `
end66a d
)66d e
{77 	
	CacheLock88 
.88 
EnterReadLock88 #
(88# $
)88$ %
;88% &
try99 
{:: 
start;; 
=;; 
start;; 
??;;  
DateTimeOffset;;! /
.;;/ 0
Now;;0 3
.;;3 4
LocalDateTime;;4 A
.;;A B
AddDays;;B I
(;;I J
-;;J K
$num;;K L
);;L M
;;;M N
end<< 
=<< 
end<< 
??<< 
DateTimeOffset<< +
.<<+ ,
Now<<, /
.<</ 0
LocalDateTime<<0 =
;<<= >
return== 
Task== 
.== 

FromResult== &
(==& '
	Instances==' 0
.==0 1
Where==1 6
(==6 7
e==7 8
=>==9 ;
e==< =
.=== >
	TimeStamp==> G
>===H J
start==K P
&&==Q S
e==T U
.==U V
	TimeStamp==V _
<===` b
end==c f
)==f g
.==g h
AsEnumerable==h t
(==t u
)==u v
)==v w
;==w x
}>> 
finally?? 
{@@ 
	CacheLockAA 
.AA 
ExitReadLockAA &
(AA& '
)AA' (
;AA( )
}BB 
}CC 	
}DD 
}EE π
OC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\InMemoryResponseLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
InMemoryResponseLog &
:' (
IResponseLog) 5
{ 
	protected 
readonly  
ReaderWriterLockSlim /
	CacheLock0 9
=: ;
new< ? 
ReaderWriterLockSlim@ T
(T U
)U V
;V W
	protected 
readonly 
List 
<  
ResponseEntry  -
>- .
	Instances/ 8
=9 :
new; >
List? C
<C D
ResponseEntryD Q
>Q R
(R S
)S T
;T U
public 
Task 
Append 
( 
ResponseEntry (
entry) .
). /
{ 	
Argument   
.   
NotNull   
(   
entry   "
,  " #
nameof  $ *
(  * +
entry  + 0
)  0 1
)  1 2
;  2 3
	CacheLock"" 
."" 
EnterWriteLock"" $
(""$ %
)""% &
;""& '
try## 
{$$ 
	Instances%% 
.%% 
Add%% 
(%% 
entry%% #
)%%# $
;%%$ %
}&& 
finally'' 
{(( 
	CacheLock)) 
.)) 
ExitWriteLock)) '
())' (
)))( )
;))) *
}** 
return++ 
Task++ 
.++ 

FromResult++ "
(++" #
$num++# $
)++$ %
;++% &
},, 	
public.. 
Task.. 
<.. 
IEnumerable.. 
<..  
ResponseEntry..  -
>..- .
>... /

GetEntries..0 :
(..: ;
DateTimeOffset..; I
?..I J
start..K P
,..P Q
DateTimeOffset..R `
?..` a
end..b e
)..e f
{// 	
	CacheLock00 
.00 
EnterReadLock00 #
(00# $
)00$ %
;00% &
try11 
{22 
start33 
=33 
start33 
??33  
DateTimeOffset33! /
.33/ 0
Now330 3
.333 4
LocalDateTime334 A
.33A B
AddDays33B I
(33I J
-33J K
$num33K L
)33L M
;33M N
end44 
=44 
end44 
??44 
DateTimeOffset44 +
.44+ ,
Now44, /
.44/ 0
LocalDateTime440 =
;44= >
return55 
Task55 
.55 

FromResult55 &
(55& '
	Instances55' 0
.550 1
Where551 6
(556 7
e557 8
=>559 ;
e55< =
.55= >
	TimeStamp55> G
>=55H J
start55K P
&&55Q S
e55T U
.55U V
	TimeStamp55V _
<=55` b
end55c f
)55f g
.55g h
AsEnumerable55h t
(55t u
)55u v
)55v w
;55w x
}66 
finally77 
{88 
	CacheLock99 
.99 
ExitReadLock99 &
(99& '
)99' (
;99( )
}:: 
};; 	
}<< 
}== Ê
GC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\IRequestLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

	interface 
IRequestLog  
{ 
Task 
Append 
( 
Request 
entry !
)! "
;" #
Task!! 
<!! 
IEnumerable!! 
<!! 
RequestEntry!! %
>!!% &
>!!& '

GetEntries!!( 2
(!!2 3
DateTimeOffset!!3 A
?!!A B
start!!C H
=!!I J
null!!K O
,!!O P
DateTimeOffset!!Q _
?!!_ `
end!!a d
=!!e f
null!!g k
)!!k l
;!!l m
}"" 
}## Ô
HC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\IResponseLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

	interface 
IResponseLog !
{ 
Task 
Append 
( 
ResponseEntry !
entry" '
)' (
;( )
Task   
<   
IEnumerable   
<   
ResponseEntry   &
>  & '
>  ' (

GetEntries  ) 3
(  3 4
DateTimeOffset  4 B
?  B C
start  D I
=  J K
null  L P
,  P Q
DateTimeOffset  R `
?  ` a
end  b e
=  f g
null  h l
)  l m
;  m n
}!! 
}"" ¡

JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\NullEventStore.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
NullEventStore !
:" #
IEventStore$ /
{ 
public 
Task 
< 
IEnumerable 
<  

EventEntry  *
>* +
>+ ,
	GetEvents- 6
(6 7
DateTimeOffset7 E
?E F
startG L
,L M
DateTimeOffsetN \
?\ ]
end^ a
)a b
{ 	
return 
Task 
. 

FromResult "
(" #
new# &

EventEntry' 1
[1 2
$num2 3
]3 4
.4 5
AsEnumerable5 A
(A B
)B C
)C D
;D E
} 	
public 
Task 
Append 
( 
EventMessage '
instance( 0
)0 1
{ 	
return 
Task 
. 

FromResult "
(" #
$num# $
)$ %
;% &
} 	
} 
} ò	
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\NullRequestLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
NullRequestLog !
:" #
IRequestLog$ /
{ 
public 
Task 
Append 
( 
Request "
entry# (
)( )
{ 	
return 
Task 
. 

FromResult "
(" #
$num# $
)$ %
;% &
} 	
public 
Task 
< 
IEnumerable 
<  
RequestEntry  ,
>, -
>- .

GetEntries/ 9
(9 :
DateTimeOffset: H
?H I
startJ O
,O P
DateTimeOffsetQ _
?_ `
enda d
)d e
{ 	
throw 
new #
NotImplementedException -
(- .
). /
;/ 0
} 	
} 
} ¢	
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\NullResponseLog.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
internal 
class 
NullResponseLog "
:# $
IResponseLog% 1
{ 
public 
Task 
Append 
( 
ResponseEntry (
entry) .
). /
{ 	
return 
Task 
. 

FromResult "
(" #
$num# $
)$ %
;% &
} 	
public 
Task 
< 
IEnumerable 
<  
ResponseEntry  -
>- .
>. /

GetEntries0 :
(: ;
DateTimeOffset; I
?I J
startK P
,P Q
DateTimeOffsetR `
?` a
endb e
)e f
{ 	
throw 
new #
NotImplementedException -
(- .
). /
;/ 0
} 	
} 
} ≠3
HC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\RequestEntry.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

class 
RequestEntry 
{ 
public 
RequestEntry 
( 
) 
{ 	
} 	
public"" 
RequestEntry"" 
("" 
Request"" #
request""$ +
,""+ ,
Application""- 8
environment""9 D
)""D E
{## 	
try$$ 
{%% 
if&& 
(&& 
request&& 
.&& 
Message&& #
.&&# $
Body&&$ (
!=&&) +
null&&, 0
)&&0 1
{'' 
this(( 
.(( 
Body(( 
=(( 
JsonConvert((  +
.((+ ,
SerializeObject((, ;
(((; <
request((< C
.((C D
Message((D K
.((K L
Body((L P
,((P Q
new((R U"
JsonSerializerSettings((V l
{)) 
ContractResolver** (
=**) *
new**+ . 
BaseContractResolver**/ C
(**C D
)**D E
}++ 
)++ 
;++ 
},, 
}-- 
catch.. 
{// 
this00 
.00 
Body00 
=00 
$str00 G
;00G H
}11 
if22 
(22 
request22 
.22 
Message22 
is22  "
IMessage22# +
)22+ ,
{33 
var44 
message44 
=44 
request44 %
.44% &
Message44& -
;44- .
this55 
.55 
RequestType55  
=55! "
message55# *
.55* +
MessageType55+ 6
.556 7
FullName557 ?
;55? @
this66 
.66 
	RequestId66 
=66  
message66! (
.66( )
Id66) +
;66+ ,
this77 
.77 
	TimeStamp77 
=77  
message77! (
.77( )
	TimeStamp77) 2
;772 3
}88 
else99 
{:: 
this;; 
.;; 
RequestType;;  
=;;! "
request;;# *
.;;* +
Message;;+ 2
?;;2 3
.;;3 4
MessageType;;4 ?
?;;? @
.;;@ A
FullName;;A I
;;;I J
}<< 
this== 
.== 
	SessionId== 
=== 
request== $
.==$ %
	SessionId==% .
;==. /
this>> 
.>> 
UserName>> 
=>> 
request>> #
.>># $
User>>$ (
?>>( )
.>>) *
Identity>>* 2
?>>2 3
.>>3 4
Name>>4 8
;>>8 9
this?? 
.?? 
Path?? 
=?? 
request?? 
.??  
Path??  $
;??$ %
this@@ 
.@@ 
SourceAddress@@ 
=@@  
request@@! (
.@@( )
SourceAddress@@) 6
;@@6 7
thisAA 
.AA 
CorrelationIdAA 
=AA  
requestAA! (
.AA( )
CorrelationIdAA) 6
;AA6 7
thisBB 
.BB 
ParentBB 
=BB 
requestBB !
.BB! "
ParentBB" (
?BB( )
.BB) *
MessageBB* 1
?BB1 2
.BB2 3
IdBB3 5
;BB5 6
thisCC 
.CC 
MachineNameCC 
=CC 
EnvironmentCC *
.CC* +
MachineNameCC+ 6
;CC6 7
thisDD 
.DD 
ApplicationNameDD  
=DD! "
environmentDD# .
.DD. /
TitleDD/ 4
;DD4 5
thisEE 
.EE 
EnvironmentNameEE  
=EE! "
environmentEE# .
.EE. /
EnvironmentEE/ :
;EE: ;
}FF 	
publicLL 
stringLL 
ApplicationNameLL %
{LL& '
getLL( +
;LL+ ,
setLL- 0
;LL0 1
}LL2 3
publicRR 
stringRR 
BodyRR 
{RR 
getRR  
;RR  !
setRR" %
;RR% &
}RR' (
publicXX 
stringXX 
CorrelationIdXX #
{XX$ %
getXX& )
;XX) *
setXX+ .
;XX. /
}XX0 1
public`` 
string`` 
EnvironmentName`` %
{``& '
get``( +
;``+ ,
set``- 0
;``0 1
}``2 3
publicff 
stringff 
Idff 
{ff 
getff 
;ff 
setff  #
;ff# $
}ff% &
=ff' (
NewIdff) .
.ff. /
NextIdff/ 5
(ff5 6
)ff6 7
;ff7 8
publicll 
stringll 
MachineNamell !
{ll" #
getll$ '
;ll' (
setll) ,
;ll, -
}ll. /
publicrr 
stringrr 
Parentrr 
{rr 
getrr "
;rr" #
setrr$ '
;rr' (
}rr) *
publicxx 
stringxx 
Pathxx 
{xx 
getxx  
;xx  !
setxx" %
;xx% &
}xx' (
public~~ 
string~~ 
	RequestId~~ 
{~~  !
get~~" %
;~~% &
set~~' *
;~~* +
}~~, -
public
ÑÑ 
string
ÑÑ 
RequestType
ÑÑ !
{
ÑÑ" #
get
ÑÑ$ '
;
ÑÑ' (
set
ÑÑ) ,
;
ÑÑ, -
}
ÑÑ. /
public
ää 
string
ää 
	SessionId
ää 
{
ää  !
get
ää" %
;
ää% &
set
ää' *
;
ää* +
}
ää, -
public
êê 
string
êê 
SourceAddress
êê #
{
êê$ %
get
êê& )
;
êê) *
set
êê+ .
;
êê. /
}
êê0 1
public
ññ 
DateTimeOffset
ññ 
?
ññ 
	TimeStamp
ññ (
{
ññ) *
get
ññ+ .
;
ññ. /
set
ññ0 3
;
ññ3 4
}
ññ5 6
public
úú 
string
úú 
UserName
úú 
{
úú  
get
úú! $
;
úú$ %
set
úú& )
;
úú) *
}
úú+ ,
}
ùù 
}ûû ÿ5
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Logging\ResponseEntry.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Logging! (
{ 
public 

class 
ResponseEntry 
{ 
public 
ResponseEntry 
( 
) 
{ 	
}   	
public'' 
ResponseEntry'' 
('' 
ExecutionContext'' -
context''. 5
,''5 6
Application''7 B
environment''C N
)''N O
{(( 	
this)) 
.)) 
CorrelationId)) 
=))  
context))! (
.))( )
Request))) 0
.))0 1
CorrelationId))1 >
;))> ?
this** 
.** 
	RequestId** 
=** 
context** $
.**$ %
Request**% ,
.**, -
Message**- 4
.**4 5
Id**5 7
;**7 8
this++ 
.++ 
	Completed++ 
=++ 
context++ $
.++$ %
	Completed++% .
;++. /
this,, 
.,, 
EndPoint,, 
=,, 
context,, #
.,,# $
EndPoint,,$ ,
.,,, -
EndPointType,,- 9
.,,9 :!
AssemblyQualifiedName,,: O
;,,O P
this-- 
.-- 
	Exception-- 
=-- 
context-- $
.--$ %
	Exception--% .
?--. /
.--/ 0
ToString--0 8
(--8 9
)--9 :
;--: ;
this.. 
... 
IsSuccessful.. 
=.. 
context..  '
...' (
IsSuccessful..( 4
;..4 5
this// 
.// 
Started// 
=// 
context// "
.//" #
Started//# *
;//* +
this00 
.00 
ValidationErrors00 !
=00" #
context00$ +
.00+ ,
ValidationErrors00, <
;00< =
this11 
.11 
	TimeStamp11 
=11 
DateTimeOffset11 +
.11+ ,
Now11, /
;11/ 0
this22 
.22 
MachineName22 
=22 
Environment22 *
.22* +
MachineName22+ 6
;226 7
this33 
.33 
ApplicationName33  
=33! "
environment33# .
.33. /
Title33/ 4
;334 5
this44 
.44 
Path44 
=44 
context44 
.44  
Request44  '
.44' (
Path44( ,
;44, -
this55 
.55 
Version55 
=55 
environment55 &
.55& '
Version55' .
;55. /
this66 
.66 
Build66 
=66 
Assembly66 !
.66! "
GetEntryAssembly66" 2
(662 3
)663 4
?664 5
.665 6
GetName666 =
(66= >
)66> ?
?66? @
.66@ A
Version66A H
.66H I
ToString66I Q
(66Q R
)66R S
;66S T
if77 
(77 
this77 
.77 
	Completed77 
.77 
HasValue77 '
)77' (
{88 
this99 
.99 
Elapsed99 
=99 
this99 #
.99# $
	Completed99$ -
.99- .
Value99. 3
-994 5
this996 :
.99: ;
Started99; B
;99B C
}:: 
this;; 
.;; 
EnvironmentName;;  
=;;! "
environment;;# .
.;;. /
Environment;;/ :
;;;: ;
}<< 	
publicBB 
stringBB 
ApplicationNameBB %
{BB& '
getBB( +
;BB+ ,
setBB- 0
;BB0 1
}BB2 3
publicJJ 
stringJJ 
BuildJJ 
{JJ 
getJJ !
;JJ! "
setJJ# &
;JJ& '
}JJ( )
publicPP 
DateTimeOffsetPP 
?PP 
	CompletedPP (
{PP) *
getPP+ .
;PP. /
setPP0 3
;PP3 4
}PP5 6
publicVV 
stringVV 
CorrelationIdVV #
{VV$ %
getVV& )
;VV) *
setVV+ .
;VV. /
}VV0 1
public\\ 
TimeSpan\\ 
Elapsed\\ 
{\\  !
get\\" %
;\\% &
set\\' *
;\\* +
}\\, -
publicbb 
stringbb 
EndPointbb 
{bb  
getbb! $
;bb$ %
setbb& )
;bb) *
}bb+ ,
publicjj 
stringjj 
EnvironmentNamejj %
{jj& '
getjj( +
;jj+ ,
setjj- 0
;jj0 1
}jj2 3
publicpp 
stringpp 
	Exceptionpp 
{pp  !
getpp" %
;pp% &
setpp' *
;pp* +
}pp, -
publicvv 
stringvv 
Idvv 
{vv 
getvv 
;vv 
setvv  #
;vv# $
}vv% &
=vv' (
NewIdvv) .
.vv. /
NextIdvv/ 5
(vv5 6
)vv6 7
;vv7 8
public|| 
bool|| 
IsSuccessful||  
{||! "
get||# &
;||& '
set||( +
;||+ ,
}||- .
public
ÇÇ 
string
ÇÇ 
MachineName
ÇÇ !
{
ÇÇ" #
get
ÇÇ$ '
;
ÇÇ' (
set
ÇÇ) ,
;
ÇÇ, -
}
ÇÇ. /
public
àà 
string
àà 
Path
àà 
{
àà 
get
àà  
;
àà  !
set
àà" %
;
àà% &
}
àà' (
public
éé 
string
éé 
	RequestId
éé 
{
éé  !
get
éé" %
;
éé% &
set
éé' *
;
éé* +
}
éé, -
public
îî 
DateTimeOffset
îî 
Started
îî %
{
îî& '
get
îî( +
;
îî+ ,
set
îî- 0
;
îî0 1
}
îî2 3
public
öö 
DateTimeOffset
öö 
	TimeStamp
öö '
{
öö( )
get
öö* -
;
öö- .
set
öö/ 2
;
öö2 3
}
öö4 5
public
†† 
IEnumerable
†† 
<
†† 
ValidationError
†† *
>
††* +
ValidationErrors
††, <
{
††= >
get
††? B
;
††B C
set
††D G
;
††G H
}
††I J
public
®® 
string
®® 
Version
®® 
{
®® 
get
®®  #
;
®®# $
set
®®% (
;
®®( )
}
®®* +
}
©© 
}™™ „
GC:\Source\Stacks\Core\src\Slalom.Stacks\Services\MessagingExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

static 
class 
MessagingExtensions +
{ 
public 
static 
IEnumerable !
<! "

EventEntry" ,
>, -
	GetEvents. 7
(7 8
this8 <
Stack= B
instanceC K
,K L
DateTimeOffsetM [
?[ \
start] b
=c d
nulle i
,i j
DateTimeOffsetk y
?y z
end{ ~
=	 Ä
null
Å Ö
)
Ö Ü
{ 	
return 
instance 
. 
	Container %
.% &
Resolve& -
<- .
IEventStore. 9
>9 :
(: ;
); <
.< =
	GetEvents= F
(F G
startG L
,L M
endN Q
)Q R
.R S
ResultS Y
;Y Z
} 	
public(( 
static(( 
IEnumerable(( !
<((! "
RequestEntry((" .
>((. /
GetRequests((0 ;
(((; <
this((< @
Stack((A F
instance((G O
,((O P
DateTimeOffset((Q _
?((_ `
start((a f
=((g h
null((i m
,((m n
DateTimeOffset((o }
?((} ~
end	(( Ç
=
((É Ñ
null
((Ö â
)
((â ä
{)) 	
return** 
instance** 
.** 
	Container** %
.**% &
Resolve**& -
<**- .
IRequestLog**. 9
>**9 :
(**: ;
)**; <
.**< =

GetEntries**= G
(**G H
start**H M
,**M N
end**O R
)**R S
.**S T
Result**T Z
;**Z [
}++ 	
public44 
static44 
IEnumerable44 !
<44! "
ResponseEntry44" /
>44/ 0
GetResponses441 =
(44= >
this44> B
Stack44C H
instance44I Q
,44Q R
DateTimeOffset44S a
?44a b
start44c h
=44i j
null44k o
,44o p
DateTimeOffset44q 
?	44 Ä
end
44Å Ñ
=
44Ö Ü
null
44á ã
)
44ã å
{55 	
return66 
instance66 
.66 
	Container66 %
.66% &
Resolve66& -
<66- .
IResponseLog66. :
>66: ;
(66; <
)66< =
.66= >

GetEntries66> H
(66H I
start66I N
,66N O
end66P S
)66S T
.66T U
Result66U [
;66[ \
}77 	
}88 
}99 è
FC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\Document.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{		 
public 

class 
Document 
{ 
public 
Document 
( 
string 
name #
,# $
byte% )
[) *
]* +
content, 3
)3 4
{ 	
this 
. 
Name 
= 
name 
; 
this 
. 
Content 
= 
content "
;" #
} 	
public 
byte 
[ 
] 
Content 
{ 
get  #
;# $
}% &
public$$ 
string$$ 
Name$$ 
{$$ 
get$$  
;$$  !
}$$" #
}%% 
}&& —
WC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\EndPointNotFoundException.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
public 

class %
EndPointNotFoundException *
:+ ,%
InvalidOperationException- F
{ 
public %
EndPointNotFoundException (
(( )
Request) 0
request1 8
)8 9
:: ;
base< @
(@ A
$"A C:
.An endpoint could not be found for the path \"C q
{q r
requestr y
.y z
Pathz ~
}~ 
\".	 Ç
"
Ç É
)
É Ñ
{ 	
} 	
} 
} ∂
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\EventAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
)* +
]+ ,
public 

class 
EventAttribute 
:  !
	Attribute" +
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
} 
} Û
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\EventMessage.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
EventMessage 
: 
Message  '
{ 
public 
EventMessage 
( 
string "
	requestId# ,
,, -
Event. 3
body4 8
)8 9
: 
base 
( 
body 
) 
{ 	
this 
. 
	RequestId 
= 
	requestId &
;& '
this 
. 
Name 
= 
this 
. 
GetEventName )
() *
)* +
;+ ,
} 	
internal$$ 
EventMessage$$ 
($$ 
string$$ $
	requestId$$% .
,$$. /
object$$0 6
body$$7 ;
)$$; <
:%% 
base%% 
(%% 
body%% 
)%% 
{&& 	
this'' 
.'' 
	RequestId'' 
='' 
	requestId'' &
;''& '
this(( 
.(( 
Name(( 
=(( 
this(( 
.(( 
GetEventName(( )
((() *
)((* +
;((+ ,
})) 	
public// 
string// 
	RequestId// 
{//  !
get//" %
;//% &
}//' (
private11 
string11 
GetEventName11 #
(11# $
)11$ %
{22 	
var33 
type33 
=33 
this33 
.33 
Body33  
.33  !
GetType33! (
(33( )
)33) *
;33* +
var44 
	attribute44 
=44 
type44  
.44  !
GetAllAttributes44! 1
<441 2
EventAttribute442 @
>44@ A
(44A B
)44B C
.44C D
FirstOrDefault44D R
(44R S
)44S T
;44T U
if55 
(55 
	attribute55 
!=55 
null55 !
)55! "
{66 
return77 
	attribute77  
.77  !
Name77! %
;77% &
}88 
return99 
type99 
.99 
Name99 
;99 
}:: 	
};; 
}<< ƒ0
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\ExecutionContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
ExecutionContext !
{ 
private 
readonly 
List 
< 
EventMessage *
>* +
_raisedEvents, 9
=: ;
new< ?
List@ D
<D E
EventMessageE Q
>Q R
(R S
)S T
;T U
private 
readonly 
List 
< 
ValidationError -
>- .
_validationErrors/ @
=A B
newC F
ListG K
<K L
ValidationErrorL [
>[ \
(\ ]
)] ^
;^ _
public!! 
ExecutionContext!! 
(!!  
Request!!  '
request!!( /
,!!/ 0
EndPointMetaData!!1 A
endPoint!!B J
,!!J K
CancellationToken!!L ]
cancellationToken!!^ o
,!!o p
ExecutionContext	!!q Å
parent
!!Ç à
=
!!â ä
null
!!ã è
)
!!è ê
{"" 	
this## 
.## 
Request## 
=## 
request## "
;##" #
this$$ 
.$$ 
EndPoint$$ 
=$$ 
endPoint$$ $
;$$$ %
this%% 
.%% 
Parent%% 
=%% 
parent%%  
;%%  !
this&& 
.&& 
CancellationToken&& "
=&&# $
cancellationToken&&% 6
;&&6 7
}'' 	
public.. 
ExecutionContext.. 
(..  
Request..  '
request..( /
,../ 0
ExecutionContext..1 A
context..B I
)..I J
{// 	
this00 
.00 
Request00 
=00 
request00 "
;00" #
this11 
.11 
Parent11 
=11 
context11 !
?11! "
.11" #
Parent11# )
;11) *
this22 
.22 
CancellationToken22 "
=22# $
context22% ,
?22, -
.22- .
CancellationToken22. ?
??22@ B
CancellationToken22C T
.22T U
None22U Y
;22Y Z
}33 	
public99 
CancellationToken99  
CancellationToken99! 2
{993 4
get995 8
;998 9
}99: ;
public?? 
DateTimeOffset?? 
??? 
	Completed?? (
{??) *
get??+ .
;??. /
private??0 7
set??8 ;
;??; <
}??= >
publicEE 
EndPointMetaDataEE 
EndPointEE  (
{EE) *
getEE+ .
;EE. /
}EE0 1
publicKK 
	ExceptionKK 
	ExceptionKK "
{KK# $
getKK% (
;KK( )
privateKK* 1
setKK2 5
;KK5 6
}KK7 8
publicQQ 
boolQQ 
IsSuccessfulQQ  
=>QQ! #
!QQ$ %
thisQQ% )
.QQ) *
ValidationErrorsQQ* :
.QQ: ;
AnyQQ; >
(QQ> ?
)QQ? @
&&QQA C
thisQQD H
.QQH I
	ExceptionQQI R
==QQS U
nullQQV Z
;QQZ [
publicWW 
ExecutionContextWW 
ParentWW  &
{WW' (
getWW) ,
;WW, -
}WW. /
public]] 
IEnumerable]] 
<]] 
EventMessage]] '
>]]' (
RaisedEvents]]) 5
=>]]6 8
_raisedEvents]]9 F
.]]F G
AsEnumerable]]G S
(]]S T
)]]T U
;]]U V
publiccc 
Requestcc 
Requestcc 
{cc  
getcc! $
;cc$ %
}cc& '
publicii 
objectii 
Responseii 
{ii  
getii! $
;ii$ %
setii& )
;ii) *
}ii+ ,
publicoo 
DateTimeOffsetoo 
Startedoo %
{oo& '
getoo( +
;oo+ ,
}oo- .
=oo/ 0
DateTimeOffsetoo1 ?
.oo? @
UtcNowoo@ F
;ooF G
publicuu 
IEnumerableuu 
<uu 
ValidationErroruu *
>uu* +
ValidationErrorsuu, <
=>uu= ?
_validationErrorsuu@ Q
.uuQ R
AsEnumerableuuR ^
(uu^ _
)uu_ `
;uu` a
public{{ 
void{{ 
AddRaisedEvent{{ "
({{" #
Event{{# (
instance{{) 1
){{1 2
{|| 	
_raisedEvents}} 
.}} 
Add}} 
(}} 
new}} !
EventMessage}}" .
(}}. /
this}}/ 3
.}}3 4
Request}}4 ;
.}}; <
Message}}< C
.}}C D
Id}}D F
,}}F G
instance}}H P
)}}P Q
)}}Q R
;}}R S
}~~ 	
public
ÑÑ 
void
ÑÑ !
AddValidationErrors
ÑÑ '
(
ÑÑ' (
IEnumerable
ÑÑ( 3
<
ÑÑ3 4
ValidationError
ÑÑ4 C
>
ÑÑC D
errors
ÑÑE K
)
ÑÑK L
{
ÖÖ 	
_validationErrors
ÜÜ 
.
ÜÜ 
AddRange
ÜÜ &
(
ÜÜ& '
errors
ÜÜ' -
)
ÜÜ- .
;
ÜÜ. /
}
áá 	
public
åå 
void
åå 
Complete
åå 
(
åå 
)
åå 
{
çç 	
this
éé 
.
éé 
	Completed
éé 
=
éé 
DateTimeOffset
éé +
.
éé+ ,
UtcNow
éé, 2
;
éé2 3
}
èè 	
public
ïï 
void
ïï 
SetException
ïï  
(
ïï  !
	Exception
ïï! *
	exception
ïï+ 4
)
ïï4 5
{
ññ 	
this
óó 
.
óó 
	Exception
óó 
=
óó 
	exception
óó &
;
óó& '
}
òò 	
}
ôô 
}öö ö
MC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IEventPublisher.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
public 

	interface 
IEventPublisher $
{ 
Task 
Publish 
( 
params 
EventMessage (
[( )
]) *
events+ 1
)1 2
;2 3
} 
} ‹
FC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IMessage.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
public 

	interface 
IMessage 
{ 
object 
Body 
{ 
get 
; 
} 
string 
Id 
{ 
get 
; 
} 
Type!! 
MessageType!! 
{!! 
get!! 
;!! 
}!!  !
string'' 
Name'' 
{'' 
get'' 
;'' 
}'' 
DateTimeOffset-- 
	TimeStamp--  
{--! "
get--# &
;--& '
}--( )
}.. 
}// ù
MC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IMessageGateway.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

	interface 
IMessageGateway $
{ 
Task 
Publish 
( 
EventMessage !
instance" *
,* +
ExecutionContext, <
context= D
=E F
nullG K
)K L
;L M
Task"" 
Publish"" 
("" 
IEnumerable""  
<""  !
EventMessage""! -
>""- .
	instances""/ 8
,""8 9
ExecutionContext"": J
context""K R
=""S T
null""U Y
)""Y Z
;""Z [
void** 
Publish** 
(** 
string** 
channel** #
,**# $
string**% +
message**, 3
)**3 4
;**4 5
void00 
Publish00 
(00 
Event00 
instance00 #
)00# $
;00$ %
Task99 
<99 
MessageResult99 
>99 
Send99  
(99  !
object99! '
message99( /
,99/ 0
ExecutionContext991 A
parentContext99B O
=99P Q
null99R V
,99V W
TimeSpan99X `
?99` a
timeout99b i
=99j k
null99l p
)99p q
;99q r
TaskBB 
<BB 
MessageResultBB 
>BB 
SendBB  
(BB  !
stringBB! '
pathBB( ,
,BB, -
ExecutionContextBB. >
parentContextBB? L
=BBM N
nullBBO S
,BBS T
TimeSpanBBU ]
?BB] ^
timeoutBB_ f
=BBg h
nullBBi m
)BBm n
;BBn o
TaskLL 
<LL 
MessageResultLL 
>LL 
SendLL  
(LL  !
stringLL! '
pathLL( ,
,LL, -
objectLL. 4
messageLL5 <
,LL< =
ExecutionContextLL> N
parentContextLLO \
=LL] ^
nullLL_ c
,LLc d
TimeSpanLLe m
?LLm n
timeoutLLo v
=LLw x
nullLLy }
)LL} ~
;LL~ 
TaskVV 
<VV 
MessageResultVV 
>VV 
SendVV  
(VV  !
stringVV! '
pathVV( ,
,VV, -
stringVV. 4
commandVV5 <
,VV< =
ExecutionContextVV> N
parentContextVVO \
=VV] ^
nullVV_ c
,VVc d
TimeSpanVVe m
?VVm n
timeoutVVo v
=VVw x
nullVVy }
)VV} ~
;VV~ 
}WW 
}XX »
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IRemoteRouter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

	interface 
IRemoteRouter "
{ 
bool 
CanRoute 
( 
Request 
request %
)% &
;& '
Task   
<   
MessageResult   
>   
Route   !
(  ! "
Request  " )
request  * 1
,  1 2
ExecutionContext  3 C
parentContext  D Q
,  Q R
TimeSpan  S [
?  [ \
timeout  ] d
=  e f
null  g k
)  k l
;  l m
}!! 
}"" Ë

MC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IRequestContext.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
public 

	interface 
IRequestContext $
{ 
Request 
Resolve 
( 
object 
message &
,& '
EndPointMetaData( 8
endPoint9 A
,A B
RequestC J
parentK Q
=R S
nullT X
)X Y
;Y Z
Request!! 
Resolve!! 
(!! 
string!! 
path!! #
,!!# $
EndPointMetaData!!% 5
endPoint!!6 >
,!!> ?
Request!!@ G
parent!!H N
=!!O P
null!!Q U
)!!U V
;!!V W
Request)) 
Resolve)) 
()) 
EventMessage)) $
instance))% -
,))- .
Request))/ 6
parent))7 =
)))= >
;))> ?
Request22 
Resolve22 
(22 
string22 
path22 #
,22# $
object22% +
message22, 3
,223 4
Request225 <
parent22= C
=22D E
null22F J
)22J K
;22K L
}33 
}44 ñ
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IRequestRouter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

	interface 
IRequestRouter #
{ 
Task 
< 
MessageResult 
> 
Route !
(! "
Request" )
request* 1
,1 2
EndPointMetaData3 C
endPointD L
,L M
ExecutionContextN ^
parentContext_ l
,l m
TimeSpann v
?v w
timeoutx 
=
Ä Å
null
Ç Ü
)
Ü á
;
á à
} 
} ˙
RC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\IUseExecutionContext.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{		 
public 

	interface  
IUseExecutionContext )
{ 
void 

UseContext 
( 
ExecutionContext (
context) 0
)0 1
;1 2
} 
} ß
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\Message.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
Message 
: 
IMessage #
{ 
public 
Message 
( 
object 
body "
)" #
{ 	
Argument 
. 
NotNull 
( 
body !
,! "
nameof# )
() *
body* .
). /
)/ 0
;0 1
var 
type 
= 
body 
. 
GetType #
(# $
)$ %
;% &
this 
. 
Body 
= 
body 
; 
this 
. 
MessageType 
= 
type #
;# $
this 
. 
Name 
= 
type 
. 
Name !
;! "
}   	
public%% 
Message%% 
(%% 
)%% 
{&& 	
}'' 	
public** 
string** 
Id** 
{** 
get** 
;** 
}**  !
=**" #
NewId**$ )
.**) *
NextId*** 0
(**0 1
)**1 2
;**2 3
public-- 
DateTimeOffset-- 
	TimeStamp-- '
{--( )
get--* -
;--- .
}--/ 0
=--1 2
DateTimeOffset--3 A
.--A B
UtcNow--B H
;--H I
public00 
object00 
Body00 
{00 
get00  
;00  !
}00" #
public33 
Type33 
MessageType33 
{33  !
get33" %
;33% &
}33' (
public66 
string66 
Name66 
{66 
get66  
;66  !
	protected66" +
set66, /
;66/ 0
}661 2
public99 
override99 
bool99 
Equals99 #
(99# $
object99$ *
obj99+ .
)99. /
{:: 	
return;; 
this;; 
.;; 
Id;; 
==;; 
(;; 
obj;; "
as;;# %
IMessage;;& .
);;. /
?;;/ 0
.;;0 1
Id;;1 3
;;;3 4
}<< 	
public?? 
override?? 
int?? 
GetHashCode?? '
(??' (
)??( )
{@@ 	
returnAA 
thisAA 
.AA 
IdAA 
.AA 
GetHashCodeAA &
(AA& '
)AA' (
;AA( )
}BB 	
publicJJ 
staticJJ 
boolJJ 
operatorJJ #
==JJ$ &
(JJ& '
MessageJJ' .
xJJ/ 0
,JJ0 1
MessageJJ2 9
yJJ: ;
)JJ; <
{KK 	
returnLL 
ReferenceEqualsLL "
(LL" #
xLL# $
,LL$ %
yLL& '
)LL' (
||LL) +
xLL, -
.LL- .
EqualsLL. 4
(LL4 5
yLL5 6
)LL6 7
;LL7 8
}MM 	
publicUU 
staticUU 
boolUU 
operatorUU #
!=UU$ &
(UU& '
MessageUU' .
xUU/ 0
,UU0 1
MessageUU2 9
yUU: ;
)UU; <
{VV 	
returnWW 
!WW 
(WW 
xWW 
==WW 
yWW 
)WW 
;WW 
}XX 	
}YY 
}ZZ ÑØ
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\MessageGateway.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
MessageGateway 
:  !
IMessageGateway" 1
{ 
private 
readonly 
Lazy 
< 
IRequestRouter ,
>, -
_dispatcher. 9
;9 :
private 
readonly 
Lazy 
< 
IEnumerable )
<) *
IRemoteRouter* 7
>7 8
>8 9
_dispatchers: F
;F G
private 
readonly 
Lazy 
< 
IRequestContext -
>- .
_requestContext/ >
;> ?
private 
readonly 
Lazy 
< 
IRequestLog )
>) *
	_requests+ 4
;4 5
private   
readonly   
Lazy   
<   
ServiceInventory   .
>  . /
	_services  0 9
;  9 :
public&& 
MessageGateway&& 
(&& 
IComponentContext&& /

components&&0 :
)&&: ;
{'' 	
Argument(( 
.(( 
NotNull(( 
((( 

components(( '
,((' (
nameof(() /
(((/ 0

components((0 :
)((: ;
)((; <
;((< =
	_services** 
=** 
new** 
Lazy**  
<**  !
ServiceInventory**! 1
>**1 2
(**2 3

components**3 =
.**= >
Resolve**> E
<**E F
ServiceInventory**F V
>**V W
)**W X
;**X Y
_requestContext++ 
=++ 
new++ !
Lazy++" &
<++& '
IRequestContext++' 6
>++6 7
(++7 8

components++8 B
.++B C
Resolve++C J
<++J K
IRequestContext++K Z
>++Z [
)++[ \
;++\ ]
	_requests,, 
=,, 
new,, 
Lazy,,  
<,,  !
IRequestLog,,! ,
>,,, -
(,,- .

components,,. 8
.,,8 9
Resolve,,9 @
<,,@ A
IRequestLog,,A L
>,,L M
),,M N
;,,N O
_dispatcher-- 
=-- 
new-- 
Lazy-- "
<--" #
IRequestRouter--# 1
>--1 2
(--2 3

components--3 =
.--= >
Resolve--> E
<--E F
IRequestRouter--F T
>--T U
)--U V
;--V W
_dispatchers.. 
=.. 
new.. 
Lazy.. #
<..# $
IEnumerable..$ /
<../ 0
IRemoteRouter..0 =
>..= >
>..> ?
(..? @

components..@ J
...J K

ResolveAll..K U
<..U V
IRemoteRouter..V c
>..c d
)..d e
;..e f
}// 	
public22 
virtual22 
async22 
Task22 !
Publish22" )
(22) *
EventMessage22* 6
instance227 ?
,22? @
ExecutionContext22A Q
context22R Y
)22Y Z
{33 	
Argument44 
.44 
NotNull44 
(44 
instance44 %
,44% &
nameof44' -
(44- .
instance44. 6
)446 7
)447 8
;448 9
var66 
request66 
=66 
_requestContext66 )
.66) *
Value66* /
.66/ 0
Resolve660 7
(667 8
instance668 @
,66@ A
context66B I
.66I J
Request66J Q
)66Q R
;66R S
await77 
this77 
.77 

LogRequest77 !
(77! "
request77" )
)77) *
;77* +
var99 
	endPoints99 
=99 
	_services99 %
.99% &
Value99& +
.99+ ,
Find99, 0
(990 1
instance991 9
)999 :
;99: ;
foreach:: 
(:: 
var:: 
endPoint:: !
in::" $
	endPoints::% .
)::. /
{;; 
if<< 
(<< 
endPoint<< 
.<< 
InvokeMethod<< )
.<<) *
GetParameters<<* 7
(<<7 8
)<<8 9
.<<9 :
FirstOrDefault<<: H
(<<H I
)<<I J
?<<J K
.<<K L
ParameterType<<L Y
==<<Z \
instance<<] e
.<<e f
MessageType<<f q
)<<q r
{== 
await>> 
_dispatcher>> %
.>>% &
Value>>& +
.>>+ ,
Route>>, 1
(>>1 2
request>>2 9
,>>9 :
endPoint>>; C
,>>C D
context>>E L
)>>L M
;>>M N
}?? 
else@@ 
{AA 
varBB 
	attributeBB !
=BB" #
endPointBB$ ,
.BB, -
EndPointTypeBB- 9
.BB9 :
GetAllAttributesBB: J
<BBJ K
SubscribeAttributeBBK ]
>BB] ^
(BB^ _
)BB_ `
.BB` a
FirstOrDefaultBBa o
(BBo p
)BBp q
;BBq r
ifCC 
(CC 
	attributeCC !
!=CC" $
nullCC% )
)CC) *
{DD 
ifEE 
(EE 
	attributeEE %
.EE% &
ChannelEE& -
==EE. 0
instanceEE1 9
.EE9 :
NameEE: >
)EE> ?
{FF 
awaitGG !
_dispatcherGG" -
.GG- .
ValueGG. 3
.GG3 4
RouteGG4 9
(GG9 :
requestGG: A
,GGA B
endPointGGC K
,GGK L
contextGGM T
)GGT U
;GGU V
}HH 
}II 
}JJ 
}KK 
}LL 	
publicOO 
asyncOO 
TaskOO 
PublishOO !
(OO! "
IEnumerableOO" -
<OO- .
EventMessageOO. :
>OO: ;
	instancesOO< E
,OOE F
ExecutionContextOOG W
contextOOX _
=OO` a
nullOOb f
)OOf g
{PP 	
ArgumentQQ 
.QQ 
NotNullQQ 
(QQ 
	instancesQQ &
,QQ& '
nameofQQ( .
(QQ. /
	instancesQQ/ 8
)QQ8 9
)QQ9 :
;QQ: ;
foreachSS 
(SS 
varSS 
itemSS 
inSS  
	instancesSS! *
)SS* +
{TT 
awaitUU 
thisUU 
.UU 
PublishUU "
(UU" #
itemUU# '
,UU' (
contextUU) 0
)UU0 1
;UU1 2
}VV 
}WW 	
publicZZ 
voidZZ 
PublishZZ 
(ZZ 
stringZZ "
channelZZ# *
,ZZ* +
stringZZ, 2
messageZZ3 :
)ZZ: ;
{[[ 	
EventMessage\\ 
current\\  
;\\  !
if]] 
(]] 
message]] 
.]] 

StartsWith]] "
(]]" #
$str]]# &
)]]& '
)]]' (
{^^ 
var__ 
instance__ 
=__ 
JsonConvert__ *
.__* +
DeserializeObject__+ <
<__< =
JObject__= D
>__D E
(__E F
message__F M
)__M N
;__N O
varaa 
	requestIdaa 
=aa 
instanceaa  (
[aa( )
$straa) 4
]aa4 5
?aa5 6
.aa6 7
Valueaa7 <
<aa< =
stringaa= C
>aaC D
(aaD E
)aaE F
??aaG I
NewIdaaJ O
.aaO P
NextIdaaP V
(aaV W
)aaW X
;aaX Y
varbb 
bodybb 
=bb 
instancebb #
[bb# $
$strbb$ *
]bb* +
?bb+ ,
.bb, -
ToObjectbb- 5
<bb5 6
objectbb6 <
>bb< =
(bb= >
)bb> ?
??bb@ B
instancebbC K
;bbK L
currentdd 
=dd 
newdd 
EventMessagedd *
(dd* +
	requestIddd+ 4
,dd4 5
bodydd6 :
)dd: ;
;dd; <
}ee 
elseff 
{gg 
currenthh 
=hh 
newhh 
EventMessagehh *
(hh* +
NewIdhh+ 0
.hh0 1
NextIdhh1 7
(hh7 8
)hh8 9
,hh9 :
messagehh; B
)hhB C
;hhC D
}ii 
foreachkk 
(kk 
varkk 
endPointkk !
inkk" $
	_serviceskk% .
.kk. /
Valuekk/ 4
.kk4 5
	EndPointskk5 >
.kk> ?
Wherekk? D
(kkD E
ekkE F
=>kkG I
ekkJ K
.kkK L
EndPointTypekkL X
.kkX Y
GetAllAttributeskkY i
<kki j
SubscribeAttributekkj |
>kk| }
(kk} ~
)kk~ 
.	kk Ä
Any
kkÄ É
(
kkÉ Ñ
x
kkÑ Ö
=>
kkÜ à
x
kkâ ä
.
kkä ã
Channel
kkã í
==
kkì ï
channel
kkñ ù
)
kkù û
)
kkû ü
)
kkü †
{ll 
varmm 
requestmm 
=mm 
_requestContextmm -
.mm- .
Valuemm. 3
.mm3 4
Resolvemm4 ;
(mm; <
currentmm< C
,mmC D
endPointmmE M
)mmM N
;mmN O
_dispatcheroo 
.oo 
Valueoo !
.oo! "
Routeoo" '
(oo' (
requestoo( /
,oo/ 0
endPointoo1 9
,oo9 :
nulloo; ?
)oo? @
;oo@ A
}pp 
}qq 	
publictt 
voidtt 
Publishtt 
(tt 
Eventtt !
instancett" *
)tt* +
{uu 	
varvv 
currentvv 
=vv 
newvv 
EventMessagevv *
(vv* +
NewIdvv+ 0
.vv0 1
NextIdvv1 7
(vv7 8
)vv8 9
,vv9 :
instancevv; C
)vvC D
;vvD E
foreachxx 
(xx 
varxx 
endPointxx !
inxx" $
	_servicesxx% .
.xx. /
Valuexx/ 4
.xx4 5
	EndPointsxx5 >
.xx> ?
Wherexx? D
(xxD E
exxE F
=>xxG I
exxJ K
.xxK L
EndPointTypexxL X
.xxX Y
GetAllAttributesxxY i
<xxi j
SubscribeAttributexxj |
>xx| }
(xx} ~
)xx~ 
.	xx Ä
Any
xxÄ É
(
xxÉ Ñ
x
xxÑ Ö
=>
xxÜ à
x
xxâ ä
.
xxä ã
Channel
xxã í
==
xxì ï
current
xxñ ù
.
xxù û
MessageType
xxû ©
.
xx© ™
FullName
xx™ ≤
.
xx≤ ≥
Split
xx≥ ∏
(
xx∏ π
$char
xxπ º
)
xxº Ω
.
xxΩ æ
Last
xxæ ¬
(
xx¬ √
)
xx√ ƒ
)
xxƒ ≈
)
xx≈ ∆
)
xx∆ «
{yy 
varzz 
requestzz 
=zz 
_requestContextzz -
.zz- .
Valuezz. 3
.zz3 4
Resolvezz4 ;
(zz; <
currentzz< C
,zzC D
endPointzzE M
)zzM N
;zzN O
_dispatcher|| 
.|| 
Value|| !
.||! "
Route||" '
(||' (
request||( /
,||/ 0
endPoint||1 9
,||9 :
null||; ?
)||? @
;||@ A
}}} 
}~~ 	
public
ÅÅ 
Task
ÅÅ 
<
ÅÅ 
MessageResult
ÅÅ !
>
ÅÅ! "
Send
ÅÅ# '
(
ÅÅ' (
object
ÅÅ( .
message
ÅÅ/ 6
,
ÅÅ6 7
ExecutionContext
ÅÅ8 H
parentContext
ÅÅI V
=
ÅÅW X
null
ÅÅY ]
,
ÅÅ] ^
TimeSpan
ÅÅ_ g
?
ÅÅg h
timeout
ÅÅi p
=
ÅÅq r
null
ÅÅs w
)
ÅÅw x
{
ÇÇ 	
return
ÉÉ 
this
ÉÉ 
.
ÉÉ 
Send
ÉÉ 
(
ÉÉ 
null
ÉÉ !
,
ÉÉ! "
message
ÉÉ# *
,
ÉÉ* +
parentContext
ÉÉ, 9
,
ÉÉ9 :
timeout
ÉÉ; B
)
ÉÉB C
;
ÉÉC D
}
ÑÑ 	
public
áá 
Task
áá 
<
áá 
MessageResult
áá !
>
áá! "
Send
áá# '
(
áá' (
string
áá( .
path
áá/ 3
,
áá3 4
ExecutionContext
áá5 E
parentContext
ááF S
=
ááT U
null
ááV Z
,
ááZ [
TimeSpan
áá\ d
?
áád e
timeout
ááf m
=
áán o
null
ááp t
)
áát u
{
àà 	
return
ââ 
this
ââ 
.
ââ 
Send
ââ 
(
ââ 
path
ââ !
,
ââ! "
null
ââ# '
,
ââ' (
parentContext
ââ) 6
,
ââ6 7
timeout
ââ8 ?
)
ââ? @
;
ââ@ A
}
ää 	
public
çç 
virtual
çç 
async
çç 
Task
çç !
<
çç! "
MessageResult
çç" /
>
çç/ 0
Send
çç1 5
(
çç5 6
string
çç6 <
path
çç= A
,
ççA B
object
ççC I
instance
ççJ R
,
ççR S
ExecutionContext
ççT d
parentContext
ççe r
=
ççs t
null
ççu y
,
ççy z
TimeSpançç{ É
?ççÉ Ñ
timeoutççÖ å
=ççç é
nullççè ì
)ççì î
{
éé 	
var
èè 
endPoint
èè 
=
èè 
	_services
èè $
.
èè$ %
Value
èè% *
.
èè* +
Find
èè+ /
(
èè/ 0
path
èè0 4
,
èè4 5
instance
èè6 >
)
èè> ?
;
èè? @
if
êê 
(
êê 
endPoint
êê 
!=
êê 
null
êê  
)
êê  !
{
ëë 
var
íí 
request
íí 
=
íí 
_requestContext
íí -
.
íí- .
Value
íí. 3
.
íí3 4
Resolve
íí4 ;
(
íí; <
instance
íí< D
,
ííD E
endPoint
ííF N
,
ííN O
parentContext
ííP ]
?
íí] ^
.
íí^ _
Request
íí_ f
)
ííf g
;
ííg h
await
ìì 
this
ìì 
.
ìì 

LogRequest
ìì %
(
ìì% &
request
ìì& -
)
ìì- .
;
ìì. /
return
îî 
await
îî 
_dispatcher
îî (
.
îî( )
Value
îî) .
.
îî. /
Route
îî/ 4
(
îî4 5
request
îî5 <
,
îî< =
endPoint
îî> F
,
îîF G
parentContext
îîH U
,
îîU V
timeout
îîW ^
)
îî^ _
;
îî_ `
}
ïï 
else
ññ 
{
óó 
var
òò 
request
òò 
=
òò 
_requestContext
òò -
.
òò- .
Value
òò. 3
.
òò3 4
Resolve
òò4 ;
(
òò; <
path
òò< @
,
òò@ A
instance
òòB J
,
òòJ K
parentContext
òòL Y
?
òòY Z
.
òòZ [
Request
òò[ b
)
òòb c
;
òòc d
await
ôô 
this
ôô 
.
ôô 

LogRequest
ôô %
(
ôô% &
request
ôô& -
)
ôô- .
;
ôô. /
var
öö 

dispatcher
öö 
=
öö  
_dispatchers
öö! -
.
öö- .
Value
öö. 3
.
öö3 4
FirstOrDefault
öö4 B
(
ööB C
e
ööC D
=>
ööE G
e
ööH I
.
ööI J
CanRoute
ööJ R
(
ööR S
request
ööS Z
)
ööZ [
)
öö[ \
;
öö\ ]
if
õõ 
(
õõ 

dispatcher
õõ 
!=
õõ !
null
õõ" &
)
õõ& '
{
úú 
return
ùù 
await
ùù  

dispatcher
ùù! +
.
ùù+ ,
Route
ùù, 1
(
ùù1 2
request
ùù2 9
,
ùù9 :
parentContext
ùù; H
,
ùùH I
timeout
ùùJ Q
)
ùùQ R
;
ùùR S
}
ûû 
}
üü 
var
°° 
current
°° 
=
°° 
_requestContext
°° )
.
°°) *
Value
°°* /
.
°°/ 0
Resolve
°°0 7
(
°°7 8
path
°°8 <
,
°°< =
instance
°°> F
)
°°F G
;
°°G H
var
¢¢ 
context
¢¢ 
=
¢¢ 
new
¢¢ 
ExecutionContext
¢¢ .
(
¢¢. /
current
¢¢/ 6
,
¢¢6 7
null
¢¢8 <
)
¢¢< =
;
¢¢= >
context
££ 
.
££ 
SetException
££  
(
££  !
new
££! $'
EndPointNotFoundException
££% >
(
££> ?
current
££? F
)
££F G
)
££G H
;
££H I
return
§§ 
new
§§ 
MessageResult
§§ $
(
§§$ %
context
§§% ,
)
§§, -
;
§§- .
}
•• 	
public
®® 
virtual
®® 
async
®® 
Task
®® !
<
®®! "
MessageResult
®®" /
>
®®/ 0
Send
®®1 5
(
®®5 6
string
®®6 <
path
®®= A
,
®®A B
string
®®C I
command
®®J Q
,
®®Q R
ExecutionContext
®®S c
parentContext
®®d q
=
®®r s
null
®®t x
,
®®x y
TimeSpan®®z Ç
?®®Ç É
timeout®®Ñ ã
=®®å ç
null®®é í
)®®í ì
{
©© 	
var
™™ 
endPoint
™™ 
=
™™ 
	_services
™™ $
.
™™$ %
Value
™™% *
.
™™* +
Find
™™+ /
(
™™/ 0
path
™™0 4
)
™™4 5
;
™™5 6
if
´´ 
(
´´ 
endPoint
´´ 
!=
´´ 
null
´´  
)
´´  !
{
¨¨ 
var
≠≠ 
request
≠≠ 
=
≠≠ 
_requestContext
≠≠ -
.
≠≠- .
Value
≠≠. 3
.
≠≠3 4
Resolve
≠≠4 ;
(
≠≠; <
command
≠≠< C
,
≠≠C D
endPoint
≠≠E M
,
≠≠M N
parentContext
≠≠O \
?
≠≠\ ]
.
≠≠] ^
Request
≠≠^ e
)
≠≠e f
;
≠≠f g
await
ÆÆ 
this
ÆÆ 
.
ÆÆ 

LogRequest
ÆÆ %
(
ÆÆ% &
request
ÆÆ& -
)
ÆÆ- .
;
ÆÆ. /
return
ØØ 
await
ØØ 
_dispatcher
ØØ (
.
ØØ( )
Value
ØØ) .
.
ØØ. /
Route
ØØ/ 4
(
ØØ4 5
request
ØØ5 <
,
ØØ< =
endPoint
ØØ> F
,
ØØF G
parentContext
ØØH U
,
ØØU V
timeout
ØØW ^
)
ØØ^ _
;
ØØ_ `
}
∞∞ 
else
±± 
{
≤≤ 
var
≥≥ 
request
≥≥ 
=
≥≥ 
_requestContext
≥≥ -
.
≥≥- .
Value
≥≥. 3
.
≥≥3 4
Resolve
≥≥4 ;
(
≥≥; <
path
≥≥< @
,
≥≥@ A
command
≥≥B I
,
≥≥I J
parentContext
≥≥K X
?
≥≥X Y
.
≥≥Y Z
Request
≥≥Z a
)
≥≥a b
;
≥≥b c
await
¥¥ 
this
¥¥ 
.
¥¥ 

LogRequest
¥¥ %
(
¥¥% &
request
¥¥& -
)
¥¥- .
;
¥¥. /
var
µµ 

dispatcher
µµ 
=
µµ  
_dispatchers
µµ! -
.
µµ- .
Value
µµ. 3
.
µµ3 4
FirstOrDefault
µµ4 B
(
µµB C
e
µµC D
=>
µµE G
e
µµH I
.
µµI J
CanRoute
µµJ R
(
µµR S
request
µµS Z
)
µµZ [
)
µµ[ \
;
µµ\ ]
if
∂∂ 
(
∂∂ 

dispatcher
∂∂ 
!=
∂∂ !
null
∂∂" &
)
∂∂& '
{
∑∑ 
return
∏∏ 
await
∏∏  

dispatcher
∏∏! +
.
∏∏+ ,
Route
∏∏, 1
(
∏∏1 2
request
∏∏2 9
,
∏∏9 :
parentContext
∏∏; H
,
∏∏H I
timeout
∏∏J Q
)
∏∏Q R
;
∏∏R S
}
ππ 
}
∫∫ 
var
ºº 
current
ºº 
=
ºº 
_requestContext
ºº )
.
ºº) *
Value
ºº* /
.
ºº/ 0
Resolve
ºº0 7
(
ºº7 8
path
ºº8 <
,
ºº< =
command
ºº> E
)
ººE F
;
ººF G
var
ΩΩ 
context
ΩΩ 
=
ΩΩ 
new
ΩΩ 
ExecutionContext
ΩΩ .
(
ΩΩ. /
current
ΩΩ/ 6
,
ΩΩ6 7
null
ΩΩ8 <
)
ΩΩ< =
;
ΩΩ= >
context
ææ 
.
ææ 
SetException
ææ  
(
ææ  !
new
ææ! $'
EndPointNotFoundException
ææ% >
(
ææ> ?
current
ææ? F
)
ææF G
)
ææG H
;
ææH I
return
øø 
new
øø 
MessageResult
øø $
(
øø$ %
context
øø% ,
)
øø, -
;
øø- .
}
¿¿ 	
private
¬¬ 
async
¬¬ 
Task
¬¬ 

LogRequest
¬¬ %
(
¬¬% &
Request
¬¬& -
request
¬¬. 5
)
¬¬5 6
{
√√ 	
if
ƒƒ 
(
ƒƒ 
request
ƒƒ 
.
ƒƒ 
Path
ƒƒ 
?
ƒƒ 
.
ƒƒ 

StartsWith
ƒƒ (
(
ƒƒ( )
$str
ƒƒ) ,
)
ƒƒ, -
==
ƒƒ. 0
false
ƒƒ1 6
)
ƒƒ6 7
{
≈≈ 
await
∆∆ 
	_requests
∆∆ 
.
∆∆  
Value
∆∆  %
.
∆∆% &
Append
∆∆& ,
(
∆∆, -
request
∆∆- 4
)
∆∆4 5
;
∆∆5 6
}
«« 
}
»» 	
}
…… 
}   «C
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\MessageResult.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
MessageResult 
< 
T  
>  !
:" #
MessageResult$ 1
{ 
public 
MessageResult 
( 
ExecutionContext -
context. 5
)5 6
: 
base 
( 
context 
) 
{ 	
} 	
public$$ 
MessageResult$$ 
($$ 
MessageResult$$ *
instance$$+ 3
)$$3 4
{%% 	
this&& 
.&& 
CorrelationId&& 
=&&  
instance&&! )
.&&) *
CorrelationId&&* 7
;&&7 8
this'' 
.'' 
Started'' 
='' 
instance'' #
.''# $
Started''$ +
;''+ ,
this(( 
.(( 
	Completed(( 
=(( 
instance(( %
.((% &
	Completed((& /
;((/ 0
this)) 
.)) 
RaisedException))  
=))! "
instance))# +
.))+ ,
RaisedException)), ;
;)); <
this** 
.** 
ValidationErrors** !
=**" #
instance**$ ,
.**, -
ValidationErrors**- =
.**= >
ToList**> D
(**D E
)**E F
;**F G
this++ 
.++ 
	RequestId++ 
=++ 
instance++ %
.++% &
	RequestId++& /
;++/ 0
this,, 
.,, 
IsCancelled,, 
=,, 
instance,, '
.,,' (
IsCancelled,,( 3
;,,3 4
if-- 
(-- 
instance-- 
.-- 
Response-- !
is--" $
T--% &
)--& '
{.. 
this// 
.// 
Response// 
=// 
(//  !
T//! "
)//" #
instance//# +
.//+ ,
Response//, 4
;//4 5
}00 
else11 
if11 
(11 
instance11 
.11 
Response11 &
is11' )
string11* 0
)110 1
{22 
this33 
.33 
Response33 
=33 
JsonConvert33  +
.33+ ,
DeserializeObject33, =
<33= >
T33> ?
>33? @
(33@ A
(33A B
string33B H
)33H I
instance33I Q
.33Q R
Response33R Z
)33Z [
;33[ \
}44 
else55 
{66 
this77 
.77 
Response77 
=77 
JsonConvert77  +
.77+ ,
DeserializeObject77, =
<77= >
T77> ?
>77? @
(77@ A
JsonConvert77A L
.77L M
SerializeObject77M \
(77\ ]
instance77] e
.77e f
Response77f n
,77n o)
DefaultSerializationSettings	77p å
.
77å ç
Instance
77ç ï
)
77ï ñ
,
77ñ ó*
DefaultSerializationSettings
77ò ¥
.
77¥ µ
Instance
77µ Ω
)
77Ω æ
;
77æ ø
}88 
}99 	
public?? 
new?? 
T?? 
Response?? 
{@@ 	
getAA 
{AA 
returnAA 
(AA 
TAA 
)AA 
baseAA  
.AA  !
ResponseAA! )
;AA) *
}AA+ ,
setBB 
{BB 
baseBB 
.BB 
ResponseBB 
=BB  !
valueBB" '
;BB' (
}BB) *
}CC 	
}DD 
publicJJ 

classJJ 
MessageResultJJ 
{KK 
	protectedOO 
MessageResultOO 
(OO  
)OO  !
{PP 	
}QQ 	
publicWW 
MessageResultWW 
(WW 
ExecutionContextWW -
contextWW. 5
)WW5 6
{XX 	
thisYY 
.YY 
CorrelationIdYY 
=YY  
contextYY! (
.YY( )
RequestYY) 0
.YY0 1
CorrelationIdYY1 >
;YY> ?
thisZZ 
.ZZ 
StartedZZ 
=ZZ 
DateTimeOffsetZZ )
.ZZ) *
UtcNowZZ* 0
;ZZ0 1
this[[ 
.[[ 
	Completed[[ 
=[[ 
context[[ $
.[[$ %
	Completed[[% .
;[[. /
this\\ 
.\\ 
RaisedException\\  
=\\! "
context\\# *
.\\* +
	Exception\\+ 4
;\\4 5
this]] 
.]] 
Response]] 
=]] 
context]] #
.]]# $
Response]]$ ,
;]], -
this^^ 
.^^ 
ValidationErrors^^ !
=^^" #
context^^$ +
.^^+ ,
ValidationErrors^^, <
.^^< =
ToList^^= C
(^^C D
)^^D E
;^^E F
this__ 
.__ 
	RequestId__ 
=__ 
context__ $
.__$ %
Request__% ,
.__, -
Message__- 4
.__4 5
Id__5 7
;__7 8
this`` 
.`` 
IsCancelled`` 
=`` 
context`` &
.``& '
CancellationToken``' 8
.``8 9#
IsCancellationRequested``9 P
;``P Q
}aa 	
publicgg 
DateTimeOffsetgg 
?gg 
	Completedgg (
{gg) *
getgg+ .
;gg. /
	protectedgg0 9
setgg: =
;gg= >
}gg? @
publicmm 
stringmm 
CorrelationIdmm #
{mm$ %
getmm& )
;mm) *
	protectedmm+ 4
setmm5 8
;mm8 9
}mm: ;
publicss 
TimeSpanss 
?ss 
Elapsedss  
{tt 	
getuu 
{vv 
varww 
timeSpanww 
=ww 
thisww #
.ww# $
	Completedww$ -
-ww. /
thisww0 4
.ww4 5
Startedww5 <
;ww< =
ifxx 
(xx 
timeSpanxx 
!=xx 
nullxx  $
)xx$ %
{yy 
returnzz 
newzz 
TimeSpanzz '
(zz' (
Mathzz( ,
.zz, -
Maxzz- 0
(zz0 1
timeSpanzz1 9
.zz9 :
Valuezz: ?
.zz? @
Tickszz@ E
,zzE F
$numzzG H
)zzH I
)zzI J
;zzJ K
}{{ 
return|| 
TimeSpan|| 
.||  
Zero||  $
;||$ %
}}} 
}~~ 	
public
ÑÑ 
bool
ÑÑ 
IsCancelled
ÑÑ 
{
ÑÑ  !
get
ÑÑ" %
;
ÑÑ% &
	protected
ÑÑ' 0
set
ÑÑ1 4
;
ÑÑ4 5
}
ÑÑ6 7
public
ää 
bool
ää 
IsSuccessful
ää  
=>
ää! #
!
ää$ %
this
ää% )
.
ää) *
ValidationErrors
ää* :
.
ää: ;
Any
ää; >
(
ää> ?
)
ää? @
&&
ääA C
this
ääD H
.
ääH I
RaisedException
ääI X
==
ääY [
null
ää\ `
;
ää` a
public
êê 
	Exception
êê 
RaisedException
êê (
{
êê) *
get
êê+ .
;
êê. /
	protected
êê0 9
set
êê: =
;
êê= >
}
êê? @
public
ññ 
string
ññ 
	RequestId
ññ 
{
ññ  !
get
ññ" %
;
ññ% &
	protected
ññ' 0
set
ññ1 4
;
ññ4 5
}
ññ6 7
public
õõ 
object
õõ 
Response
õõ 
{
õõ  
get
õõ! $
;
õõ$ %
internal
õõ& .
set
õõ/ 2
;
õõ2 3
}
õõ4 5
public
°° 
DateTimeOffset
°° 
Started
°° %
{
°°& '
get
°°( +
;
°°+ ,
	protected
°°- 6
set
°°7 :
;
°°: ;
}
°°< =
public
ßß 
IReadOnlyList
ßß 
<
ßß 
ValidationError
ßß ,
>
ßß, -
ValidationErrors
ßß. >
{
ßß? @
get
ßßA D
;
ßßD E
	protected
ßßF O
set
ßßP S
;
ßßS T
}
ßßU V
}
®® 
}©© Ój
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\Request.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
Request 
: 
IRequestContext *
{ 
private 
string 

_sessionId !
;! "
public 
string 
CorrelationId #
{$ %
get& )
;) *
private+ 2
set3 6
;6 7
}8 9
public$$ 
IMessage$$ 
Message$$ 
{$$  !
get$$" %
;$$% &
private$$' .
set$$/ 2
;$$2 3
}$$4 5
public** 
Request** 
Parent** 
{** 
get**  #
;**# $
private**% ,
set**- 0
;**0 1
}**2 3
public00 
string00 
Path00 
{00 
get00  
;00  !
private00" )
set00* -
;00- .
}00/ 0
public66 
string66 
	SessionId66 
{66  !
get66" %
;66% &
private66' .
set66/ 2
;662 3
}664 5
public<< 
string<< 
SourceAddress<< #
{<<$ %
get<<& )
;<<) *
private<<+ 2
set<<3 6
;<<6 7
}<<8 9
[BB 	
JsonConverterBB	 
(BB 
typeofBB 
(BB $
ClaimsPrincipalConverterBB 6
)BB6 7
)BB7 8
]BB8 9
publicCC 
ClaimsPrincipalCC 
UserCC #
{CC$ %
getCC& )
;CC) *
privateCC+ 2
setCC3 6
;CC6 7
}CC8 9
publicFF 
RequestFF 
ResolveFF 
(FF 
objectFF %
messageFF& -
,FF- .
EndPointMetaDataFF/ ?
endPointFF@ H
,FFH I
RequestFFJ Q
parentFFR X
=FFY Z
nullFF[ _
)FF_ `
{GG 	
returnHH 
newHH 
RequestHH 
{II 
CorrelationIdJJ 
=JJ 
parentJJ  &
?JJ& '
.JJ' (
CorrelationIdJJ( 5
??JJ6 8
thisJJ9 =
.JJ= >
GetCorrelationIdJJ> N
(JJN O
)JJO P
,JJP Q
SourceAddressKK 
=KK 
parentKK  &
?KK& '
.KK' (
SourceAddressKK( 5
??KK6 8
thisKK9 =
.KK= >
GetSourceIPAddressKK> P
(KKP Q
)KKQ R
,KKR S
	SessionIdLL 
=LL 
parentLL "
?LL" #
.LL# $
	SessionIdLL$ -
??LL. 0
thisLL1 5
.LL5 6

GetSessionLL6 @
(LL@ A
)LLA B
,LLB C
UserMM 
=MM 
parentMM 
?MM 
.MM 
UserMM #
??MM$ &
thisMM' +
.MM+ ,
GetUserMM, 3
(MM3 4
)MM4 5
,MM5 6
ParentNN 
=NN 
parentNN 
,NN  
PathOO 
=OO 
endPointOO 
.OO  
PathOO  $
,OO$ %
MessagePP 
=PP 
thisPP 
.PP 

GetMessagePP )
(PP) *
messagePP* 1
,PP1 2
endPointPP3 ;
)PP; <
}QQ 
;QQ 
}RR 	
publicUU 
RequestUU 
ResolveUU 
(UU 
stringUU %
pathUU& *
,UU* +
objectUU, 2
messageUU3 :
,UU: ;
RequestUU< C
parentUUD J
=UUK L
nullUUM Q
)UUQ R
{VV 	
returnWW 
newWW 
RequestWW 
{XX 
CorrelationIdYY 
=YY 
parentYY  &
?YY& '
.YY' (
CorrelationIdYY( 5
??YY6 8
thisYY9 =
.YY= >
GetCorrelationIdYY> N
(YYN O
)YYO P
,YYP Q
SourceAddressZZ 
=ZZ 
parentZZ  &
?ZZ& '
.ZZ' (
SourceAddressZZ( 5
??ZZ6 8
thisZZ9 =
.ZZ= >
GetSourceIPAddressZZ> P
(ZZP Q
)ZZQ R
,ZZR S
	SessionId[[ 
=[[ 
parent[[ "
?[[" #
.[[# $
	SessionId[[$ -
??[[. 0
this[[1 5
.[[5 6

GetSession[[6 @
([[@ A
)[[A B
,[[B C
User\\ 
=\\ 
parent\\ 
?\\ 
.\\ 
User\\ #
??\\$ &
this\\' +
.\\+ ,
GetUser\\, 3
(\\3 4
)\\4 5
,\\5 6
Parent]] 
=]] 
parent]] 
,]]  
Path^^ 
=^^ 
path^^ 
??^^ 
message^^ &
?^^& '
.^^' (
GetType^^( /
(^^/ 0
)^^0 1
.^^1 2
GetAllAttributes^^2 B
<^^B C
RequestAttribute^^C S
>^^S T
(^^T U
)^^U V
.^^V W
FirstOrDefault^^W e
(^^e f
)^^f g
?^^g h
.^^h i
Path^^i m
,^^m n
Message__ 
=__ 
this__ 
.__ 

GetMessage__ )
(__) *
path__* .
,__. /
message__0 7
)__7 8
}`` 
;`` 
}aa 	
publicdd 
Requestdd 
Resolvedd 
(dd 
stringdd %
commanddd& -
,dd- .
EndPointMetaDatadd/ ?
endPointdd@ H
,ddH I
RequestddJ Q
parentddR X
=ddY Z
nulldd[ _
)dd_ `
{ee 	
returnff 
newff 
Requestff 
{gg 
CorrelationIdhh 
=hh 
parenthh  &
?hh& '
.hh' (
CorrelationIdhh( 5
??hh6 8
thishh9 =
.hh= >
GetCorrelationIdhh> N
(hhN O
)hhO P
,hhP Q
SourceAddressii 
=ii 
parentii  &
?ii& '
.ii' (
SourceAddressii( 5
??ii6 8
thisii9 =
.ii= >
GetSourceIPAddressii> P
(iiP Q
)iiQ R
,iiR S
	SessionIdjj 
=jj 
parentjj "
?jj" #
.jj# $
	SessionIdjj$ -
??jj. 0
thisjj1 5
.jj5 6

GetSessionjj6 @
(jj@ A
)jjA B
,jjB C
Userkk 
=kk 
parentkk 
?kk 
.kk 
Userkk #
??kk$ &
thiskk' +
.kk+ ,
GetUserkk, 3
(kk3 4
)kk4 5
,kk5 6
Parentll 
=ll 
parentll 
,ll  
Pathmm 
=mm 
endPointmm 
.mm  
Pathmm  $
,mm$ %
Messagenn 
=nn 
thisnn 
.nn 

GetMessagenn )
(nn) *
commandnn* 1
,nn1 2
endPointnn3 ;
)nn; <
}oo 
;oo 
}pp 	
publicss 
Requestss 
Resolvess 
(ss 
EventMessagess +
instancess, 4
,ss4 5
Requestss6 =
parentss> D
)ssD E
{tt 	
returnuu 
newuu 
Requestuu 
{vv 
CorrelationIdww 
=ww 
parentww  &
.ww& '
CorrelationIdww' 4
,ww4 5
SourceAddressxx 
=xx 
parentxx  &
.xx& '
SourceAddressxx' 4
,xx4 5
	SessionIdyy 
=yy 
parentyy "
.yy" #
	SessionIdyy# ,
,yy, -
Userzz 
=zz 
parentzz 
.zz 
Userzz "
,zz" #
Parent{{ 
={{ 
parent{{ 
,{{  
Message|| 
=|| 
instance|| "
}}} 
;}} 
}~~ 	
	protected
ÑÑ 
virtual
ÑÑ 
string
ÑÑ  
GetCorrelationId
ÑÑ! 1
(
ÑÑ1 2
)
ÑÑ2 3
{
ÖÖ 	
return
ÜÜ 
NewId
ÜÜ 
.
ÜÜ 
NextId
ÜÜ 
(
ÜÜ  
)
ÜÜ  !
;
ÜÜ! "
}
áá 	
	protected
çç 
virtual
çç 
string
çç  

GetSession
çç! +
(
çç+ ,
)
çç, -
{
éé 	
return
èè 

_sessionId
èè 
??
èè  
(
èè! "

_sessionId
èè" ,
=
èè- .
NewId
èè/ 4
.
èè4 5
NextId
èè5 ;
(
èè; <
)
èè< =
)
èè= >
;
èè> ?
}
êê 	
	protected
ññ 
virtual
ññ 
string
ññ   
GetSourceIPAddress
ññ! 3
(
ññ3 4
)
ññ4 5
{
óó 	
return
òò 
$str
òò 
;
òò 
}
ôô 	
	protected
üü 
virtual
üü 
ClaimsPrincipal
üü )
GetUser
üü* 1
(
üü1 2
)
üü2 3
{
†† 	
return
°° 
ClaimsPrincipal
°° "
.
°°" #
Current
°°# *
;
°°* +
}
¢¢ 	
private
§§ 
IMessage
§§ 

GetMessage
§§ #
(
§§# $
string
§§$ *
path
§§+ /
,
§§/ 0
object
§§1 7
message
§§8 ?
)
§§? @
{
•• 	
if
¶¶ 
(
¶¶ 
message
¶¶ 
is
¶¶ 
IMessage
¶¶ #
)
¶¶# $
{
ßß 
return
®® 
(
®® 
IMessage
®®  
)
®®  !
message
®®! (
;
®®( )
}
©© 
return
™™ 
message
™™ 
==
™™ 
null
™™ "
?
™™# $
new
™™% (
Message
™™) 0
(
™™0 1
)
™™1 2
:
™™3 4
new
™™5 8
Message
™™9 @
(
™™@ A
message
™™A H
)
™™H I
;
™™I J
}
´´ 	
private
≠≠ 
IMessage
≠≠ 

GetMessage
≠≠ #
(
≠≠# $
string
≠≠$ *
message
≠≠+ 2
,
≠≠2 3
EndPointMetaData
≠≠4 D
endPoint
≠≠E M
)
≠≠M N
{
ÆÆ 	
if
ØØ 
(
ØØ 
message
ØØ 
==
ØØ 
null
ØØ 
)
ØØ  
{
∞∞ 
if
±± 
(
±± 
endPoint
±± 
.
±± 
RequestType
±± (
!=
±±) +
typeof
±±, 2
(
±±2 3
object
±±3 9
)
±±9 :
)
±±: ;
{
≤≤ 
return
≥≥ 
new
≥≥ 
Message
≥≥ &
(
≥≥& '
JsonConvert
≥≥' 2
.
≥≥2 3
DeserializeObject
≥≥3 D
(
≥≥D E
$str
≥≥E I
,
≥≥I J
endPoint
≥≥K S
.
≥≥S T
RequestType
≥≥T _
)
≥≥_ `
)
≥≥` a
;
≥≥a b
}
¥¥ 
return
µµ 
new
µµ 
Message
µµ "
(
µµ" #
)
µµ# $
;
µµ$ %
}
∂∂ 
return
∑∑ 
new
∑∑ 
Message
∑∑ 
(
∑∑ 
JsonConvert
∑∑ *
.
∑∑* +
DeserializeObject
∑∑+ <
(
∑∑< =
message
∑∑= D
,
∑∑D E
endPoint
∑∑F N
.
∑∑N O
RequestType
∑∑O Z
)
∑∑Z [
)
∑∑[ \
;
∑∑\ ]
}
∏∏ 	
private
∫∫ 
IMessage
∫∫ 

GetMessage
∫∫ #
(
∫∫# $
object
∫∫$ *
message
∫∫+ 2
,
∫∫2 3
EndPointMetaData
∫∫4 D
endPoint
∫∫E M
)
∫∫M N
{
ªª 	
if
ºº 
(
ºº 
message
ºº 
!=
ºº 
null
ºº 
&&
ºº  "
message
ºº# *
.
ºº* +
GetType
ºº+ 2
(
ºº2 3
)
ºº3 4
==
ºº5 7
endPoint
ºº8 @
.
ºº@ A
RequestType
ººA L
)
ººL M
{
ΩΩ 
return
ææ 
new
ææ 
Message
ææ "
(
ææ" #
message
ææ# *
)
ææ* +
;
ææ+ ,
}
øø 
if
¿¿ 
(
¿¿ 
message
¿¿ 
!=
¿¿ 
null
¿¿ 
)
¿¿  
{
¡¡ 
var
¬¬ 
content
¬¬ 
=
¬¬ 
JsonConvert
¬¬ )
.
¬¬) *
SerializeObject
¬¬* 9
(
¬¬9 :
(
¬¬: ;
message
¬¬; B
as
¬¬C E
EventMessage
¬¬F R
)
¬¬R S
?
¬¬S T
.
¬¬T U
Body
¬¬U Y
??
¬¬Z \
message
¬¬] d
)
¬¬d e
;
¬¬e f
return
√√ 
new
√√ 
Message
√√ "
(
√√" #
JsonConvert
√√# .
.
√√. /
DeserializeObject
√√/ @
(
√√@ A
content
√√A H
,
√√H I
endPoint
√√J R
.
√√R S
RequestType
√√S ^
)
√√^ _
)
√√_ `
;
√√` a
}
ƒƒ 
return
≈≈ 
new
≈≈ 
Message
≈≈ 
(
≈≈ 
JsonConvert
≈≈ *
.
≈≈* +
DeserializeObject
≈≈+ <
(
≈≈< =
$str
≈≈= A
,
≈≈A B
endPoint
≈≈C K
.
≈≈K L
RequestType
≈≈L W
)
≈≈W X
)
≈≈X Y
;
≈≈Y Z
}
∆∆ 	
}
«« 
}»» ì
NC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\RequestAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
	Messaging

! *
{ 
public 

class 
RequestAttribute !
:" #
	Attribute$ -
{ 
public 
RequestAttribute 
(  
string  &
path' +
)+ ,
{ 	
this 
. 
Path 
= 
path 
; 
} 	
public 
string 
Method 
{ 
get "
;" #
set$ '
;' (
}) *
public$$ 
string$$ 
Path$$ 
{$$ 
get$$  
;$$  !
}$$" #
}%% 
}&& ≈.
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Messaging\RequestRouter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
	Messaging! *
{ 
public 

class 
RequestRouter 
:  
IRequestRouter! /
{ 
private 
readonly 
IComponentContext *
_components+ 6
;6 7
public   
RequestRouter   
(   
IComponentContext   .

components  / 9
)  9 :
{!! 	
_components"" 
="" 

components"" $
;""$ %
}## 	
public&& 
virtual&& 
async&& 
Task&& !
<&&! "
MessageResult&&" /
>&&/ 0
Route&&1 6
(&&6 7
Request&&7 >
request&&? F
,&&F G
EndPointMetaData&&H X
endPoint&&Y a
,&&a b
ExecutionContext&&c s
parentContext	&&t Å
,
&&Å Ç
TimeSpan
&&É ã
?
&&ã å
timeout
&&ç î
=
&&ï ñ
null
&&ó õ
)
&&õ ú
{'' 	#
CancellationTokenSource(( #
source(($ *
;((* +
if)) 
()) 
timeout)) 
.)) 
HasValue))  
||))! #
endPoint))$ ,
.)), -
Timeout))- 4
.))4 5
HasValue))5 =
)))= >
{** 
source++ 
=++ 
new++ #
CancellationTokenSource++ 4
(++4 5
timeout++5 <
??++= ?
endPoint++@ H
.++H I
Timeout++I P
.++P Q
Value++Q V
)++V W
;++W X
},, 
else-- 
{.. 
source// 
=// 
new// #
CancellationTokenSource// 4
(//4 5
)//5 6
;//6 7
}00 
var22 
context22 
=22 
new22 
ExecutionContext22 .
(22. /
request22/ 6
,226 7
endPoint228 @
,22@ A
source22B H
.22H I
Token22I N
,22N O
parentContext22P ]
)22] ^
;22^ _
var44 
handler44 
=44 
_components44 %
.44% &
Resolve44& -
(44- .
endPoint44. 6
.446 7
EndPointType447 C
)44C D
;44D E
var55 
service55 
=55 
handler55 !
as55" $
	IEndPoint55% .
;55. /
if66 
(66 
service66 
!=66 
null66 
)66  
{77 
service88 
.88 
Context88 
=88  !
context88" )
;88) *
}99 
var;; 
body;; 
=;; 
request;; 
.;; 
Message;; &
.;;& '
Body;;' +
;;;+ ,
var<< 
parameterType<< 
=<< 
endPoint<<  (
.<<( )
InvokeMethod<<) 5
.<<5 6
GetParameters<<6 C
(<<C D
)<<D E
.<<E F
First<<F K
(<<K L
)<<L M
.<<M N
ParameterType<<N [
;<<[ \
if== 
(== 
body== 
==== 
null== 
||== 
body==  $
.==$ %
GetType==% ,
(==, -
)==- .
!===/ 1
parameterType==2 ?
)==? @
{>> 
body?? 
=?? 
JsonConvert?? "
.??" #
DeserializeObject??# 4
(??4 5
JsonConvert??5 @
.??@ A
SerializeObject??A P
(??P Q
body??Q U
????V X
$str??Y [
)??[ \
,??\ ]
parameterType??^ k
)??k l
;??l m
}@@ 
awaitBB 
(BB 
TaskBB 
)BB 
endPointBB  
.BB  !
InvokeMethodBB! -
.BB- .
InvokeBB. 4
(BB4 5
handlerBB5 <
,BB< =
newBB> A
[BBA B
]BBB C
{BBD E
bodyBBF J
}BBK L
)BBL M
;BBM N
awaitDD 
thisDD 
.DD 
CompleteDD 
(DD  
contextDD  '
)DD' (
;DD( )
returnFF 
newFF 
MessageResultFF $
(FF$ %
contextFF% ,
)FF, -
;FF- .
}GG 	
	protectedNN 
virtualNN 
asyncNN 
TaskNN  $
CompleteNN% -
(NN- .
ExecutionContextNN. >
contextNN? F
)NNF G
{OO 	
varPP 
stepsPP 
=PP 
newPP 
ListPP  
<PP  !!
IMessageExecutionStepPP! 6
>PP6 7
{QQ 
_componentsRR 
.RR 
ResolveRR #
<RR# $
HandleExceptionRR$ 3
>RR3 4
(RR4 5
)RR5 6
,RR6 7
_componentsSS 
.SS 
ResolveSS #
<SS# $
CompleteSS$ ,
>SS, -
(SS- .
)SS. /
,SS/ 0
_componentsTT 
.TT 
ResolveTT #
<TT# $
PublishEventsTT$ 1
>TT1 2
(TT2 3
)TT3 4
,TT4 5
_componentsUU 
.UU 
ResolveUU #
<UU# $
LogCompletionUU$ 1
>UU1 2
(UU2 3
)UU3 4
}VV 
;VV 
foreachWW 
(WW 
varWW 
stepWW 
inWW  
stepsWW! &
)WW& '
{XX 
awaitYY 
stepYY 
.YY 
ExecuteYY "
(YY" #
contextYY# *
)YY* +
;YY+ ,
}ZZ 
}[[ 	
}\\ 
}]] ﬁU
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Modules\ServicesModule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Modules! (
{ 
internal 
class 
ServicesModule !
:" #
Module$ *
{ 
private 
readonly 
Stack 
_stack %
;% &
public## 
ServicesModule## 
(## 
Stack## #
stack##$ )
)##) *
{$$ 	
_stack%% 
=%% 
stack%% 
;%% 
}&& 	
	protected00 
override00 
void00 
Load00  $
(00$ %
ContainerBuilder00% 5
builder006 =
)00= >
{11 	
base22 
.22 
Load22 
(22 
builder22 
)22 
;22 
builder44 
.44 
Register44 
(44 
c44 
=>44 !
new44" %
MessageGateway44& 4
(444 5
c445 6
.446 7
Resolve447 >
<44> ?
IComponentContext44? P
>44P Q
(44Q R
)44R S
)44S T
)44T U
.55 
As55 
<55 
IMessageGateway55 #
>55# $
(55$ %
)55% &
.66 
SingleInstance66 
(66  
)66  !
;66! "
builder88 
.88 
RegisterType88  
<88  !
RequestRouter88! .
>88. /
(88/ 0
)880 1
.881 2
As882 4
<884 5
IRequestRouter885 C
>88C D
(88D E
)88E F
;88F G
builder99 
.99 
RegisterType99  
<99  !"
RemoteServiceInventory99! 7
>997 8
(998 9
)999 :
.99: ;
AsSelf99; A
(99A B
)99B C
.99C D
SingleInstance99D R
(99R S
)99S T
;99T U
builder;; 
.;; 
RegisterType;;  
<;;  !
InMemoryEventStore;;! 3
>;;3 4
(;;4 5
);;5 6
.;;6 7
As;;7 9
<;;9 :
IEventStore;;: E
>;;E F
(;;F G
);;G H
.;;H I
SingleInstance;;I W
(;;W X
);;X Y
;;;Y Z
builder== 
.== !
RegisterAssemblyTypes== )
(==) *
_stack==* 0
.==0 1

Assemblies==1 ;
.==; <
Union==< A
(==A B
new==B E
[==E F
]==F G
{==H I
typeof==J P
(==P Q!
IMessageExecutionStep==Q f
)==f g
.==g h
GetTypeInfo==h s
(==s t
)==t u
.==u v
Assembly==v ~
}	== Ä
)
==Ä Å
.
==Å Ç
Distinct
==Ç ä
(
==ä ã
)
==ã å
.
==å ç
ToArray
==ç î
(
==î ï
)
==ï ñ
)
==ñ ó
.>> 
Where>> 
(>> 
e>> 
=>>> 
e>> 
.>> 
GetInterfaces>> +
(>>+ ,
)>>, -
.>>- .
Any>>. 1
(>>1 2
x>>2 3
=>>>4 6
x>>7 8
==>>9 ;
typeof>>< B
(>>B C!
IMessageExecutionStep>>C X
)>>X Y
)>>Y Z
)>>Z [
.?? 
AsSelf?? 
(?? 
)?? 
;?? 
builderAA 
.AA 
RegisterTypeAA  
<AA  !
ServiceInventoryAA! 1
>AA1 2
(AA2 3
)AA3 4
.BB 
AsSelfBB 
(BB 
)BB 
.CC 
SingleInstanceCC 
(CC  
)CC  !
.DD 
OnActivatedDD 
(DD 
eDD 
=>DD !
{EE 
eFF 
.FF 
InstanceFF 
.FF 
LoadFF #
(FF# $
_stackFF$ *
.FF* +

AssembliesFF+ 5
.FF5 6
ToArrayFF6 =
(FF= >
)FF> ?
)FF? @
;FF@ A
}GG 
)GG 
;GG 
builderII 
.II 
RegisterII 
(II 
cII 
=>II !
newII" %
RequestII& -
(II- .
)II. /
)II/ 0
.JJ 
AsJJ 
<JJ 
IRequestContextJJ #
>JJ# $
(JJ$ %
)JJ% &
;JJ& '
builderLL 
.LL 
RegisterTypeLL  
<LL  !
InMemoryRequestLogLL! 3
>LL3 4
(LL4 5
)LL5 6
.LL6 7
AsLL7 9
<LL9 :
IRequestLogLL: E
>LLE F
(LLF G
)LLG H
.LLH I
SingleInstanceLLI W
(LLW X
)LLX Y
;LLY Z
builderMM 
.MM 
RegisterTypeMM  
<MM  !
InMemoryResponseLogMM! 4
>MM4 5
(MM5 6
)MM6 7
.MM7 8
AsMM8 :
<MM: ;
IResponseLogMM; G
>MMG H
(MMH I
)MMI J
.MMJ K
SingleInstanceMMK Y
(MMY Z
)MMZ [
;MM[ \
builderNN 
.NN 
RegisterTypeNN  
<NN  !
InMemoryEventStoreNN! 3
>NN3 4
(NN4 5
)NN5 6
.NN6 7
AsNN7 9
<NN9 :
IEventStoreNN: E
>NNE F
(NNF G
)NNG H
.NNH I
SingleInstanceNNI W
(NNW X
)NNX Y
;NNY Z
builderPP 
.PP 
RegisterGenericPP #
(PP# $
typeofPP$ *
(PP* +
MessageValidatorPP+ ;
<PP; <
>PP< =
)PP= >
)PP> ?
;PP? @
thisRR 
.RR !
RegisterAssemblyTypesRR &
(RR& '
builderRR' .
,RR. /
_stackRR0 6
.RR6 7

AssembliesRR7 A
.RRA B
ToArrayRRB I
(RRI J
)RRJ K
)RRK L
;RRL M
_stackTT 
.TT 

AssembliesTT 
.TT 
CollectionChangedTT /
+=TT0 2
thisTT3 7
.TT7 8#
HandleCollectionChangedTT8 O
;TTO P
}UU 	
privateWW 
voidWW #
HandleCollectionChangedWW ,
(WW, -
objectWW- 3
senderWW4 :
,WW: ;,
 NotifyCollectionChangedEventArgsWW< \
eWW] ^
)WW^ _
{XX 	
_stackYY 
.YY 
UseYY 
(YY 
builderYY 
=>YY !
{YY" #
thisYY$ (
.YY( )!
RegisterAssemblyTypesYY) >
(YY> ?
builderYY? F
,YYF G
eYYH I
.YYI J
NewItemsYYJ R
.YYR S
OfTypeYYS Y
<YYY Z
AssemblyYYZ b
>YYb c
(YYc d
)YYd e
.YYe f
ToArrayYYf m
(YYm n
)YYn o
)YYo p
;YYp q
}YYr s
)YYs t
;YYt u
}ZZ 	
private\\ 
void\\ !
RegisterAssemblyTypes\\ *
(\\* +
ContainerBuilder\\+ ;
builder\\< C
,\\C D
Assembly\\E M
[\\M N
]\\N O

assemblies\\P Z
)\\Z [
{]] 	
builder^^ 
.^^ !
RegisterAssemblyTypes^^ )
(^^) *

assemblies^^* 4
)^^4 5
.__ 
Where__ 
(__ 
e__ 
=>__ 
e__ 
.__ 
GetInterfaces__ +
(__+ ,
)__, -
.__- .
Any__. 1
(__1 2
x__2 3
=>__4 6
x__7 8
.__8 9
GetTypeInfo__9 D
(__D E
)__E F
.__F G
IsGenericType__G T
&&__U W
x__X Y
.__Y Z$
GetGenericTypeDefinition__Z r
(__r s
)__s t
==__u w
typeof__x ~
(__~ 
	IValidate	__ à
<
__à â
>
__â ä
)
__ä ã
)
__ã å
)
__å ç
.`` "
AsBaseAndContractTypes`` '
(``' (
)``( )
.aa "
AllPropertiesAutowiredaa '
(aa' (
)aa( )
;aa) *
buildercc 
.cc !
RegisterAssemblyTypescc )
(cc) *

assembliescc* 4
)cc4 5
.dd 
Wheredd 
(dd 
edd 
=>dd 
edd 
.dd 
GetInterfacesdd +
(dd+ ,
)dd, -
.dd- .
Anydd. 1
(dd1 2
xdd2 3
=>dd4 6
xdd7 8
==dd9 ;
typeofdd< B
(ddB C
	IEndPointddC L
)ddL M
)ddM N
)ddN O
.ee "
AsBaseAndContractTypesee '
(ee' (
)ee( )
.ff 
AsSelfff 
(ff 
)ff 
.gg "
AllPropertiesAutowiredgg '
(gg' (
)gg( )
.hh 
OnActivatedhh 
(hh 
ehh 
=>hh !
{hh" #
(hh$ %
(hh% &
	IEndPointhh& /
)hh/ 0
ehh0 1
.hh1 2
Instancehh2 :
)hh: ;
.hh; <
OnStarthh< C
(hhC D
)hhD E
;hhE F
}hhG H
)hhH I
;hhI J
builderjj 
.jj !
RegisterAssemblyTypesjj )
(jj) *

assembliesjj* 4
)jj4 5
.kk 
Wherekk 
(kk 
ekk 
=>kk 
ekk 
.kk 
GetInterfaceskk +
(kk+ ,
)kk, -
.kk- .
Containskk. 6
(kk6 7
typeofkk7 =
(kk= >
IEventPublisherkk> M
)kkM N
)kkN O
)kkO P
.ll 
Asll 
<ll 
IEventPublisherll #
>ll# $
(ll$ %
)ll% &
.mm 
AsSelfmm 
(mm 
)mm 
.nn 
SingleInstancenn 
(nn  
)nn  !
;nn! "
builderpp 
.pp !
RegisterAssemblyTypespp )
(pp) *

assembliespp* 4
)pp4 5
.qq 
Whereqq 
(qq 
eqq 
=>qq 
eqq 
.qq 
GetInterfacesqq +
(qq+ ,
)qq, -
.qq- .
Containsqq. 6
(qq6 7
typeofqq7 =
(qq= >
IRemoteRouterqq> K
)qqK L
)qqL M
)qqM N
.rr 
Asrr 
<rr 
IRemoteRouterrr !
>rr! "
(rr" #
)rr# $
.ss 
AsSelfss 
(ss 
)ss 
.tt 
SingleInstancett 
(tt  
)tt  !
;tt! "
}uu 	
}vv 
}ww ï
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\BodyParameter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
BodyParameter 
:  

IParameter! +
{ 
public 
Schema 
Schema 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
=) *
$str+ 1
;1 2
public 
string 
In 
{ 
get 
; 
set  #
;# $
}% &
=' (
$str) /
;/ 0
public"" 
string"" 
Description"" !
{""" #
get""$ '
;""' (
set"") ,
;"", -
}"". /
public%% 
bool%% 
Required%% 
{%% 
get%% "
;%%" #
set%%$ '
;%%' (
}%%) *
=%%+ ,
true%%- 1
;%%1 2
[(( 	
JsonExtensionData((	 
](( 
public)) 

Dictionary)) 
<)) 
string))  
,))  !
object))" (
>))( )

Extensions))* 4
{))5 6
get))7 :
;)): ;
private))< C
set))D G
;))G H
}))I J
=))K L
new))M P

Dictionary))Q [
<))[ \
string))\ b
,))b c
object))d j
>))j k
())k l
)))l m
;))m n
}** 
}++ é
HC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\ExternalDocs.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{		 
public 

class 
ExternalDocs 
{ 
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Url 
{ 
get 
;  
set! $
;$ %
}& '
} 
}   ï
BC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Header.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{		 
public 

class 
Header 
: 
PartialSchema '
{ 
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ≤
FC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\IParameter.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
OpenApi

! (
{ 
public 

	interface 

IParameter 
{ 
string 
Description 
{ 
get  
;  !
set" %
;% &
}' (

Dictionary   
<   
string   
,   
object   !
>  ! "

Extensions  # -
{  . /
get  0 3
;  3 4
}  5 6
string(( 
In(( 
{(( 
get(( 
;(( 
set(( 
;(( 
}(( 
string00 
Name00 
{00 
get00 
;00 
set00 
;00 
}00  !
bool88 
Required88 
{88 
get88 
;88 
set88  
;88  !
}88" #
}99 
}:: õ
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\NonBodyParameter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
NonBodyParameter !
:" #
PartialSchema$ 1
,1 2

IParameter3 =
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
In 
{ 
get 
; 
set  #
;# $
}% &
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
Required 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
JsonExtensionData	 
] 
public 

Dictionary 
< 
string  
,  !
object" (
>( )

Extensions* 4
{5 6
get7 :
;: ;
private< C
setD G
;G H
}I J
=K L
newM P

DictionaryQ [
<[ \
string\ b
,b c
objectd j
>j k
(k l
)l m
;m n
} 
} êÈ
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\OpenApiDocument.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
OpenApiDocument  
{ 
public!! 
IDictionary!! 
<!! 
string!! !
,!!! "
SecurityScheme!!# 1
>!!1 2
SecurityDefinitions!!3 F
{!!G H
get!!I L
;!!L M
set!!N Q
;!!Q R
}!!S T
=!!U V
new!!W Z
SortedDictionary!![ k
<!!k l
string!!l r
,!!r s
SecurityScheme	!!t Ç
>
!!Ç É
{"" 	
{## 
$str## 
,## 
new## 
SecurityScheme## +
{##, -
Type##. 2
=##3 4
$str##5 =
,##= >
Name##? C
=##C D
$str##D M
,##M N
In##O Q
=##R S
$str##T \
}##] ^
}##_ `
}$$ 	
;$$	 

public,, 
string,, 
BasePath,, 
{,,  
get,,! $
;,,$ %
set,,& )
;,,) *
},,+ ,
public44 
SchemaCollection44 
Definitions44  +
{44, -
get44. 1
;441 2
set443 6
;446 7
}448 9
=44: ;
new44< ?
SchemaCollection44@ P
(44P Q
)44Q R
;44R S
public<< 
ExternalDocs<< 
ExternalDocs<< (
{<<) *
get<<+ .
;<<. /
set<<0 3
;<<3 4
}<<5 6
publicDD 
stringDD 
HostDD 
{DD 
getDD  
;DD  !
setDD" %
;DD% &
}DD' (
publicLL 
ApplicationLL 
InfoLL 
{LL  !
getLL" %
;LL% &
setLL' *
;LL* +
}LL, -
publicTT 
IDictionaryTT 
<TT 
stringTT !
,TT! "
PathItemTT# +
>TT+ ,
PathsTT- 2
{TT3 4
getTT5 8
;TT8 9
setTT: =
;TT= >
}TT? @
=TTA B
newTTC F
SortedDictionaryTTG W
<TTW X
stringTTX ^
,TT^ _
PathItemTT` h
>TTh i
(TTi j
newTTj m
PathComparerTTn z
(TTz {
)TT{ |
)TT| }
;TT} ~
public\\ 
string\\ 
[\\ 
]\\ 
Schemes\\ 
{\\  !
get\\" %
;\\% &
set\\' *
;\\* +
}\\, -
=\\. /
{\\0 1
$str\\2 8
,\\8 9
$str\\: A
}\\B C
;\\C D
publicdd 
stringdd 
Swaggerdd 
{dd 
getdd  #
;dd# $
setdd% (
;dd( )
}dd* +
=dd, -
$strdd. 3
;dd3 4
publicll 
Listll 
<ll 
Tagll 
>ll 
Tagsll 
{ll 
getll  #
;ll# $
setll% (
;ll( )
}ll* +
=ll, -
newll. 1
Listll2 6
<ll6 7
Tagll7 :
>ll: ;
{ll< =
newll> A
TagllB E
{llF G
NamellH L
=llM N
$strllO W
,llW X
DescriptionllY d
=lle f
$str	llg Ç
}
llÉ Ñ
}
llÖ Ü
;
llÜ á
publicss 
voidss 
Loadss 
(ss 
ServiceInventoryss )
servicesss* 2
,ss2 3
boolss4 8

includeAllss9 C
=ssD E
falsessF K
)ssK L
{tt 	
thisuu 
.uu 
Infouu 
=uu 
servicesuu  
.uu  !
Applicationuu! ,
;uu, -
varvv 
	endPointsvv 
=vv 
servicesvv $
.vv$ %
	EndPointsvv% .
.vv. /
Wherevv/ 4
(vv4 5
evv5 6
=>vv7 9

includeAllvv: D
||vvE G
evvH I
.vvI J
PublicvvJ P
&&vvQ S
!vvT U
evvU V
.vvV W
IsVersionedvvW b
)vvb c
.vvc d
ToListvvd j
(vvj k
)vvk l
;vvl m
foreachww 
(ww 
varww 
endPointww !
inww" $
	endPointsww% .
)ww. /
{xx 
ifyy 
(yy 
endPointyy 
.yy 
RequestTypeyy (
!=yy) +
nullyy, 0
)yy0 1
{zz 
this{{ 
.{{ 
Definitions{{ $
.{{$ %
GetOrAdd{{% -
({{- .
endPoint{{. 6
.{{6 7
RequestType{{7 B
){{B C
;{{C D
}|| 
if}} 
(}} 
endPoint}} 
.}} 
ResponseType}} )
!=}}* ,
null}}- 1
)}}1 2
{~~ 
this 
. 
Definitions $
.$ %
GetOrAdd% -
(- .
endPoint. 6
.6 7
ResponseType7 C
)C D
;D E
}
ÄÄ 
if
ÇÇ 
(
ÇÇ 
endPoint
ÇÇ 
.
ÇÇ 
Path
ÇÇ !
!=
ÇÇ" $
null
ÇÇ% )
)
ÇÇ) *
{
ÉÉ 
this
ÑÑ 
.
ÑÑ 
Paths
ÑÑ 
.
ÑÑ 
Add
ÑÑ "
(
ÑÑ" #
$str
ÑÑ# &
+
ÑÑ' (
endPoint
ÑÑ) 1
.
ÑÑ1 2
Path
ÑÑ2 6
,
ÑÑ6 7
new
ÑÑ8 ;
PathItem
ÑÑ< D
{
ÖÖ 
Post
ÜÜ 
=
ÜÜ 
this
ÜÜ #
.
ÜÜ# $
GetPostOperation
ÜÜ$ 4
(
ÜÜ4 5
endPoint
ÜÜ5 =
)
ÜÜ= >
,
ÜÜ> ?
Get
áá 
=
áá 
this
áá "
.
áá" #
GetGetOperation
áá# 2
(
áá2 3
endPoint
áá3 ;
)
áá; <
}
àà 
)
àà 
;
àà 
}
ââ 
}
ää 
}
ãã 	
private
çç 
	Operation
çç 
GetGetOperation
çç )
(
çç) *
EndPointMetaData
çç* :
endPoint
çç; C
)
ççC D
{
éé 	
if
èè 
(
èè 
endPoint
èè 
.
èè 
Method
èè 
==
èè  "
$str
èè# (
)
èè( )
{
êê 
var
ëë 

parameters
ëë 
=
ëë  
new
ëë! $
List
ëë% )
<
ëë) *

IParameter
ëë* 4
>
ëë4 5
(
ëë5 6
)
ëë6 7
;
ëë7 8
foreach
íí 
(
íí 
var
íí 
property
íí %
in
íí& (
endPoint
íí) 1
.
íí1 2
RequestType
íí2 =
.
íí= >
GetProperties
íí> K
(
ííK L
)
ííL M
)
ííM N
{
ìì 
var
îî 
schema
îî 
=
îî  
this
îî! %
.
îî% &
Definitions
îî& 1
.
îî1 2#
CreatePrimitiveSchema
îî2 G
(
îîG H
property
îîH P
.
îîP Q
PropertyType
îîQ ]
)
îî] ^
;
îî^ _
var
ïï 
required
ïï  
=
ïï! "
property
ïï# +
.
ïï+ ,!
GetCustomAttributes
ïï, ?
<
ïï? @!
ValidationAttribute
ïï@ S
>
ïïS T
(
ïïT U
true
ïïU Y
)
ïïY Z
.
ïïZ [
Any
ïï[ ^
(
ïï^ _
)
ïï_ `
;
ïï` a

parameters
ññ 
.
ññ 
Add
ññ "
(
ññ" #
new
ññ# &
NonBodyParameter
ññ' 7
{
óó 
Name
òò 
=
òò 
property
òò '
.
òò' (
Name
òò( ,
,
òò, -
Required
ôô  
=
ôô! "
required
ôô# +
,
ôô+ ,
In
öö 
=
öö 
$str
öö $
,
öö$ %
Description
õõ #
=
õõ$ %
property
õõ& .
.
õõ. /
GetComments
õõ/ :
(
õõ: ;
)
õõ; <
?
õõ< =
.
õõ= >
Value
õõ> C
,
õõC D
Type
úú 
=
úú 
schema
úú %
.
úú% &
Type
úú& *
,
úú* +
Format
ùù 
=
ùù  
schema
ùù! '
.
ùù' (
Format
ùù( .
}
ûû 
)
ûû 
;
ûû 
}
üü 
var
°° 
	operation
°° 
=
°° 
new
°°  #
	Operation
°°$ -
{
¢¢ 
Tags
££ 
=
££ 
this
££ 
.
££  
GetTags
££  '
(
££' (
endPoint
££( 0
)
££0 1
.
££1 2
ToList
££2 8
(
££8 9
)
££9 :
,
££: ;
Summary
§§ 
=
§§ 
endPoint
§§ &
.
§§& '
Name
§§' +
,
§§+ ,
Description
•• 
=
••  !
endPoint
••" *
.
••* +
Summary
••+ 2
,
••2 3
Consumes
¶¶ 
=
¶¶ 
new
¶¶ "
List
¶¶# '
<
¶¶' (
string
¶¶( .
>
¶¶. /
{
¶¶0 1
$str
¶¶2 D
}
¶¶E F
,
¶¶F G
Produces
ßß 
=
ßß 
new
ßß "
List
ßß# '
<
ßß' (
string
ßß( .
>
ßß. /
{
ßß0 1
$str
ßß2 D
}
ßßE F
,
ßßF G
OperationId
®® 
=
®®  !
NewId
®®" '
.
®®' (
NextId
®®( .
(
®®. /
)
®®/ 0
.
®®0 1
Replace
®®1 8
(
®®8 9
$str
®®9 <
,
®®< =
$str
®®> @
)
®®@ A
,
®®A B

Parameters
©© 
=
©©  

parameters
©©! +
,
©©+ ,
	Responses
™™ 
=
™™ 
this
™™  $
.
™™$ %
GetResponses
™™% 1
(
™™1 2
endPoint
™™2 :
)
™™: ;
}
´´ 
;
´´ 
if
≠≠ 
(
≠≠ 
	operation
≠≠ 
.
≠≠ 
	Responses
≠≠ '
.
≠≠' (
ContainsKey
≠≠( 3
(
≠≠3 4
$str
≠≠4 9
)
≠≠9 :
)
≠≠: ;
{
ÆÆ 
	operation
ØØ 
.
ØØ 
IncludeSecurity
ØØ -
(
ØØ- .
$str
ØØ. 7
)
ØØ7 8
;
ØØ8 9
}
∞∞ 
if
≤≤ 
(
≤≤ 
endPoint
≤≤ 
.
≤≤ 
IsVersioned
≤≤ (
)
≤≤( )
{
≥≥ 
	operation
¥¥ 
.
¥¥ 

Deprecated
¥¥ (
=
¥¥) *
true
¥¥+ /
;
¥¥/ 0
}
µµ 
return
∑∑ 
	operation
∑∑  
;
∑∑  !
}
∏∏ 
return
ππ 
null
ππ 
;
ππ 
}
∫∫ 	
private
ºº 
	Operation
ºº 
GetPostOperation
ºº *
(
ºº* +
EndPointMetaData
ºº+ ;
endPoint
ºº< D
)
ººD E
{
ΩΩ 	
if
ææ 
(
ææ 
endPoint
ææ 
.
ææ 
Method
ææ 
==
ææ  "
$str
ææ# )
)
ææ) *
{
øø 
var
¿¿ 
	operation
¿¿ 
=
¿¿ 
new
¿¿  #
	Operation
¿¿$ -
{
¡¡ 
Tags
¬¬ 
=
¬¬ 
this
¬¬ 
.
¬¬  
GetTags
¬¬  '
(
¬¬' (
endPoint
¬¬( 0
)
¬¬0 1
.
¬¬1 2
ToList
¬¬2 8
(
¬¬8 9
)
¬¬9 :
,
¬¬: ;
Summary
√√ 
=
√√ 
endPoint
√√ &
.
√√& '
Name
√√' +
,
√√+ ,
Description
ƒƒ 
=
ƒƒ  !
endPoint
ƒƒ" *
.
ƒƒ* +
Summary
ƒƒ+ 2
,
ƒƒ2 3
Consumes
≈≈ 
=
≈≈ 
new
≈≈ "
List
≈≈# '
<
≈≈' (
string
≈≈( .
>
≈≈. /
{
≈≈0 1
$str
≈≈2 D
}
≈≈E F
,
≈≈F G
Produces
∆∆ 
=
∆∆ 
new
∆∆ "
List
∆∆# '
<
∆∆' (
string
∆∆( .
>
∆∆. /
{
∆∆0 1
$str
∆∆2 D
}
∆∆E F
,
∆∆F G
OperationId
«« 
=
««  !
NewId
««" '
.
««' (
NextId
««( .
(
««. /
)
««/ 0
.
««0 1
Replace
««1 8
(
««8 9
$str
««9 <
,
««< =
$str
««> @
)
««@ A
,
««A B

Parameters
»» 
=
»»  
this
»»! %
.
»»% &
GetPostParameters
»»& 7
(
»»7 8
endPoint
»»8 @
)
»»@ A
.
»»A B
ToList
»»B H
(
»»H I
)
»»I J
,
»»J K
	Responses
…… 
=
…… 
this
……  $
.
……$ %
GetResponses
……% 1
(
……1 2
endPoint
……2 :
)
……: ;
}
   
;
   
if
ÃÃ 
(
ÃÃ 
	operation
ÃÃ 
.
ÃÃ 
	Responses
ÃÃ '
.
ÃÃ' (
ContainsKey
ÃÃ( 3
(
ÃÃ3 4
$str
ÃÃ4 9
)
ÃÃ9 :
)
ÃÃ: ;
{
ÕÕ 
	operation
ŒŒ 
.
ŒŒ 
IncludeSecurity
ŒŒ -
(
ŒŒ- .
$str
ŒŒ. 7
)
ŒŒ7 8
;
ŒŒ8 9
}
œœ 
if
—— 
(
—— 
endPoint
—— 
.
—— 
IsVersioned
—— (
)
——( )
{
““ 
	operation
”” 
.
”” 

Deprecated
”” (
=
””) *
true
””+ /
;
””/ 0
}
‘‘ 
return
÷÷ 
	operation
÷÷  
;
÷÷  !
}
◊◊ 
return
ÿÿ 
null
ÿÿ 
;
ÿÿ 
}
ŸŸ 	
private
€€ 
IEnumerable
€€ 
<
€€ 

IParameter
€€ &
>
€€& '
GetPostParameters
€€( 9
(
€€9 :
EndPointMetaData
€€: J
endPoint
€€K S
)
€€S T
{
‹‹ 	
if
›› 
(
›› 
endPoint
›› 
.
›› 
RequestType
›› $
!=
››% '
null
››( ,
&&
››- /
endPoint
››0 8
.
››8 9
RequestType
››9 D
!=
››E G
typeof
››H N
(
››N O
object
››O U
)
››U V
)
››V W
{
ﬁﬁ 
yield
ﬂﬂ 
return
ﬂﬂ 
new
ﬂﬂ  
BodyParameter
ﬂﬂ! .
{
‡‡ 
Schema
·· 
=
·· 
this
·· !
.
··! "
Definitions
··" -
.
··- . 
GetReferenceSchema
··. @
(
··@ A
endPoint
··A I
.
··I J
RequestType
··J U
,
··U V
endPoint
··W _
.
··_ `
RequestType
··` k
.
··k l
GetComments
··l w
(
··w x
)
··x y
?
··y z
.
··z {
Summary··{ Ç
)··Ç É
}
‚‚ 
;
‚‚ 
}
„„ 
}
‰‰ 	
private
ÊÊ 

Dictionary
ÊÊ 
<
ÊÊ 
string
ÊÊ !
,
ÊÊ! "
Response
ÊÊ# +
>
ÊÊ+ ,
GetResponses
ÊÊ- 9
(
ÊÊ9 :
EndPointMetaData
ÊÊ: J
endPoint
ÊÊK S
)
ÊÊS T
{
ÁÁ 	
var
ËË 
	responses
ËË 
=
ËË 
new
ËË 

Dictionary
ËË  *
<
ËË* +
string
ËË+ 1
,
ËË1 2
Response
ËË3 ;
>
ËË; <
(
ËË< =
)
ËË= >
;
ËË> ?
if
ÈÈ 
(
ÈÈ 
endPoint
ÈÈ 
.
ÈÈ 
ResponseType
ÈÈ %
==
ÈÈ& (
null
ÈÈ) -
)
ÈÈ- .
{
ÍÍ 
	responses
ÎÎ 
.
ÎÎ 
Add
ÎÎ 
(
ÎÎ 
$str
ÎÎ #
,
ÎÎ# $
new
ÎÎ% (
Response
ÎÎ) 1
{
ÏÏ 
Description
ÌÌ 
=
ÌÌ  !
$str
ÌÌ" N
}
ÓÓ 
)
ÓÓ 
;
ÓÓ 
}
ÔÔ 
else
 
{
ÒÒ 
var
ÚÚ 
responseType
ÚÚ  
=
ÚÚ! "
endPoint
ÚÚ# +
.
ÚÚ+ ,
ResponseType
ÚÚ, 8
;
ÚÚ8 9
	responses
ÛÛ 
.
ÛÛ 
Add
ÛÛ 
(
ÛÛ 
$str
ÛÛ #
,
ÛÛ# $
new
ÛÛ% (
Response
ÛÛ) 1
{
ÙÙ 
Description
ıı 
=
ıı  !
responseType
ıı" .
.
ıı. /
GetComments
ıı/ :
(
ıı: ;
)
ıı; <
?
ıı< =
.
ıı= >
Summary
ıı> E
??
ııF H
$str
ııI K
,
ııK L
Schema
ˆˆ 
=
ˆˆ 
this
ˆˆ !
.
ˆˆ! "
Definitions
ˆˆ" -
.
ˆˆ- . 
GetReferenceSchema
ˆˆ. @
(
ˆˆ@ A
responseType
ˆˆA M
,
ˆˆM N
endPoint
ˆˆO W
.
ˆˆW X
ResponseType
ˆˆX d
.
ˆˆd e
GetComments
ˆˆe p
(
ˆˆp q
)
ˆˆq r
?
ˆˆr s
.
ˆˆs t
Summary
ˆˆt {
)
ˆˆ{ |
}
˜˜ 
)
˜˜ 
;
˜˜ 
}
¯¯ 
var
˘˘ 
builder
˘˘ 
=
˘˘ 
new
˘˘ 
StringBuilder
˘˘ +
(
˘˘+ ,
)
˘˘, -
;
˘˘- .
foreach
˙˙ 
(
˙˙ 
var
˙˙ 
property
˙˙ !
in
˙˙" $
endPoint
˙˙% -
.
˙˙- .
RequestType
˙˙. 9
.
˙˙9 :
GetProperties
˙˙: G
(
˙˙G H
)
˙˙H I
)
˙˙I J
{
˚˚ 
foreach
¸¸ 
(
¸¸ 
var
¸¸ 
	attribute
¸¸ &
in
¸¸' )
property
¸¸* 2
.
¸¸2 3!
GetCustomAttributes
¸¸3 F
<
¸¸F G!
ValidationAttribute
¸¸G Z
>
¸¸Z [
(
¸¸[ \
true
¸¸\ `
)
¸¸` a
)
¸¸a b
{
˝˝ 
builder
˛˛ 
.
˛˛ 

AppendLine
˛˛ &
(
˛˛& '
$str
˛˛' ,
+
˛˛- .
	attribute
˛˛/ 8
.
˛˛8 9 
GetValidationError
˛˛9 K
(
˛˛K L
property
˛˛L T
)
˛˛T U
.
˛˛U V
Message
˛˛V ]
+
˛˛^ _
$str
˛˛` f
)
˛˛f g
;
˛˛g h
}
ˇˇ 
}
ÄÄ 
foreach
ÅÅ 
(
ÅÅ 
var
ÅÅ 
source
ÅÅ 
in
ÅÅ  "
endPoint
ÅÅ# +
.
ÅÅ+ ,
Rules
ÅÅ, 1
.
ÅÅ1 2
Where
ÅÅ2 7
(
ÅÅ7 8
e
ÅÅ8 9
=>
ÅÅ: <
e
ÅÅ= >
.
ÅÅ> ?
RuleType
ÅÅ? G
==
ÅÅH J
ValidationType
ÅÅK Y
.
ÅÅY Z
Input
ÅÅZ _
)
ÅÅ_ `
)
ÅÅ` a
{
ÇÇ 
builder
ÉÉ 
.
ÉÉ 

AppendLine
ÉÉ "
(
ÉÉ" #
source
ÉÉ# )
.
ÉÉ) *
Name
ÉÉ* .
.
ÉÉ. /
ToTitle
ÉÉ/ 6
(
ÉÉ6 7
)
ÉÉ7 8
+
ÉÉ9 :
$str
ÉÉ; @
)
ÉÉ@ A
;
ÉÉA B
}
ÑÑ 
if
ÖÖ 
(
ÖÖ 
builder
ÖÖ 
.
ÖÖ 
Length
ÖÖ 
>
ÖÖ  
$num
ÖÖ! "
)
ÖÖ" #
{
ÜÜ 
	responses
áá 
.
áá 
Add
áá 
(
áá 
$str
áá #
,
áá# $
new
áá% (
Response
áá) 1
{
àà 
Schema
ââ 
=
ââ 
this
ââ !
.
ââ! "
Definitions
ââ" -
.
ââ- . 
GetReferenceSchema
ââ. @
(
ââ@ A
typeof
ââA G
(
ââG H
ValidationError
ââH W
[
ââW X
]
ââX Y
)
ââY Z
,
ââZ [
null
ââ\ `
)
ââ` a
,
ââa b
Description
ää 
=
ää  !
builder
ää" )
.
ää) *
ToString
ää* 2
(
ää2 3
)
ää3 4
}
ãã 
)
ãã 
;
ãã 
}
åå 
builder
çç 
.
çç 
Clear
çç 
(
çç 
)
çç 
;
çç 
if
èè 
(
èè 
endPoint
èè 
.
èè 
Secure
èè 
)
èè  
{
êê 
	responses
ëë 
.
ëë 
Add
ëë 
(
ëë 
$str
ëë #
,
ëë# $
new
ëë% (
Response
ëë) 1
{
íí 
Schema
ìì 
=
ìì 
this
ìì !
.
ìì! "
Definitions
ìì" -
.
ìì- . 
GetReferenceSchema
ìì. @
(
ìì@ A
typeof
ììA G
(
ììG H
ValidationError
ììH W
[
ììW X
]
ììX Y
)
ììY Z
,
ììZ [
null
ìì\ `
)
ìì` a
,
ììa b
Description
îî 
=
îî  !
$str
îî" I
}
ïï 
)
ïï 
;
ïï 
}
ññ 
foreach
òò 
(
òò 
var
òò 
source
òò 
in
òò  "
endPoint
òò# +
.
òò+ ,
Rules
òò, 1
.
òò1 2
Where
òò2 7
(
òò7 8
e
òò8 9
=>
òò: <
e
òò= >
.
òò> ?
RuleType
òò? G
==
òòH J
ValidationType
òòK Y
.
òòY Z
Business
òòZ b
)
òòb c
)
òòc d
{
ôô 
builder
öö 
.
öö 

AppendLine
öö "
(
öö" #
$str
öö# (
+
öö) *
source
öö+ 1
.
öö1 2
Name
öö2 6
.
öö6 7
ToTitle
öö7 >
(
öö> ?
)
öö? @
+
ööA B
$str
ööC J
)
ööJ K
;
ööK L
}
õõ 
if
úú 
(
úú 
builder
úú 
.
úú 
Length
úú 
>
úú  
$num
úú! "
)
úú" #
{
ùù 
	responses
ûû 
.
ûû 
Add
ûû 
(
ûû 
$str
ûû #
,
ûû# $
new
ûû% (
Response
ûû) 1
{
üü 
Schema
†† 
=
†† 
this
†† !
.
††! "
Definitions
††" -
.
††- . 
GetReferenceSchema
††. @
(
††@ A
typeof
††A G
(
††G H
ValidationError
††H W
[
††W X
]
††X Y
)
††Y Z
,
††Z [
null
††\ `
)
††` a
,
††a b
Description
°° 
=
°°  !
builder
°°" )
.
°°) *
ToString
°°* 2
(
°°2 3
)
°°3 4
}
¢¢ 
)
¢¢ 
;
¢¢ 
}
££ 
builder
§§ 
.
§§ 
Clear
§§ 
(
§§ 
)
§§ 
;
§§ 
foreach
•• 
(
•• 
var
•• 
source
•• 
in
••  "
endPoint
••# +
.
••+ ,
Rules
••, 1
.
••1 2
Where
••2 7
(
••7 8
e
••8 9
=>
••: <
e
••= >
.
••> ?
RuleType
••? G
==
••H J
ValidationType
••K Y
.
••Y Z
Security
••Z b
)
••b c
)
••c d
{
¶¶ 
builder
ßß 
.
ßß 

AppendLine
ßß "
(
ßß" #
$str
ßß# (
+
ßß) *
source
ßß+ 1
.
ßß1 2
Name
ßß2 6
.
ßß6 7
ToTitle
ßß7 >
(
ßß> ?
)
ßß? @
+
ßßA B
$str
ßßC J
)
ßßJ K
;
ßßK L
}
®® 
if
©© 
(
©© 
builder
©© 
.
©© 
Length
©© 
>
©©  
$num
©©! "
)
©©" #
{
™™ 
if
´´ 
(
´´ 
!
´´ 
	responses
´´ 
.
´´ 
ContainsKey
´´ *
(
´´* +
$str
´´+ 0
)
´´0 1
)
´´1 2
{
¨¨ 
	responses
≠≠ 
.
≠≠ 
Add
≠≠ !
(
≠≠! "
$str
≠≠" '
,
≠≠' (
new
≠≠) ,
Response
≠≠- 5
{
ÆÆ 
Schema
ØØ 
=
ØØ  
this
ØØ! %
.
ØØ% &
Definitions
ØØ& 1
.
ØØ1 2 
GetReferenceSchema
ØØ2 D
(
ØØD E
typeof
ØØE K
(
ØØK L
ValidationError
ØØL [
[
ØØ[ \
]
ØØ\ ]
)
ØØ] ^
,
ØØ^ _
null
ØØ` d
)
ØØd e
,
ØØe f
Description
∞∞ #
=
∞∞$ %
$str
∞∞& M
}
±± 
)
±± 
;
±± 
}
≤≤ 
	responses
≥≥ 
.
≥≥ 
Add
≥≥ 
(
≥≥ 
$str
≥≥ #
,
≥≥# $
new
≥≥% (
Response
≥≥) 1
{
¥¥ 
Schema
µµ 
=
µµ 
this
µµ !
.
µµ! "
Definitions
µµ" -
.
µµ- . 
GetReferenceSchema
µµ. @
(
µµ@ A
typeof
µµA G
(
µµG H
ValidationError
µµH W
[
µµW X
]
µµX Y
)
µµY Z
,
µµZ [
null
µµ\ `
)
µµ` a
,
µµa b
Description
∂∂ 
=
∂∂  !
builder
∂∂" )
.
∂∂) *
ToString
∂∂* 2
(
∂∂2 3
)
∂∂3 4
}
∑∑ 
)
∑∑ 
;
∑∑ 
}
∏∏ 
return
∫∫ 
	responses
∫∫ 
;
∫∫ 
}
ªª 	
private
ΩΩ 
IEnumerable
ΩΩ 
<
ΩΩ 
string
ΩΩ "
>
ΩΩ" #
GetTags
ΩΩ$ +
(
ΩΩ+ ,
EndPointMetaData
ΩΩ, <
endPoint
ΩΩ= E
)
ΩΩE F
{
ææ 	
if
øø 
(
øø 
endPoint
øø 
.
øø 
Path
øø 
==
øø  
null
øø! %
)
øø% &
{
¿¿ 
yield
¡¡ 
break
¡¡ 
;
¡¡ 
}
¬¬ 
if
ƒƒ 
(
ƒƒ 
endPoint
ƒƒ 
.
ƒƒ 
Path
ƒƒ 
.
ƒƒ 

StartsWith
ƒƒ (
(
ƒƒ( )
$str
ƒƒ) ,
)
ƒƒ, -
||
ƒƒ. 0
endPoint
ƒƒ1 9
.
ƒƒ9 :
IsVersioned
ƒƒ: E
&&
ƒƒF H
endPoint
ƒƒI Q
.
ƒƒQ R
Path
ƒƒR V
.
ƒƒV W
Split
ƒƒW \
(
ƒƒ\ ]
$char
ƒƒ] `
)
ƒƒ` a
.
ƒƒa b
	ElementAt
ƒƒb k
(
ƒƒk l
$num
ƒƒl m
)
ƒƒm n
?
ƒƒn o
.
ƒƒo p

StartsWith
ƒƒp z
(
ƒƒz {
$str
ƒƒ{ ~
)
ƒƒ~ 
==ƒƒÄ Ç
trueƒƒÉ á
)ƒƒá à
{
≈≈ 
yield
∆∆ 
return
∆∆ 
$str
∆∆ %
;
∆∆% &
yield
«« 
break
«« 
;
«« 
}
»» 
if
   
(
   
endPoint
   
.
   
Tags
   
!=
    
null
  ! %
)
  % &
{
ÀÀ 
foreach
ÃÃ 
(
ÃÃ 
var
ÃÃ 
tag
ÃÃ  
in
ÃÃ! #
endPoint
ÃÃ$ ,
.
ÃÃ, -
Tags
ÃÃ- 1
)
ÃÃ1 2
{
ÕÕ 
yield
ŒŒ 
return
ŒŒ  
tag
ŒŒ! $
;
ŒŒ$ %
}
œœ 
yield
–– 
break
–– 
;
–– 
}
—— 
var
”” 
segments
”” 
=
”” 
endPoint
”” #
.
””# $
Path
””$ (
.
””( )
Split
””) .
(
””. /
$char
””/ 2
)
””2 3
;
””3 4
if
‘‘ 
(
‘‘ 
segments
‘‘ 
.
‘‘ 
Length
‘‘ 
>=
‘‘  "
$num
‘‘# $
)
‘‘$ %
{
’’ 
if
÷÷ 
(
÷÷ 
endPoint
÷÷ 
.
÷÷ 
IsVersioned
÷÷ (
)
÷÷( )
{
◊◊ 
yield
ÿÿ 
return
ÿÿ  
segments
ÿÿ! )
[
ÿÿ) *
$num
ÿÿ* +
]
ÿÿ+ ,
.
ÿÿ, -
Replace
ÿÿ- 4
(
ÿÿ4 5
$str
ÿÿ5 8
,
ÿÿ8 9
$str
ÿÿ: =
)
ÿÿ= >
.
ÿÿ> ?
ToTitle
ÿÿ? F
(
ÿÿF G
)
ÿÿG H
;
ÿÿH I
}
ŸŸ 
else
⁄⁄ 
{
€€ 
yield
‹‹ 
return
‹‹  
segments
‹‹! )
[
‹‹) *
$num
‹‹* +
]
‹‹+ ,
.
‹‹, -
Replace
‹‹- 4
(
‹‹4 5
$str
‹‹5 8
,
‹‹8 9
$str
‹‹: =
)
‹‹= >
.
‹‹> ?
ToTitle
‹‹? F
(
‹‹F G
)
‹‹G H
;
‹‹H I
}
›› 
}
ﬁﬁ 
}
ﬂﬂ 	
private
‚‚ 
class
‚‚ 
PathComparer
‚‚ "
:
‚‚# $
	IComparer
‚‚% .
<
‚‚. /
string
‚‚/ 5
>
‚‚5 6
{
„„ 	
public
‰‰ 
int
‰‰ 
Compare
‰‰ 
(
‰‰ 
string
‰‰ %
x
‰‰& '
,
‰‰' (
string
‰‰) /
y
‰‰0 1
)
‰‰1 2
{
ÂÂ 
var
ÊÊ 
left
ÊÊ 
=
ÊÊ 
new
ÊÊ 
EndPointPath
ÊÊ +
(
ÊÊ+ ,
x
ÊÊ, -
)
ÊÊ- .
;
ÊÊ. /
var
ÁÁ 
right
ÁÁ 
=
ÁÁ 
new
ÁÁ 
EndPointPath
ÁÁ  ,
(
ÁÁ, -
y
ÁÁ- .
)
ÁÁ. /
;
ÁÁ/ 0
return
ÈÈ 
left
ÈÈ 
.
ÈÈ 
	CompareTo
ÈÈ %
(
ÈÈ% &
right
ÈÈ& +
)
ÈÈ+ ,
;
ÈÈ, -
}
ÍÍ 
}
ÎÎ 	
}
ÏÏ 
}ÌÌ ˛%
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Operation.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
	Operation 
{ 
public 
IList 
< 
string 
> 
Consumes %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public!! 
bool!! 
?!! 

Deprecated!! 
{!!  !
get!!" %
;!!% &
set!!' *
;!!* +
}!!, -
public)) 
string)) 
Description)) !
{))" #
get))$ '
;))' (
set))) ,
;)), -
})). /
[11 	
JsonExtensionData11	 
]11 
public22 

Dictionary22 
<22 
string22  
,22  !
object22" (
>22( )

Extensions22* 4
{225 6
get227 :
;22: ;
private22< C
set22D G
;22G H
}22I J
=22K L
new22M P

Dictionary22Q [
<22[ \
string22\ b
,22b c
object22d j
>22j k
(22k l
)22l m
;22m n
public:: 
ExternalDocs:: 
ExternalDocs:: (
{::) *
get::+ .
;::. /
set::0 3
;::3 4
}::5 6
publicBB 
stringBB 
OperationIdBB !
{BB" #
getBB$ '
;BB' (
setBB) ,
;BB, -
}BB. /
publicJJ 
IListJJ 
<JJ 

IParameterJJ 
>JJ  

ParametersJJ! +
{JJ, -
getJJ. 1
;JJ1 2
setJJ3 6
;JJ6 7
}JJ8 9
publicRR 
IListRR 
<RR 
stringRR 
>RR 
ProducesRR %
{RR& '
getRR( +
;RR+ ,
setRR- 0
;RR0 1
}RR2 3
publicZZ 
IDictionaryZZ 
<ZZ 
stringZZ !
,ZZ! "
ResponseZZ# +
>ZZ+ ,
	ResponsesZZ- 6
{ZZ7 8
getZZ9 <
;ZZ< =
setZZ> A
;ZZA B
}ZZC D
publicbb 
IListbb 
<bb 
stringbb 
>bb 
Schemesbb $
{bb% &
getbb' *
;bb* +
setbb, /
;bb/ 0
}bb1 2
publicjj 
Listjj 
<jj 

Dictionaryjj 
<jj 
stringjj %
,jj% &
Listjj' +
<jj+ ,
stringjj, 2
>jj2 3
>jj3 4
>jj4 5
Securityjj6 >
{jj? @
getjjA D
;jjD E
setjjF I
;jjI J
}jjK L
publicrr 
stringrr 
Summaryrr 
{rr 
getrr  #
;rr# $
setrr% (
;rr( )
}rr* +
publiczz 
IListzz 
<zz 
stringzz 
>zz 
Tagszz !
{zz" #
getzz$ '
;zz' (
setzz) ,
;zz, -
}zz. /
public
ÅÅ 
void
ÅÅ 
IncludeSecurity
ÅÅ #
(
ÅÅ# $
string
ÅÅ$ *

definition
ÅÅ+ 5
,
ÅÅ5 6
params
ÅÅ7 =
string
ÅÅ> D
[
ÅÅD E
]
ÅÅE F
scopes
ÅÅG M
)
ÅÅM N
{
ÇÇ 	
if
ÉÉ 
(
ÉÉ 
this
ÉÉ 
.
ÉÉ 
Security
ÉÉ 
==
ÉÉ  
null
ÉÉ! %
)
ÉÉ% &
{
ÑÑ 
this
ÖÖ 
.
ÖÖ 
Security
ÖÖ 
=
ÖÖ 
new
ÖÖ  #
List
ÖÖ$ (
<
ÖÖ( )

Dictionary
ÖÖ) 3
<
ÖÖ3 4
string
ÖÖ4 :
,
ÖÖ: ;
List
ÖÖ< @
<
ÖÖ@ A
string
ÖÖA G
>
ÖÖG H
>
ÖÖH I
>
ÖÖI J
(
ÖÖJ K
)
ÖÖK L
;
ÖÖL M
}
ÜÜ 
var
áá 
requirement
áá 
=
áá 
new
áá !

Dictionary
áá" ,
<
áá, -
string
áá- 3
,
áá3 4
List
áá5 9
<
áá9 :
string
áá: @
>
áá@ A
>
ááA B
{
àà 
{
ââ 

definition
ââ 
,
ââ 
new
ââ !
List
ââ" &
<
ââ& '
string
ââ' -
>
ââ- .
(
ââ. /
scopes
ââ/ 5
)
ââ5 6
}
ââ7 8
}
ää 
;
ää 
this
ãã 
.
ãã 
Security
ãã 
.
ãã 
Add
ãã 
(
ãã 
requirement
ãã )
)
ãã) *
;
ãã* +
}
åå 	
}
çç 
}éé ◊
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\PartialSchema.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
OpenApi

! (
{ 
public 

class 
PartialSchema 
{ 
public 
string 
CollectionFormat &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public$$ 
object$$ 
Default$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
public,, 
List,, 
<,, 
object,, 
>,, 
Enum,,  
{,,! "
get,,# &
;,,& '
set,,( +
;,,+ ,
},,- .
=,,/ 0
new,,1 4
List,,5 9
<,,9 :
object,,: @
>,,@ A
(,,A B
),,B C
;,,C D
public44 
bool44 
?44 
ExclusiveMaximum44 %
{44& '
get44( +
;44+ ,
set44- 0
;440 1
}442 3
public<< 
bool<< 
?<< 
ExclusiveMinimum<< %
{<<& '
get<<( +
;<<+ ,
set<<- 0
;<<0 1
}<<2 3
publicDD 
stringDD 
FormatDD 
{DD 
getDD "
;DD" #
setDD$ '
;DD' (
}DD) *
publicLL 
PartialSchemaLL 
ItemsLL "
{LL# $
getLL% (
;LL( )
setLL* -
;LL- .
}LL/ 0
publicTT 
intTT 
?TT 
MaximumTT 
{TT 
getTT !
;TT! "
setTT# &
;TT& '
}TT( )
public\\ 
int\\ 
?\\ 
MaxItems\\ 
{\\ 
get\\ "
;\\" #
set\\$ '
;\\' (
}\\) *
publicdd 
intdd 
?dd 
	MaxLengthdd 
{dd 
getdd  #
;dd# $
setdd% (
;dd( )
}dd* +
publicll 
intll 
?ll 
Minimumll 
{ll 
getll !
;ll! "
setll# &
;ll& '
}ll( )
publictt 
inttt 
?tt 
MinItemstt 
{tt 
gettt "
;tt" #
settt$ '
;tt' (
}tt) *
public|| 
int|| 
?|| 
	MinLength|| 
{|| 
get||  #
;||# $
set||% (
;||( )
}||* +
public
ÑÑ 
int
ÑÑ 
?
ÑÑ 

MultipleOf
ÑÑ 
{
ÑÑ  
get
ÑÑ! $
;
ÑÑ$ %
set
ÑÑ& )
;
ÑÑ) *
}
ÑÑ+ ,
public
åå 
string
åå 
Pattern
åå 
{
åå 
get
åå  #
;
åå# $
set
åå% (
;
åå( )
}
åå* +
public
îî 
string
îî 
Type
îî 
{
îî 
get
îî  
;
îî  !
set
îî" %
;
îî% &
}
îî' (
public
úú 
bool
úú 
?
úú 
UniqueItems
úú  
{
úú! "
get
úú# &
;
úú& '
set
úú( +
;
úú+ ,
}
úú- .
}
ùù 
}ûû Ê
DC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\PathItem.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
PathItem 
{ 
public 
	Operation 
Delete 
{  !
get" %
;% &
set' *
;* +
}, -
[!! 	
JsonExtensionData!!	 
]!! 
public"" 

Dictionary"" 
<"" 
string""  
,""  !
object""" (
>""( )

Extensions""* 4
{""5 6
get""7 :
;"": ;
private""< C
set""D G
;""G H
}""I J
=""K L
new""M P

Dictionary""Q [
<""[ \
string""\ b
,""b c
object""d j
>""j k
(""k l
)""l m
;""m n
public** 
	Operation** 
Get** 
{** 
get** "
;**" #
set**$ '
;**' (
}**) *
public22 
	Operation22 
Head22 
{22 
get22  #
;22# $
set22% (
;22( )
}22* +
public:: 
	Operation:: 
Options::  
{::! "
get::# &
;::& '
set::( +
;::+ ,
}::- .
publicBB 
IListBB 
<BB 

IParameterBB 
>BB  

ParametersBB! +
{BB, -
getBB. 1
;BB1 2
setBB3 6
;BB6 7
}BB8 9
publicJJ 
	OperationJJ 
PatchJJ 
{JJ  
getJJ! $
;JJ$ %
setJJ& )
;JJ) *
}JJ+ ,
publicRR 
	OperationRR 
PostRR 
{RR 
getRR  #
;RR# $
setRR% (
;RR( )
}RR* +
publicZZ 
	OperationZZ 
PutZZ 
{ZZ 
getZZ "
;ZZ" #
setZZ$ '
;ZZ' (
}ZZ) *
[bb 	
JsonPropertybb	 
(bb 
$strbb 
)bb 
]bb 
publiccc 
stringcc 
Refcc 
{cc 
getcc 
;cc  
setcc! $
;cc$ %
}cc& '
}dd 
}ee ù
DC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Response.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
Response 
{ 
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public!! 
object!! 
Examples!! 
{!!  
get!!! $
;!!$ %
set!!& )
;!!) *
}!!+ ,
[)) 	
JsonExtensionData))	 
])) 
public** 

Dictionary** 
<** 
string**  
,**  !
object**" (
>**( )

Extensions*** 4
{**5 6
get**7 :
;**: ;
private**< C
set**D G
;**G H
}**I J
=**K L
new**M P

Dictionary**Q [
<**[ \
string**\ b
,**b c
object**d j
>**j k
(**k l
)**l m
;**m n
public22 
IDictionary22 
<22 
string22 !
,22! "
Header22# )
>22) *
Headers22+ 2
{223 4
get225 8
;228 9
set22: =
;22= >
}22? @
public:: 
Schema:: 
Schema:: 
{:: 
get:: "
;::" #
set::$ '
;::' (
}::) *
};; 
}<< §2
BC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Schema.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
Schema 
{ 
public 
Schema  
AdditionalProperties *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public!! 
IList!! 
<!! 
Schema!! 
>!! 
AllOf!! "
{!!# $
get!!% (
;!!( )
set!!* -
;!!- .
}!!/ 0
public)) 
object)) 
Default)) 
{)) 
get))  #
;))# $
set))% (
;))( )
}))* +
public11 
string11 
Description11 !
{11" #
get11$ '
;11' (
set11) ,
;11, -
}11. /
public99 
string99 
Discriminator99 #
{99$ %
get99& )
;99) *
set99+ .
;99. /
}990 1
publicAA 
stringAA 
[AA 
]AA 
EnumAA 
{AA 
getAA "
;AA" #
setAA$ '
;AA' (
}AA) *
publicII 
objectII 
ExampleII 
{II 
getII  #
;II# $
setII% (
;II( )
}II* +
publicQQ 
boolQQ 
?QQ 
ExclusiveMaximumQQ %
{QQ& '
getQQ( +
;QQ+ ,
setQQ- 0
;QQ0 1
}QQ2 3
publicYY 
boolYY 
?YY 
ExclusiveMinimumYY %
{YY& '
getYY( +
;YY+ ,
setYY- 0
;YY0 1
}YY2 3
[aa 	
JsonExtensionDataaa	 
]aa 
publicbb 

Dictionarybb 
<bb 
stringbb  
,bb  !
objectbb" (
>bb( )

Extensionsbb* 4
{bb5 6
getbb7 :
;bb: ;
privatebb< C
setbbD G
;bbG H
}bbI J
=bbK L
newbbM P

DictionarybbQ [
<bb[ \
stringbb\ b
,bbb c
objectbbd j
>bbj k
(bbk l
)bbl m
;bbm n
publicjj 
ExternalDocsjj 
ExternalDocsjj (
{jj) *
getjj+ .
;jj. /
setjj0 3
;jj3 4
}jj5 6
publicrr 
stringrr 
Formatrr 
{rr 
getrr "
;rr" #
setrr$ '
;rr' (
}rr) *
publiczz 
Schemazz 
Itemszz 
{zz 
getzz !
;zz! "
setzz# &
;zz& '
}zz( )
public
ÇÇ 
int
ÇÇ 
?
ÇÇ 
Maximum
ÇÇ 
{
ÇÇ 
get
ÇÇ !
;
ÇÇ! "
set
ÇÇ# &
;
ÇÇ& '
}
ÇÇ( )
public
ää 
int
ää 
?
ää 
MaxItems
ää 
{
ää 
get
ää "
;
ää" #
set
ää$ '
;
ää' (
}
ää) *
public
íí 
int
íí 
?
íí 
	MaxLength
íí 
{
íí 
get
íí  #
;
íí# $
set
íí% (
;
íí( )
}
íí* +
public
öö 
int
öö 
?
öö 
Minimum
öö 
{
öö 
get
öö !
;
öö! "
set
öö# &
;
öö& '
}
öö( )
public
¢¢ 
int
¢¢ 
?
¢¢ 
MinItems
¢¢ 
{
¢¢ 
get
¢¢ "
;
¢¢" #
set
¢¢$ '
;
¢¢' (
}
¢¢) *
public
™™ 
int
™™ 
?
™™ 
	MinLength
™™ 
{
™™ 
get
™™  #
;
™™# $
set
™™% (
;
™™( )
}
™™* +
public
≤≤ 
int
≤≤ 
?
≤≤ 
MinProperties
≤≤ !
{
≤≤" #
get
≤≤$ '
;
≤≤' (
set
≤≤) ,
;
≤≤, -
}
≤≤. /
public
∫∫ 
int
∫∫ 
?
∫∫ 

MultipleOf
∫∫ 
{
∫∫  
get
∫∫! $
;
∫∫$ %
set
∫∫& )
;
∫∫) *
}
∫∫+ ,
public
¬¬ 
string
¬¬ 
Pattern
¬¬ 
{
¬¬ 
get
¬¬  #
;
¬¬# $
set
¬¬% (
;
¬¬( )
}
¬¬* +
public
ÀÀ 
IDictionary
ÀÀ 
<
ÀÀ 
string
ÀÀ !
,
ÀÀ! "
Schema
ÀÀ# )
>
ÀÀ) *

Properties
ÀÀ+ 5
{
ÀÀ6 7
get
ÀÀ8 ;
;
ÀÀ; <
set
ÀÀ= @
;
ÀÀ@ A
}
ÀÀB C
=
ÀÀD E
new
ÀÀF I
SortedDictionary
ÀÀJ Z
<
ÀÀZ [
string
ÀÀ[ a
,
ÀÀa b
Schema
ÀÀc i
>
ÀÀi j
(
ÀÀj k
)
ÀÀk l
;
ÀÀl m
public
”” 
bool
”” 
?
”” 
ReadOnly
”” 
{
”” 
get
””  #
;
””# $
set
””% (
;
””( )
}
””* +
[
€€ 	
JsonProperty
€€	 
(
€€ 
$str
€€ 
)
€€ 
]
€€ 
public
‹‹ 
string
‹‹ 
Ref
‹‹ 
{
‹‹ 
get
‹‹ 
;
‹‹  
set
‹‹! $
;
‹‹$ %
}
‹‹& '
public
‰‰ 
IList
‰‰ 
<
‰‰ 
string
‰‰ 
>
‰‰ 
Required
‰‰ %
{
‰‰& '
get
‰‰( +
;
‰‰+ ,
set
‰‰- 0
;
‰‰0 1
}
‰‰2 3
public
ÏÏ 
string
ÏÏ 
Title
ÏÏ 
{
ÏÏ 
get
ÏÏ !
;
ÏÏ! "
set
ÏÏ# &
;
ÏÏ& '
}
ÏÏ( )
public
ÙÙ 
string
ÙÙ 
Type
ÙÙ 
{
ÙÙ 
get
ÙÙ  
;
ÙÙ  !
set
ÙÙ" %
;
ÙÙ% &
}
ÙÙ' (
public
¸¸ 
bool
¸¸ 
?
¸¸ 
UniqueItems
¸¸  
{
¸¸! "
get
¸¸# &
;
¸¸& '
set
¸¸( +
;
¸¸+ ,
}
¸¸- .
public
ÑÑ 
Xml
ÑÑ 
Xml
ÑÑ 
{
ÑÑ 
get
ÑÑ 
;
ÑÑ 
set
ÑÑ !
;
ÑÑ! "
}
ÑÑ# $
}
ÖÖ 
}ÜÜ ©Ÿ
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\SchemaCollection.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !
OpenApi

! (
{ 
public 

class 
SchemaCollection !
:" #
SortedDictionary$ 4
<4 5
string5 ;
,; <
Schema= C
>C D
{ 
public 
Schema 
GetOrAdd 
( 
Type #
type$ (
,( )
string* 0
description1 <
== >
null? C
)C D
{ 	
if 
( 
type 
== 
null 
) 
{ 
return 
null 
; 
} 
if   
(   
type   
.   

IsNullable   
(    
)    !
)  ! "
{!! 
type"" 
="" 
Nullable"" 
.""  
GetUnderlyingType""  1
(""1 2
type""2 6
)""6 7
;""7 8
}## 
if%% 
(%% 
type%% 
.%% 
IsPrimitive%%  
(%%  !
)%%! "
||%%# %
type%%& *
==%%+ -
typeof%%. 4
(%%4 5
object%%5 ;
)%%; <
)%%< =
{&& 
return'' 
this'' 
.'' !
CreatePrimitiveSchema'' 1
(''1 2
type''2 6
)''6 7
;''7 8
}(( 
if** 
(** 
type** 
.** 
IsDictionary** !
(**! "
)**" #
)**# $
{++ 
return,, 
this,, 
.,, "
CreateDictionarySchema,, 2
(,,2 3
type,,3 7
,,,7 8
description,,9 D
),,D E
;,,E F
}-- 
if// 
(// 
typeof// 
(// 
IEnumerable// "
)//" #
.//# $
IsAssignableFrom//$ 4
(//4 5
type//5 9
)//9 :
||//; =
type//> B
.//B C
IsArray//C J
)//J K
{00 
return11 
this11 
.11 
CreateArraySchema11 -
(11- .
type11. 2
,112 3
description114 ?
)11? @
;11@ A
}22 
var44 
key44 
=44 
GetKey44 
(44 
type44 !
)44! "
;44" #
if66 
(66 
!66 
this66 
.66 
ContainsKey66 !
(66! "
key66" %
)66% &
)66& '
{77 
this88 
.88 
Add88 
(88 
key88 
,88 
null88 "
)88" #
;88# $
this99 
[99 
key99 
]99 
=99 
this99  
.99  !
CreateSchema99! -
(99- .
type99. 2
,992 3
description994 ?
)99? @
;99@ A
}:: 
return<< 
this<< 
[<< 
key<< 
]<< 
;<< 
}== 	
internal?? 
Schema?? !
CreatePrimitiveSchema?? -
(??- .
Type??. 2
type??3 7
,??7 8
string??9 ?
description??@ K
=??L M
null??N R
)??R S
{@@ 	
ifAA 
(AA 
typeAA 
==AA 
typeofAA 
(AA 
boolAA #
)AA# $
)AA$ %
{BB 
returnCC 
newCC 
SchemaCC !
{CC" #
TypeCC$ (
=CC) *
$strCC+ 4
,CC4 5
DescriptionCC6 A
=CCB C
descriptionCCD O
}CCP Q
;CCQ R
}DD 
ifEE 
(EE 
typeEE 
==EE 
typeofEE 
(EE 
GuidEE #
)EE# $
)EE$ %
{FF 
returnGG 
newGG 
SchemaGG !
{GG" #
TypeGG$ (
=GG) *
$strGG+ 3
,GG3 4
FormatGG5 ;
=GG< =
$strGG> D
,GGD E
DescriptionGGF Q
=GGR S
descriptionGGT _
}GG` a
;GGa b
}HH 
ifII 
(II 
typeII 
==II 
typeofII 
(II 
DateTimeII '
)II' (
)II( )
{JJ 
returnKK 
newKK 
SchemaKK !
{KK" #
TypeKK$ (
=KK) *
$strKK+ 3
,KK3 4
FormatKK5 ;
=KK< =
$strKK> I
,KKI J
DescriptionKKK V
=KKW X
descriptionKKY d
}KKe f
;KKf g
}LL 
ifMM 
(MM 
typeMM 
==MM 
typeofMM 
(MM 
DateTimeOffsetMM -
)MM- .
)MM. /
{NN 
returnOO 
newOO 
SchemaOO !
{OO" #
TypeOO$ (
=OO) *
$strOO+ 3
,OO3 4
FormatOO5 ;
=OO< =
$strOO> I
,OOI J
DescriptionOOK V
=OOW X
descriptionOOY d
}OOe f
;OOf g
}PP 
ifQQ 
(QQ 
typeQQ 
==QQ 
typeofQQ 
(QQ 
TimeSpanQQ '
)QQ' (
)QQ( )
{RR 
returnSS 
newSS 
SchemaSS !
{SS" #
TypeSS$ (
=SS) *
$strSS+ 3
,SS3 4
ExampleSS5 <
=SS= >
TimeSpanSS? G
.SSG H
FromSecondsSSH S
(SSS T
$numSST V
)SSV W
,SSW X
DescriptionSSY d
=SSe f
descriptionSSg r
}SSs t
;SSt u
}TT 
ifUU 
(UU 
typeUU 
==UU 
typeofUU 
(UU 
charUU #
)UU# $
)UU$ %
{VV 
returnWW 
newWW 
SchemaWW !
{WW" #
TypeWW$ (
=WW) *
$strWW+ 3
,WW3 4
DescriptionWW5 @
=WWA B
descriptionWWC N
}WWO P
;WWP Q
}XX 
ifYY 
(YY 
typeYY 
==YY 
typeofYY 
(YY 
intYY "
)YY" #
||YY$ &
typeYY' +
==YY, .
typeofYY/ 5
(YY5 6
uintYY6 :
)YY: ;
||YY< >
typeYY? C
==YYD F
typeofYYG M
(YYM N
shortYYN S
)YYS T
||YYU W
typeYYX \
==YY] _
typeofYY` f
(YYf g
ushortYYg m
)YYm n
)YYn o
{ZZ 
return[[ 
new[[ 
Schema[[ !
{[[" #
Type[[$ (
=[[) *
$str[[+ 4
,[[4 5
Format[[6 <
=[[= >
$str[[? F
,[[F G
Description[[H S
=[[T U
description[[V a
}[[b c
;[[c d
}\\ 
if]] 
(]] 
type]] 
==]] 
typeof]] 
(]] 
long]] #
)]]# $
||]]% '
type]]( ,
==]]- /
typeof]]0 6
(]]6 7
ulong]]7 <
)]]< =
)]]= >
{^^ 
return__ 
new__ 
Schema__ !
{__" #
Type__$ (
=__) *
$str__+ 4
,__4 5
Format__6 <
=__= >
$str__? F
,__F G
Description__H S
=__T U
description__V a
}__b c
;__c d
}`` 
ifaa 
(aa 
typeaa 
==aa 
typeofaa 
(aa 
floataa $
)aa$ %
)aa% &
{bb 
returncc 
newcc 
Schemacc !
{cc" #
Typecc$ (
=cc) *
$strcc+ 3
,cc3 4
Formatcc5 ;
=cc< =
$strcc> E
,ccE F
DescriptionccG R
=ccS T
descriptionccU `
}cca b
;ccb c
}dd 
ifee 
(ee 
typeee 
==ee 
typeofee 
(ee 
doubleee %
)ee% &
||ee' )
typeee* .
==ee/ 1
typeofee2 8
(ee8 9
decimalee9 @
)ee@ A
)eeA B
{ff 
returngg 
newgg 
Schemagg !
{gg" #
Typegg$ (
=gg) *
$strgg+ 3
,gg3 4
Formatgg5 ;
=gg< =
$strgg> F
,ggF G
DescriptionggH S
=ggT U
descriptionggV a
}ggb c
;ggc d
}hh 
ifii 
(ii 
typeii 
.ii 

IsNullableii 
(ii  
)ii  !
)ii! "
{jj 
returnkk 
thiskk 
.kk !
CreatePrimitiveSchemakk 1
(kk1 2
Nullablekk2 :
.kk: ;
GetUnderlyingTypekk; L
(kkL M
typekkM Q
)kkQ R
,kkR S
descriptionkkT _
)kk_ `
;kk` a
}ll 
returnmm 
newmm 
Schemamm 
{mm 
Typemm  $
=mm% &
$strmm' /
,mm/ 0
Descriptionmm1 <
=mm= >
descriptionmm? J
}mmK L
;mmL M
}nn 	
internaluu 
staticuu 
stringuu 
GetKeyuu %
(uu% &
Typeuu& *
typeuu+ /
)uu/ 0
{vv 	
returnww 
typeww 
.ww 
FullNameww  
.ww  !
ToCamelCaseww! ,
(ww, -
)ww- .
;ww. /
}xx 	
internalzz 
Schemazz 
GetReferenceSchemazz *
(zz* +
Typezz+ /
typezz0 4
,zz4 5
stringzz6 <
descriptionzz= H
)zzH I
{{{ 	
if|| 
(|| 
type|| 
.|| 

IsNullable|| 
(||  
)||  !
)||! "
{}} 
type~~ 
=~~ 
Nullable~~ 
.~~  
GetUnderlyingType~~  1
(~~1 2
type~~2 6
)~~6 7
;~~7 8
} 
if
ÅÅ 
(
ÅÅ 
type
ÅÅ 
.
ÅÅ 
IsPrimitive
ÅÅ  
(
ÅÅ  !
)
ÅÅ! "
||
ÅÅ# %
type
ÅÅ& *
==
ÅÅ+ -
typeof
ÅÅ. 4
(
ÅÅ4 5
object
ÅÅ5 ;
)
ÅÅ; <
)
ÅÅ< =
{
ÇÇ 
return
ÉÉ 
this
ÉÉ 
.
ÉÉ #
CreatePrimitiveSchema
ÉÉ 1
(
ÉÉ1 2
type
ÉÉ2 6
)
ÉÉ6 7
;
ÉÉ7 8
}
ÑÑ 
if
ÖÖ 
(
ÖÖ 
type
ÖÖ 
.
ÖÖ 
IsDictionary
ÖÖ !
(
ÖÖ! "
)
ÖÖ" #
)
ÖÖ# $
{
ÜÜ 
return
áá 
this
áá 
.
áá $
CreateDictionarySchema
áá 2
(
áá2 3
type
áá3 7
,
áá7 8
description
áá9 D
)
ááD E
;
ááE F
}
àà 
if
ââ 
(
ââ 
type
ââ 
.
ââ 
IsArray
ââ 
)
ââ 
{
ää 
return
ãã 
this
ãã 
.
ãã 
CreateArraySchema
ãã -
(
ãã- .
type
ãã. 2
.
ãã2 3
GetElementType
ãã3 A
(
ããA B
)
ããB C
,
ããC D
description
ããE P
)
ããP Q
;
ããQ R
}
åå 
if
çç 
(
çç 
typeof
çç 
(
çç 
IEnumerable
çç "
)
çç" #
.
çç# $
IsAssignableFrom
çç$ 4
(
çç4 5
type
çç5 9
)
çç9 :
)
çç: ;
{
éé 
if
èè 
(
èè 
type
èè 
.
èè 
GetTypeInfo
èè $
(
èè$ %
)
èè% &
.
èè& '
IsInterface
èè' 2
)
èè2 3
{
êê 
return
ëë 
this
ëë 
.
ëë  
CreateArraySchema
ëë  1
(
ëë1 2
type
ëë2 6
.
ëë6 7!
GetGenericArguments
ëë7 J
(
ëëJ K
)
ëëK L
[
ëëL M
$num
ëëM N
]
ëëN O
,
ëëO P
description
ëëQ \
)
ëë\ ]
;
ëë] ^
}
íí 
var
ìì 
target
ìì 
=
ìì 
type
ìì !
.
ìì! "
GetInterfaces
ìì" /
(
ìì/ 0
)
ìì0 1
.
ìì1 2
First
ìì2 7
(
ìì7 8
e
ìì8 9
=>
ìì: <
e
ìì= >
.
ìì> ?
GetTypeInfo
ìì? J
(
ììJ K
)
ììK L
.
ììL M
IsGenericType
ììM Z
&&
ìì[ ]
e
ìì^ _
.
ìì_ `&
GetGenericTypeDefinition
ìì` x
(
ììx y
)
ììy z
==
ìì{ }
typeofìì~ Ñ
(ììÑ Ö
IEnumerableììÖ ê
<ììê ë
>ììë í
)ììí ì
)ììì î
;ììî ï
return
îî 
this
îî 
.
îî 
CreateArraySchema
îî -
(
îî- .
target
îî. 4
.
îî4 5!
GetGenericArguments
îî5 H
(
îîH I
)
îîI J
[
îîJ K
$num
îîK L
]
îîL M
,
îîM N
description
îîO Z
)
îîZ [
;
îî[ \
}
ïï 
this
ññ 
.
ññ 
GetOrAdd
ññ 
(
ññ 
type
ññ 
)
ññ 
;
ññ  
return
óó 
this
óó 
.
óó #
CreateReferenceSchema
óó -
(
óó- .
type
óó. 2
,
óó2 3
description
óó4 ?
)
óó? @
;
óó@ A
}
òò 	
private
öö 
Schema
öö 
CreateArraySchema
öö (
(
öö( )
Type
öö) -
type
öö. 2
,
öö2 3
string
öö4 :
description
öö; F
)
ööF G
{
õõ 	
if
úú 
(
úú 
type
úú 
.
úú 
IsArray
úú 
)
úú 
{
ùù 
return
ûû 
this
ûû 
.
ûû 
CreateArraySchema
ûû -
(
ûû- .
type
ûû. 2
.
ûû2 3
GetElementType
ûû3 A
(
ûûA B
)
ûûB C
,
ûûC D
description
ûûE P
)
ûûP Q
;
ûûQ R
}
üü 
if
†† 
(
†† 
typeof
†† 
(
†† 
IEnumerable
†† "
)
††" #
.
††# $
IsAssignableFrom
††$ 4
(
††4 5
type
††5 9
)
††9 :
&&
††; =
type
††> B
!=
††C E
typeof
††F L
(
††L M
string
††M S
)
††S T
)
††T U
{
°° 
if
¢¢ 
(
¢¢ 
type
¢¢ 
.
¢¢ 
GetTypeInfo
¢¢ $
(
¢¢$ %
)
¢¢% &
.
¢¢& '
IsInterface
¢¢' 2
)
¢¢2 3
{
££ 
return
§§ 
this
§§ 
.
§§  
CreateArraySchema
§§  1
(
§§1 2
type
§§2 6
.
§§6 7!
GetGenericArguments
§§7 J
(
§§J K
)
§§K L
[
§§L M
$num
§§M N
]
§§N O
,
§§O P
description
§§Q \
)
§§\ ]
;
§§] ^
}
•• 
var
¶¶ 
target
¶¶ 
=
¶¶ 
type
¶¶ !
.
¶¶! "
GetInterfaces
¶¶" /
(
¶¶/ 0
)
¶¶0 1
.
¶¶1 2
First
¶¶2 7
(
¶¶7 8
e
¶¶8 9
=>
¶¶: <
e
¶¶= >
.
¶¶> ?
GetTypeInfo
¶¶? J
(
¶¶J K
)
¶¶K L
.
¶¶L M
IsGenericType
¶¶M Z
&&
¶¶[ ]
e
¶¶^ _
.
¶¶_ `&
GetGenericTypeDefinition
¶¶` x
(
¶¶x y
)
¶¶y z
==
¶¶{ }
typeof¶¶~ Ñ
(¶¶Ñ Ö
IEnumerable¶¶Ö ê
<¶¶ê ë
>¶¶ë í
)¶¶í ì
)¶¶ì î
;¶¶î ï
return
ßß 
this
ßß 
.
ßß 
CreateArraySchema
ßß -
(
ßß- .
target
ßß. 4
.
ßß4 5!
GetGenericArguments
ßß5 H
(
ßßH I
)
ßßI J
[
ßßJ K
$num
ßßK L
]
ßßL M
,
ßßM N
description
ßßO Z
)
ßßZ [
;
ßß[ \
}
®® 
this
™™ 
.
™™ 
GetOrAdd
™™ 
(
™™ 
type
™™ 
)
™™ 
;
™™  
return
¨¨ 
new
¨¨ 
Schema
¨¨ 
{
≠≠ 
Type
ÆÆ 
=
ÆÆ 
$str
ÆÆ 
,
ÆÆ 
Items
ØØ 
=
ØØ 
type
ØØ 
.
ØØ 
IsPrimitive
ØØ (
(
ØØ( )
)
ØØ) *
||
ØØ+ -
type
ØØ. 2
==
ØØ3 5
typeof
ØØ6 <
(
ØØ< =
object
ØØ= C
)
ØØC D
?
ØØE F
this
ØØG K
.
ØØK L#
CreatePrimitiveSchema
ØØL a
(
ØØa b
type
ØØb f
)
ØØf g
:
ØØh i
this
ØØj n
.
ØØn o$
CreateReferenceSchemaØØo Ñ
(ØØÑ Ö
typeØØÖ â
)ØØâ ä
,ØØä ã
Description
∞∞ 
=
∞∞ 
description
∞∞ )
}
±± 
;
±± 
}
≤≤ 	
private
¥¥ 
Schema
¥¥ $
CreateDictionarySchema
¥¥ -
(
¥¥- .
Type
¥¥. 2
type
¥¥3 7
,
¥¥7 8
string
¥¥9 ?
description
¥¥@ K
=
¥¥L M
null
¥¥N R
)
¥¥R S
{
µµ 	
var
∂∂ 
	valueType
∂∂ 
=
∂∂ 
type
∂∂  
.
∂∂  !
GetInterfaces
∂∂! .
(
∂∂. /
)
∂∂/ 0
.
∂∂0 1
FirstOrDefault
∂∂1 ?
(
∂∂? @
e
∂∂@ A
=>
∂∂B D
e
∂∂E F
.
∂∂F G
GetTypeInfo
∂∂G R
(
∂∂R S
)
∂∂S T
.
∂∂T U
IsGenericType
∂∂U b
&&
∂∂c e
e
∂∂f g
.
∂∂g h'
GetGenericTypeDefinition∂∂h Ä
(∂∂Ä Å
)∂∂Å Ç
==∂∂É Ö
typeof∂∂Ü å
(∂∂å ç
IDictionary∂∂ç ò
<∂∂ò ô
,∂∂ô ö
>∂∂ö õ
)∂∂õ ú
)∂∂ú ù
?∂∂ù û
.∂∂û ü#
GetGenericArguments∂∂ü ≤
(∂∂≤ ≥
)∂∂≥ ¥
.∂∂¥ µ
	ElementAt∂∂µ æ
(∂∂æ ø
$num∂∂ø ¿
)∂∂¿ ¡
;∂∂¡ ¬
this
∑∑ 
.
∑∑ 
GetOrAdd
∑∑ 
(
∑∑ 
	valueType
∑∑ #
)
∑∑# $
;
∑∑$ %
if
∏∏ 
(
∏∏ 
	valueType
∏∏ 
!=
∏∏ 
null
∏∏ !
)
∏∏! "
{
ππ 
return
∫∫ 
new
∫∫ 
Schema
∫∫ !
{
ªª 
Type
ºº 
=
ºº 
$str
ºº #
,
ºº# $"
AdditionalProperties
ΩΩ (
=
ΩΩ) *
	valueType
ΩΩ+ 4
.
ΩΩ4 5
IsPrimitive
ΩΩ5 @
(
ΩΩ@ A
)
ΩΩA B
||
ΩΩC E
	valueType
ΩΩF O
==
ΩΩP R
typeof
ΩΩS Y
(
ΩΩY Z
object
ΩΩZ `
)
ΩΩ` a
?
ΩΩb c
this
ΩΩd h
.
ΩΩh i#
CreatePrimitiveSchema
ΩΩi ~
(
ΩΩ~ 
	valueTypeΩΩ à
)ΩΩà â
:ΩΩä ã
thisΩΩå ê
.ΩΩê ë%
CreateReferenceSchemaΩΩë ¶
(ΩΩ¶ ß
	valueTypeΩΩß ∞
,ΩΩ∞ ±
descriptionΩΩ≤ Ω
)ΩΩΩ æ
,ΩΩæ ø
Description
ææ 
=
ææ  !
description
ææ" -
}
øø 
;
øø 
}
¿¿ 
return
¡¡ 
new
¡¡ 
Schema
¡¡ 
{
¬¬ 
Type
√√ 
=
√√ 
$str
√√ 
,
√√  
Description
ƒƒ 
=
ƒƒ 
description
ƒƒ )
}
≈≈ 
;
≈≈ 
}
∆∆ 	
private
»» 
Schema
»» 
CreateEnumSchema
»» '
(
»»' (
Type
»»( ,
type
»»- 1
,
»»1 2
string
»»3 9
description
»»: E
=
»»F G
null
»»H L
)
»»L M
{
…… 	
return
   
new
   
Schema
   
{
ÀÀ 
Type
ÃÃ 
=
ÃÃ 
$str
ÃÃ 
,
ÃÃ  
Enum
ÕÕ 
=
ÕÕ 
Enum
ÕÕ 
.
ÕÕ 
GetNames
ÕÕ $
(
ÕÕ$ %
type
ÕÕ% )
)
ÕÕ) *
.
ÕÕ* +
Select
ÕÕ+ 1
(
ÕÕ1 2
name
ÕÕ2 6
=>
ÕÕ7 9
name
ÕÕ: >
.
ÕÕ> ?
ToCamelCase
ÕÕ? J
(
ÕÕJ K
)
ÕÕK L
)
ÕÕL M
.
ÕÕM N
ToArray
ÕÕN U
(
ÕÕU V
)
ÕÕV W
,
ÕÕW X
Description
ŒŒ 
=
ŒŒ 
description
ŒŒ )
??
ŒŒ* ,
type
ŒŒ- 1
.
ŒŒ1 2
GetComments
ŒŒ2 =
(
ŒŒ= >
)
ŒŒ> ?
?
ŒŒ? @
.
ŒŒ@ A
Summary
ŒŒA H
}
œœ 
;
œœ 
}
–– 	
private
““ 
Schema
““  
CreateObjectSchema
““ )
(
““) *
Type
““* .
type
““/ 3
,
““3 4
string
““5 ;
description
““< G
=
““H I
null
““J N
)
““N O
{
”” 	
var
‘‘ 
schema
‘‘ 
=
‘‘ 
new
‘‘ 
Schema
‘‘ #
{
’’ 
Type
÷÷ 
=
÷÷ 
$str
÷÷ 
,
÷÷  
Description
◊◊ 
=
◊◊ 
description
◊◊ )
??
◊◊* ,
type
◊◊- 1
.
◊◊1 2
GetComments
◊◊2 =
(
◊◊= >
)
◊◊> ?
?
◊◊? @
.
◊◊@ A
Summary
◊◊A H
}
ÿÿ 
;
ÿÿ 
var
⁄⁄ 
required
⁄⁄ 
=
⁄⁄ 
new
⁄⁄ 
List
⁄⁄ #
<
⁄⁄# $
string
⁄⁄$ *
>
⁄⁄* +
(
⁄⁄+ ,
)
⁄⁄, -
;
⁄⁄- .
foreach
€€ 
(
€€ 
var
€€ 
property
€€ !
in
€€" $
type
€€% )
.
€€) *
GetProperties
€€* 7
(
€€7 8
)
€€8 9
)
€€9 :
{
‹‹ 
schema
›› 
.
›› 

Properties
›› !
.
››! "
Add
››" %
(
››% &
property
››& .
.
››. /
Name
››/ 3
,
››3 4
this
››5 9
.
››9 : 
GetReferenceSchema
››: L
(
››L M
property
››M U
.
››U V
PropertyType
››V b
,
››b c
description
››d o
)
››o p
)
››p q
;
››q r
if
ﬁﬁ 
(
ﬁﬁ 
property
ﬁﬁ 
.
ﬁﬁ !
GetCustomAttributes
ﬁﬁ 0
<
ﬁﬁ0 1!
ValidationAttribute
ﬁﬁ1 D
>
ﬁﬁD E
(
ﬁﬁE F
true
ﬁﬁF J
)
ﬁﬁJ K
.
ﬁﬁK L
Any
ﬁﬁL O
(
ﬁﬁO P
)
ﬁﬁP Q
)
ﬁﬁQ R
{
ﬂﬂ 
required
‡‡ 
.
‡‡ 
Add
‡‡  
(
‡‡  !
property
‡‡! )
.
‡‡) *
Name
‡‡* .
.
‡‡. /
ToCamelCase
‡‡/ :
(
‡‡: ;
)
‡‡; <
)
‡‡< =
;
‡‡= >
}
·· 
}
‚‚ 
if
„„ 
(
„„ 
required
„„ 
.
„„ 
Any
„„ 
(
„„ 
)
„„ 
)
„„ 
{
‰‰ 
schema
ÂÂ 
.
ÂÂ 
Required
ÂÂ 
=
ÂÂ  !
required
ÂÂ" *
;
ÂÂ* +
}
ÊÊ 
return
ÁÁ 
schema
ÁÁ 
;
ÁÁ 
}
ËË 	
private
ÍÍ 
Schema
ÍÍ #
CreateReferenceSchema
ÍÍ ,
(
ÍÍ, -
Type
ÍÍ- 1
type
ÍÍ2 6
,
ÍÍ6 7
string
ÍÍ8 >
description
ÍÍ? J
=
ÍÍK L
null
ÍÍM Q
)
ÍÍQ R
{
ÎÎ 	
return
ÏÏ 
new
ÏÏ 
Schema
ÏÏ 
{
ÌÌ 
Ref
ÓÓ 
=
ÓÓ 
$"
ÓÓ 
#/definitions/
ÓÓ &
{
ÓÓ& '
GetKey
ÓÓ' -
(
ÓÓ- .
type
ÓÓ. 2
)
ÓÓ2 3
}
ÓÓ3 4
"
ÓÓ4 5
,
ÓÓ5 6
Description
ÔÔ 
=
ÔÔ 
description
ÔÔ )
}
 
;
 
}
ÒÒ 	
private
ÛÛ 
Schema
ÛÛ 
CreateSchema
ÛÛ #
(
ÛÛ# $
Type
ÛÛ$ (
type
ÛÛ) -
,
ÛÛ- .
string
ÛÛ/ 5
description
ÛÛ6 A
=
ÛÛB C
null
ÛÛD H
)
ÛÛH I
{
ÙÙ 	
if
ıı 
(
ıı 
type
ıı 
.
ıı 

IsNullable
ıı 
(
ıı  
)
ıı  !
)
ıı! "
{
ˆˆ 
return
˜˜ 
this
˜˜ 
.
˜˜ 
CreateSchema
˜˜ (
(
˜˜( )
Nullable
˜˜) 1
.
˜˜1 2
GetUnderlyingType
˜˜2 C
(
˜˜C D
type
˜˜D H
)
˜˜H I
,
˜˜I J
description
˜˜K V
)
˜˜V W
;
˜˜W X
}
¯¯ 
if
˘˘ 
(
˘˘ 
type
˘˘ 
.
˘˘ 
GetTypeInfo
˘˘  
(
˘˘  !
)
˘˘! "
.
˘˘" #
IsEnum
˘˘# )
)
˘˘) *
{
˙˙ 
return
˚˚ 
this
˚˚ 
.
˚˚ 
CreateEnumSchema
˚˚ ,
(
˚˚, -
type
˚˚- 1
,
˚˚1 2
description
˚˚3 >
)
˚˚> ?
;
˚˚? @
}
¸¸ 
if
˝˝ 
(
˝˝ 
type
˝˝ 
.
˝˝ 
IsPrimitive
˝˝  
(
˝˝  !
)
˝˝! "
)
˝˝" #
{
˛˛ 
return
ˇˇ 
this
ˇˇ 
.
ˇˇ #
CreatePrimitiveSchema
ˇˇ 1
(
ˇˇ1 2
type
ˇˇ2 6
,
ˇˇ6 7
description
ˇˇ8 C
)
ˇˇC D
;
ˇˇD E
}
ÄÄ 
if
ÅÅ 
(
ÅÅ 
type
ÅÅ 
.
ÅÅ 
IsDictionary
ÅÅ !
(
ÅÅ! "
)
ÅÅ" #
)
ÅÅ# $
{
ÇÇ 
return
ÉÉ 
this
ÉÉ 
.
ÉÉ $
CreateDictionarySchema
ÉÉ 2
(
ÉÉ2 3
type
ÉÉ3 7
,
ÉÉ7 8
description
ÉÉ9 D
)
ÉÉD E
;
ÉÉE F
}
ÑÑ 
if
ÖÖ 
(
ÖÖ 
type
ÖÖ 
.
ÖÖ 
IsArray
ÖÖ 
||
ÖÖ 
typeof
ÖÖ  &
(
ÖÖ& '
IEnumerable
ÖÖ' 2
)
ÖÖ2 3
.
ÖÖ3 4
IsAssignableFrom
ÖÖ4 D
(
ÖÖD E
type
ÖÖE I
)
ÖÖI J
)
ÖÖJ K
{
ÜÜ 
return
áá 
this
áá 
.
áá 
CreateArraySchema
áá -
(
áá- .
type
áá. 2
,
áá2 3
description
áá4 ?
)
áá? @
;
áá@ A
}
àà 
return
ââ 
this
ââ 
.
ââ  
CreateObjectSchema
ââ *
(
ââ* +
type
ââ+ /
,
ââ/ 0
description
ââ1 <
)
ââ< =
;
ââ= >
}
ää 	
}
ãã 
}åå ¢
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\SecurityScheme.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public 

class 
SecurityScheme 
{ 
public 
string 
Type 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
In 
{ 
get 
; 
set  #
;# $
}% &
} 
}   £
?C:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Tag.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{ 
public		 

class		 
Tag		 
{

 
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
ExternalDocs 
ExternalDocs (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public!! 
string!! 
Name!! 
{!! 
get!!  
;!!  !
set!!" %
;!!% &
}!!' (
}"" 
}## Á
?C:\Source\Stacks\Core\src\Slalom.Stacks\Services\OpenApi\Xml.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
OpenApi! (
{		 
public 

class 
Xml 
{ 
public 
bool 
? 
	Attribute 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public&& 
string&& 
	Namespace&& 
{&&  !
get&&" %
;&&% &
set&&' *
;&&* +
}&&, -
public.. 
string.. 
Prefix.. 
{.. 
get.. "
;.." #
set..$ '
;..' (
}..) *
public66 
bool66 
?66 
Wrapped66 
{66 
get66 "
;66" #
set66$ '
;66' (
}66) *
}77 
}88 £
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\Complete.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
Complete 
: !
IMessageExecutionStep 3
{ 
public 
Task 
Execute 
( 
ExecutionContext ,
context- 4
)4 5
{ 	
context 
. 
Complete 
( 
) 
; 
return 
Task 
. 

FromResult "
(" #
$num# $
)$ %
;% &
} 	
} 
} ﬂ
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\HandleException.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
HandleException "
:# $!
IMessageExecutionStep% :
{ 
public 
Task 
Execute 
( 
ExecutionContext ,
context- 4
)4 5
{ 	
var 
	exception 
= 
context #
.# $
	Exception$ -
;- .
var 
validationException #
=$ %
	exception& /
as0 2
ValidationException3 F
;F G
if 
( 
validationException #
!=$ &
null' +
)+ ,
{ 
context 
. 
AddValidationErrors +
(+ ,
validationException, ?
.? @
ValidationErrors@ P
)P Q
;Q R
context 
. 
SetException $
($ %
null% )
)) *
;* +
} 
else   
if   
(   
	exception   
is   !
AggregateException  " 4
)  4 5
{!! 
var"" 
innerException"" "
=""# $
	exception""% .
."". /
InnerException""/ =
as""> @
ValidationException""A T
;""T U
if## 
(## 
innerException## "
!=### %
null##& *
)##* +
{$$ 
context%% 
.%% 
AddValidationErrors%% /
(%%/ 0
innerException%%0 >
.%%> ?
ValidationErrors%%? O
)%%O P
;%%P Q
context&& 
.&& 
SetException&& (
(&&( )
null&&) -
)&&- .
;&&. /
}'' 
else(( 
if(( 
((( 
	exception(( "
.((" #
InnerException((# 1
is((2 4%
TargetInvocationException((5 N
)((N O
{)) 
context** 
.** 
SetException** (
(**( )
(**) *
(*** +%
TargetInvocationException**+ D
)**D E
	exception**F O
.**O P
InnerException**P ^
)**^ _
.**_ `
InnerException**` n
)**n o
;**o p
}++ 
else,, 
if,, 
(,, 
(,, 
(,, 
AggregateException,, -
),,- .
	exception,,/ 8
),,8 9
.,,9 :
InnerExceptions,,: I
.,,I J
Count,,J O
==,,P R
$num,,S T
),,T U
{-- 
context.. 
... 
SetException.. (
(..( )
	exception..) 2
...2 3
InnerException..3 A
)..A B
;..B C
}// 
}00 
else11 
if11 
(11 
	exception11 
is11 !%
TargetInvocationException11" ;
)11; <
{22 
context33 
.33 
SetException33 $
(33$ %
	exception33% .
.33. /
InnerException33/ =
)33= >
;33> ?
}44 
else55 
{66 
context77 
.77 
SetException77 $
(77$ %
	exception77% .
)77. /
;77/ 0
}88 
return99 
Task99 
.99 

FromResult99 "
(99" #
$num99# $
)99$ %
;99% &
}:: 	
};; 
}<< ˘
RC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\IMessageExecutionStep.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
	interface !
IMessageExecutionStep ,
{ 
Task 
Execute 
( 
ExecutionContext %
context& -
)- .
;. /
} 
} ¶(
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\LogCompletion.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
LogCompletion  
:! "!
IMessageExecutionStep# 8
{ 
private 
readonly 
IResponseLog %
_actions& .
;. /
private 
readonly 
Application $
_environmentContext% 8
;8 9
private 
readonly 
ILogger  
_logger! (
;( )
public!! 
LogCompletion!! 
(!! 
IComponentContext!! .

components!!/ 9
)!!9 :
{"" 	
_actions## 
=## 

components## !
.##! "
Resolve##" )
<##) *
IResponseLog##* 6
>##6 7
(##7 8
)##8 9
;##9 :
_logger$$ 
=$$ 

components$$  
.$$  !
Resolve$$! (
<$$( )
ILogger$$) 0
>$$0 1
($$1 2
)$$2 3
;$$3 4
_environmentContext%% 
=%%  !

components%%" ,
.%%, -
Resolve%%- 4
<%%4 5
Application%%5 @
>%%@ A
(%%A B
)%%B C
;%%C D
}&& 	
public)) 
Task)) 
Execute)) 
()) 
ExecutionContext)) ,
context))- 4
)))4 5
{** 	
if++ 
(++ 
context++ 
.++ 
Request++ 
.++  
Path++  $
?++$ %
.++% &

StartsWith++& 0
(++0 1
$str++1 4
)++4 5
==++6 8
true++9 =
&&++> @
context++A H
.++H I
IsSuccessful++I U
)++U V
{,, 
return-- 
Task-- 
.-- 

FromResult-- &
(--& '
$num--' (
)--( )
;--) *
}.. 
var00 
tasks00 
=00 
new00 
List00  
<00  !
Task00! %
>00% &
{00' (
_actions00( 0
.000 1
Append001 7
(007 8
new008 ;
ResponseEntry00< I
(00I J
context00J Q
,00Q R
_environmentContext00S f
)00f g
)00g h
}00h i
;00i j
var22 
name22 
=22 
context22 
.22 
Request22 &
.22& '
Path22' +
??22, .
context22/ 6
.226 7
Request227 >
.22> ?
Message22? F
.22F G
Name22G K
;22K L
if33 
(33 
!33 
context33 
.33 
IsSuccessful33 %
)33% &
{44 
if55 
(55 
context55 
.55 
	Exception55 %
!=55& (
null55) -
)55- .
{66 
_logger77 
.77 
Error77 !
(77! "
context77" )
.77) *
	Exception77* 3
,773 4
$str775 k
+77l m
name77n r
+77s t
$str77u z
,77z {
context	77| É
)
77É Ñ
;
77Ñ Ö
}88 
else99 
if99 
(99 
context99  
.99  !
ValidationErrors99! 1
?991 2
.992 3
Any993 6
(996 7
)997 8
??999 ;
false99< A
)99A B
{:: 
_logger;; 
.;; 
Error;; !
(;;! "
$str;;" a
+;;b c
name;;d h
+;;i j
$str;;k q
+;;r s
string;;t z
.;;z {
Join;;{ 
(	;; Ä
$str
;;Ä Ñ
,
;;Ñ Ö
context
;;Ü ç
.
;;ç é
ValidationErrors
;;é û
.
;;û ü
Select
;;ü •
(
;;• ¶
e
;;¶ ß
=>
;;® ™
e
;;´ ¨
.
;;¨ ≠
Type
;;≠ ±
+
;;≤ ≥
$str
;;¥ ∏
+
;;π ∫
e
;;ª º
.
;;º Ω
Message
;;Ω ƒ
)
;;ƒ ≈
)
;;≈ ∆
,
;;∆ «
context
;;» œ
)
;;œ –
;
;;– —
}<< 
else== 
{>> 
_logger?? 
.?? 
Error?? !
(??! "
$str??" Y
+??Z [
name??\ `
+??a b
$str??c h
,??h i
context??j q
)??q r
;??r s
}@@ 
}AA 
elseBB 
{CC 
_loggerDD 
.DD 
VerboseDD 
(DD  
$strDD  :
+DD; <
nameDD= A
+DDB C
$strDDD I
,DDI J
contextDDK R
)DDR S
;DDS T
}EE 
returnGG 
TaskGG 
.GG 
WhenAllGG 
(GG  
tasksGG  %
)GG% &
;GG& '
}HH 	
}II 
}JJ Œ
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\LogStart.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
LogStart 
: !
IMessageExecutionStep 3
{ 
private 
readonly 
ILogger  
_logger! (
;( )
private 
readonly 
IRequestLog $
	_requests% .
;. /
public 
LogStart 
( 
ILogger 
logger  &
,& '
IRequestLog( 3
requests4 <
)< =
{ 	
Argument   
.   
NotNull   
(   
logger   #
,  # $
nameof  % +
(  + ,
logger  , 2
)  2 3
)  3 4
;  4 5
_logger"" 
="" 
logger"" 
;"" 
	_requests## 
=## 
requests##  
;##  !
}$$ 	
public'' 
async'' 
Task'' 
Execute'' !
(''! "
ExecutionContext''" 2
context''3 :
)'': ;
{(( 	
await)) 
	_requests)) 
.)) 
Append)) "
())" #
context))# *
.))* +
Request))+ 2
)))2 3
.))3 4
ConfigureAwait))4 B
())B C
false))C H
)))H I
;))I J
var++ 
message++ 
=++ 
context++ !
.++! "
Request++" )
.++) *
Message++* 1
;++1 2
if,, 
(,, 
message,, 
.,, 
Body,, 
!=,, 
null,,  $
&&,,% '
context,,( /
.,,/ 0
Request,,0 7
.,,7 8
Path,,8 <
!=,,= ?
null,,@ D
),,D E
{-- 
_logger.. 
... 
Verbose.. 
(..  
$str..  .
+../ 0
message..1 8
...8 9
Name..9 =
+..> ?
$str..@ O
+..P Q
context..R Y
...Y Z
Request..Z a
...a b
Path..b f
+..g h
$str..i n
)..n o
;..o p
}// 
else00 
if00 
(00 
message00 
.00 
Body00 !
!=00" $
null00% )
)00) *
{11 
_logger22 
.22 
Verbose22 
(22  
$str22  .
+22/ 0
message221 8
.228 9
Name229 =
+22> ?
$str22@ C
)22C D
;22D E
}33 
else44 
{55 
_logger66 
.66 
Verbose66 
(66  
$str66  >
+66? @
context66A H
.66H I
Request66I P
.66P Q
Path66Q U
+66V W
$str66X ]
)66] ^
;66^ _
}77 
}88 	
}99 
}:: ê
JC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\PublishEvents.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
PublishEvents  
:! "!
IMessageExecutionStep# 8
{ 
private 
readonly 
IEventStore $
_eventStore% 0
;0 1
private 
readonly 
IMessageGateway (
_messageGateway) 8
;8 9
private 
readonly 
IEnumerable $
<$ %
IEventPublisher% 4
>4 5
_eventPublishers6 F
;F G
public 
PublishEvents 
( 
IComponentContext .

components/ 9
)9 :
{   	
_messageGateway!! 
=!! 

components!! (
.!!( )
Resolve!!) 0
<!!0 1
IMessageGateway!!1 @
>!!@ A
(!!A B
)!!B C
;!!C D
_eventStore"" 
="" 

components"" $
.""$ %
Resolve""% ,
<"", -
IEventStore""- 8
>""8 9
(""9 :
)"": ;
;""; <
_eventPublishers## 
=## 

components## )
.##) *

ResolveAll##* 4
<##4 5
IEventPublisher##5 D
>##D E
(##E F
)##F G
;##G H
}$$ 	
public'' 
async'' 
Task'' 
Execute'' !
(''! "
ExecutionContext''" 2
context''3 :
)'': ;
{(( 	
if)) 
()) 
context)) 
.)) 
IsSuccessful)) $
)))$ %
{** 
var++ 
raisedEvents++  
=++! "
context++# *
.++* +
RaisedEvents+++ 7
.++7 8
Union++8 =
(++= >
new++> A
[++A B
]++B C
{++D E
context++E L
.++L M
Response++M U
as++V X
EventMessage++Y e
}++e f
)++f g
.++g h
Where++h m
(++m n
e++n o
=>++p r
e++s t
!=++u w
null++x |
)++| }
.++} ~
ToArray	++~ Ö
(
++Ö Ü
)
++Ü á
;
++á à
foreach,, 
(,, 
var,, 
instance,, %
in,,& (
raisedEvents,,) 5
),,5 6
{-- 
await.. 
_eventStore.. %
...% &
Append..& ,
(.., -
instance..- 5
)..5 6
;..6 7
await00 
_messageGateway00 )
.00) *
Publish00* 1
(001 2
instance002 :
,00: ;
context00< C
)00C D
;00D E
}11 
await33 
Task33 
.33 
WhenAll33 "
(33" #
_eventPublishers33# 3
.333 4
Select334 :
(33: ;
e33; <
=>33= ?
e33@ A
.33A B
Publish33B I
(33I J
raisedEvents33J V
)33V W
)33W X
)33X Y
;33Y Z
}44 
}55 	
}66 
}77 Ë
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Pipeline\ValidateMessage.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Pipeline! )
{ 
internal 
class 
ValidateMessage "
:# $!
IMessageExecutionStep% :
{ 
private 
readonly 
IComponentContext *
_components+ 6
;6 7
public 
ValidateMessage 
( 
IComponentContext 0

components1 ;
); <
{ 	
Argument 
. 
NotNull 
( 

components '
,' (
nameof) /
(/ 0

components0 :
): ;
); <
;< =
_components   
=   

components   $
;  $ %
}!! 	
public$$ 
async$$ 
Task$$ 
Execute$$ !
($$! "
ExecutionContext$$" 2
context$$3 :
)$$: ;
{%% 	
var&& 
message&& 
=&& 
context&& !
.&&! "
Request&&" )
.&&) *
Message&&* 1
;&&1 2
if(( 
((( 
message(( 
.(( 
Body(( 
!=(( 
null((  $
)(($ %
{)) 
var** 
	validator** 
=** 
(**  !
IMessageValidator**! 2
)**2 3
_components**4 ?
.**? @
Resolve**@ G
(**G H
typeof**H N
(**N O
MessageValidator**O _
<**_ `
>**` a
)**a b
.**b c
MakeGenericType**c r
(**r s
context**s z
.**z {
EndPoint	**{ É
.
**É Ñ
RequestType
**Ñ è
)
**è ê
)
**ê ë
;
**ë í
var++ 
results++ 
=++ 
await++ #
	validator++$ -
.++- .
Validate++. 6
(++6 7
message++7 >
.++> ?
Body++? C
,++C D
context++E L
)++L M
;++M N
context,, 
.,, 
AddValidationErrors,, +
(,,+ ,
results,,, 3
),,3 4
;,,4 5
}-- 
}.. 	
}// 
}00 ¨
WC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Serialization\EventContractResolver.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !
Serialization! .
{ 
public 

class !
EventContractResolver &
:' ( 
BaseContractResolver) =
{ 
	protected 
override 
JsonProperty '
CreateProperty( 6
(6 7

MemberInfo7 A
memberB H
,H I
MemberSerializationJ ]
memberSerialization^ q
)q r
{ 	
var 
prop 
= 
base 
. 
CreateProperty *
(* +
member+ 1
,1 2
memberSerialization3 F
)F G
;G H
if 
( 
( 
member 
as 
PropertyInfo '
)' (
.( )
GetCustomAttributes) <
<< =
IgnoreAttribute= L
>L M
(M N
)N O
.O P
AnyP S
(S T
)T U
)U V
{   
prop!! 
.!! 
Ignored!! 
=!! 
true!! #
;!!# $
return"" 
prop"" 
;"" 
}## 
var$$ 
declaringType$$ 
=$$ 
($$  !
member$$! '
as$$( *
PropertyInfo$$+ 7
)$$7 8
?$$8 9
.$$9 :
DeclaringType$$: G
;$$G H
if%% 
(%% 
declaringType%% 
==%%  
typeof%%! '
(%%' (
EventMessage%%( 4
)%%4 5
)%%5 6
{&& 
prop'' 
.'' 
Ignored'' 
='' 
true'' #
;''# $
return(( 
prop(( 
;(( 
})) 
return** 
base** 
.** 
CreateProperty** &
(**& '
member**' -
,**- .
memberSerialization**/ B
)**B C
;**C D
}++ 	
},, 
}-- ˝
EC:\Source\Stacks\Core\src\Slalom.Stacks\Services\ServiceExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

static 
class 
ServiceExtensions )
{ 
public 
static 
ServiceInventory &
GetServices' 2
(2 3
this3 7
Stack8 =
instance> F
)F G
{ 	
return 
instance 
. 
	Container %
.% &
Resolve& -
<- .
ServiceInventory. >
>> ?
(? @
)@ A
;A B
} 	
} 
} ú	
FC:\Source\Stacks\Core\src\Slalom.Stacks\Services\SubscribeAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
,* +
AllowMultiple, 9
=: ;
true< @
)@ A
]A B
public 

class 
SubscribeAttribute #
:$ %
	Attribute& /
{ 
public 
SubscribeAttribute !
(! "
string" (
channel) 0
)0 1
{ 	
Argument 
. 
NotNullOrWhiteSpace (
(( )
channel) 0
,0 1
nameof2 8
(8 9
channel9 @
)@ A
)A B
;B C
this 
. 
Channel 
= 
channel "
;" #
} 	
public## 
string## 
Channel## 
{## 
get##  #
;### $
}##% &
}$$ 
}%% Ü>
BC:\Source\Stacks\Core\src\Slalom.Stacks\Services\TypeExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
{ 
public 

static 
class 
TypeExtensions &
{ 
private 
static 
readonly  
ConcurrentDictionary  4
<4 5
Assembly5 =
,= >
	XDocument? H
>H I
_commentsCacheJ X
=Y Z
new[ ^ 
ConcurrentDictionary_ s
<s t
Assemblyt |
,| }
	XDocument	~ á
>
á à
(
à â
)
â ä
;
ä ã
public## 
static## 
	XDocument## 
GetComments##  +
(##+ ,
this##, 0
Assembly##1 9
assembly##: B
)##B C
{$$ 	
return%% 
_commentsCache%% !
.%%! "
GetOrAdd%%" *
(%%* +
assembly%%+ 3
,%%3 4
a%%5 6
=>%%7 9
{&& 
var'' 
path'' 
='' 
Path'' 
.''  
Combine''  '
(''' (
Path''( ,
.'', -
GetDirectoryName''- =
(''= >
a''> ?
.''? @
Location''@ H
)''H I
,''I J
Path''K O
.''O P'
GetFileNameWithoutExtension''P k
(''k l
a''l m
.''m n
Location''n v
)''v w
+''x y
$str	''z Ä
)
''Ä Å
;
''Å Ç
if(( 
((( 
File(( 
.(( 
Exists(( 
(((  
path((  $
)(($ %
)((% &
{)) 
return** 
	XDocument** $
.**$ %
Load**% )
(**) *
path*** .
)**. /
;**/ 0
}++ 
return,, 
null,, 
;,, 
}-- 
)-- 
;-- 
}.. 	
public55 
static55 
Comments55 
GetComments55 *
(55* +
this55+ /
Type550 4
type555 9
)559 :
{66 	
var77 
document77 
=77 
type77 
.77  
GetTypeInfo77  +
(77+ ,
)77, -
.77- .
Assembly77. 6
.776 7
GetComments777 B
(77B C
)77C D
;77D E
if88 
(88 
document88 
!=88 
null88  
)88  !
{99 
var:: 
node:: 
=:: 
document:: #
.::# $
XPathSelectElement::$ 6
(::6 7
$str::7 L
+::M N
type::O S
.::S T
FullName::T \
+::] ^
$str::_ d
)::d e
;::e f
if;; 
(;; 
node;; 
!=;; 
null;;  
);;  !
{<< 
return== 
new== 
Comments== '
(==' (
node==( ,
)==, -
;==- .
}>> 
}?? 
return@@ 
null@@ 
;@@ 
}AA 	
publicHH 
staticHH 
CommentsHH 
GetCommentsHH *
(HH* +
thisHH+ /
PropertyInfoHH0 <
propertyHH= E
)HHE F
{II 	
varJJ 
documentJJ 
=JJ 
propertyJJ #
.JJ# $
DeclaringTypeJJ$ 1
.JJ1 2
GetTypeInfoJJ2 =
(JJ= >
)JJ> ?
.JJ? @
AssemblyJJ@ H
.JJH I
GetCommentsJJI T
(JJT U
)JJU V
;JJV W
ifKK 
(KK 
documentKK 
!=KK 
nullKK  
)KK  !
{LL 
varMM 
nodeMM 
=MM 
documentMM #
.MM# $
XPathSelectElementMM$ 6
(MM6 7
$strMM7 L
+MMM N
propertyMMO W
.MMW X
DeclaringTypeMMX e
.MMe f
FullNameMMf n
+MMo p
$strMMq t
+MMu v
propertyMMw 
.	MM Ä
Name
MMÄ Ñ
+
MMÖ Ü
$str
MMá å
)
MMå ç
;
MMç é
ifNN 
(NN 
nodeNN 
!=NN 
nullNN  
)NN  !
{OO 
returnPP 
newPP 
CommentsPP '
(PP' (
nodePP( ,
)PP, -
;PP- .
}QQ 
}RR 
returnSS 
nullSS 
;SS 
}TT 	
public[[ 
static[[ 
string[[ 
GetPath[[ $
([[$ %
this[[% )
Type[[* .
type[[/ 3
)[[3 4
{\\ 	
return]] 
type]] 
.]] 
GetAllAttributes]] (
<]]( )
EndPointAttribute]]) :
>]]: ;
(]]; <
)]]< =
.]]= >
Select]]> D
(]]D E
e]]E F
=>]]G I
e]]J K
.]]K L
Path]]L P
)]]P Q
.]]Q R
FirstOrDefault]]R `
(]]` a
)]]a b
;]]b c
}^^ 	
publicff 
staticff 
Typeff 
[ff 
]ff 
GetRulesff %
(ff% &
thisff& *
Typeff+ /
typeff0 4
)ff4 5
{gg 	
returnhh 
typehh 
.hh 
GetTypeInfohh #
(hh# $
)hh$ %
.hh% &
Assemblyhh& .
.hh. /
SafelyGetTypeshh/ =
(hh= >
typeofhh> D
(hhD E
	IValidatehhE N
<hhN O
>hhO P
)hhP Q
.hhQ R
MakeGenericTypehhR a
(hha b
typehhb f
)hhf g
)hhg h
;hhh i
}ii 	
publicpp 
staticpp 
TimeSpanpp 
?pp 

GetTimeoutpp  *
(pp* +
thispp+ /
Typepp0 4
typepp5 9
)pp9 :
{qq 	
varrr 
	attributerr 
=rr 
typerr  
.rr  !
GetAllAttributesrr! 1
<rr1 2
EndPointAttributerr2 C
>rrC D
(rrD E
)rrE F
.rrF G
FirstOrDefaultrrG U
(rrU V
)rrV W
;rrW X
ifss 
(ss 
	attributess 
!=ss 
nullss !
&&ss" $
	attributess% .
.ss. /
Timeoutss/ 6
>ss7 8
$numss9 :
)ss: ;
{tt 
returnuu 
TimeSpanuu 
.uu  
FromMillisecondsuu  0
(uu0 1
	attributeuu1 :
.uu: ;
Timeoutuu; B
)uuB C
;uuC D
}vv 
returnww 
nullww 
;ww 
}xx 	
public 
static 
int 

GetVersion $
($ %
this% )
Type* .
type/ 3
)3 4
{
ÄÄ 	
return
ÅÅ 
type
ÅÅ 
.
ÅÅ 
GetAllAttributes
ÅÅ (
<
ÅÅ( )
EndPointAttribute
ÅÅ) :
>
ÅÅ: ;
(
ÅÅ; <
)
ÅÅ< =
.
ÅÅ= >
FirstOrDefault
ÅÅ> L
(
ÅÅL M
)
ÅÅM N
?
ÅÅN O
.
ÅÅO P
Version
ÅÅP W
??
ÅÅX Z
$num
ÅÅ[ \
;
ÅÅ\ ]
}
ÇÇ 	
public
ââ 
static
ââ 
bool
ââ 
	IsDynamic
ââ $
(
ââ$ %
this
ââ% )
Type
ââ* .
type
ââ/ 3
)
ââ3 4
{
ää 	
return
ãã 
typeof
ãã 
(
ãã (
IDynamicMetaObjectProvider
ãã 4
)
ãã4 5
.
ãã5 6
IsAssignableFrom
ãã6 F
(
ããF G
type
ããG K
)
ããK L
;
ããL M
}
åå 	
}
çç 
}éé ÷
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\BusinessRule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

abstract 
class 
BusinessRule &
<& '
TValue' -
>- .
:/ 0
IBusinessRule1 >
<> ?
TValue? E
>E F
,F G 
IUseExecutionContextH \
{ 
public 
IDomainFacade 
Domain #
{$ %
get& )
;) *
	protected+ 4
set5 8
;8 9
}: ;
public$$ 
Request$$ 
Request$$ 
=>$$ !
this$$" &
.$$& '
Context$$' .
?$$. /
.$$/ 0
Request$$0 7
;$$7 8
internal** 
ExecutionContext** !
Context**" )
{*** +
get**, /
;**/ 0
set**1 4
;**4 5
}**6 7
IEnumerable,, 
<,, 
ValidationError,, #
>,,# $
	IValidate,,% .
<,,. /
TValue,,/ 5
>,,5 6
.,,6 7
Validate,,7 ?
(,,? @
TValue,,@ F
instance,,G O
),,O P
{-- 	
var.. 
target.. 
=.. 
this.. 
... 
Validate.. &
(..& '
instance..' /
)../ 0
;..0 1
if// 
(// 
target// 
==// 
null// 
)// 
{00 
return11 

Enumerable11 !
.11! "
Empty11" '
<11' (
ValidationError11( 7
>117 8
(118 9
)119 :
;11: ;
}22 
return33 
new33 
[33 
]33 
{33 
target33  
}33  !
;33! "
}44 	
void66  
IUseExecutionContext66 !
.66! "

UseContext66" ,
(66, -
ExecutionContext66- =
context66> E
)66E F
{77 	
this88 
.88 
Context88 
=88 
context88 "
;88" #
}99 	
publicBB 
virtualBB 
ValidationErrorBB &
ValidateBB' /
(BB/ 0
TValueBB0 6
instanceBB7 ?
)BB? @
{CC 	
ArgumentDD 
.DD 
NotNullDD 
(DD 
instanceDD %
,DD% &
nameofDD' -
(DD- .
instanceDD. 6
)DD6 7
)DD7 8
;DD8 9
returnFF 
thisFF 
.FF 
ValidateAsyncFF %
(FF% &
instanceFF& .
)FF. /
.FF/ 0
ResultFF0 6
;FF6 7
}GG 	
publicPP 
virtualPP 
TaskPP 
<PP 
ValidationErrorPP +
>PP+ ,
ValidateAsyncPP- :
(PP: ;
TValuePP; A
instancePPB J
)PPJ K
{QQ 	
throwRR 
newRR #
NotImplementedExceptionRR -
(RR- .
)RR. /
;RR/ 0
}SS 	
}TT 
}UU ˛
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\IBusinessRule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !

Validation

! +
{ 
public 

	interface 
IBusinessRule "
<" #
in# %
TValue& ,
>, -
:. /
	IValidate0 9
<9 :
TValue: @
>@ A
{ 
} 
} ¯
IC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\IInputRule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !

Validation

! +
{ 
public 

	interface 

IInputRule 
<  
in  "
TValue# )
>) *
:+ ,
	IValidate- 6
<6 7
TValue7 =
>= >
{ 
} 
} û
PC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\IMessageValidator.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

	interface 
IMessageValidator &
{ 
Task 
< 
IEnumerable 
< 
ValidationError (
>( )
>) *
Validate+ 3
(3 4
object4 :
command; B
,B C
ExecutionContextD T
contextU \
)\ ]
;] ^
} 
} Í
HC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\InputRule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

abstract 
class 
	InputRule #
<# $
TCommand$ ,
>, -
:. /

IInputRule0 :
<: ;
TCommand; C
>C D
{ 
public 
abstract 
IEnumerable #
<# $
ValidationError$ 3
>3 4
Validate5 =
(= >
TCommand> F
instanceG O
)O P
;P Q
} 
} ˛
LC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\ISecurityRule.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 
Services

  
.

  !

Validation

! +
{ 
public 

	interface 
ISecurityRule "
<" #
in# %
TValue& ,
>, -
:. /
	IValidate0 9
<9 :
TValue: @
>@ A
{ 
} 
} w
OC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\MessageValidator.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

class 
MessageValidator !
<! "
TCommand" *
>* +
:, -
IMessageValidator. ?
where@ E
TCommandF N
:O P
classQ V
{ 
private 
readonly 
IEnumerable $
<$ %
	IValidate% .
<. /
TCommand/ 7
>7 8
>8 9
_rules: @
;@ A
public 
MessageValidator 
(  
IEnumerable  +
<+ ,
	IValidate, 5
<5 6
TCommand6 >
>> ?
>? @
rulesA F
)F G
{ 	
Argument   
.   
NotNull   
(   
rules   "
,  " #
nameof  $ *
(  * +
rules  + 0
)  0 1
)  1 2
;  2 3
_rules"" 
="" 
rules"" 
;"" 
}## 	
public,, 
Task,, 
<,, 
IEnumerable,, 
<,,  
ValidationError,,  /
>,,/ 0
>,,0 1
Validate,,2 :
(,,: ;
object,,; A
command,,B I
,,,I J
ExecutionContext,,K [
context,,\ c
),,c d
{-- 	
Argument.. 
... 
NotNull.. 
(.. 
command.. $
,..$ %
nameof..& ,
(.., -
command..- 4
)..4 5
)..5 6
;..6 7
var00 
instance00 
=00 
command00 "
as00# %
TCommand00& .
;00. /
if11 
(11 
instance11 
==11 
null11  
)11  !
{22 
instance33 
=33 
(33 
TCommand33 $
)33$ %
JsonConvert33& 1
.331 2
DeserializeObject332 C
(33C D
JsonConvert33D O
.33O P
SerializeObject33P _
(33_ `
command33` g
)33g h
,33h i
typeof33j p
(33p q
TCommand33q y
)33y z
)33z {
;33{ |
}44 
if66 
(66 
context66 
.66 
EndPoint66  
.66  !
Secure66! '
&&66( *
!66+ ,
(66, -
context66- 4
.664 5
Request665 <
.66< =
User66= A
?66A B
.66B C
Identity66C K
?66K L
.66L M
IsAuthenticated66M \
??66] _
false66` e
)66e f
)66f g
{77 
return88 
Task88 
.88 

FromResult88 &
(88& '
new88' *
[88* +
]88+ ,
{88- .
new88. 1
ValidationError882 A
(88A B
$str88B P
,88P Q
$str88R b
+88c d
context88e l
.88l m
Request88m t
.88t u
Path88u y
+88z {
$str	88| ô
,
88ô ö
ValidationType
88õ ©
.
88© ™
Security
88™ ≤
)
88≤ ≥
}
88≥ ¥
.
88¥ µ
AsEnumerable
88µ ¡
(
88¡ ¬
)
88¬ √
)
88√ ƒ
;
88ƒ ≈
}99 
var<< 
input<< 
=<< 
this<< 
.<< 
CheckInputRules<< ,
(<<, -
instance<<- 5
)<<5 6
.<<6 7
ToList<<7 =
(<<= >
)<<> ?
;<<? @
if== 
(== 
input== 
.== 
Any== 
(== 
)== 
)== 
{>> 
return?? 
Task?? 
.?? 

FromResult?? &
(??& '
input??' ,
.??, -
WithType??- 5
(??5 6
ValidationType??6 D
.??D E
Input??E J
)??J K
)??K L
;??L M
}@@ 
varBB 
securityBB 
=BB 
thisBB 
.BB  
CheckSecurityRulesBB  2
(BB2 3
instanceBB3 ;
,BB; <
contextBB= D
)BBD E
.BBE F
ToListBBF L
(BBL M
)BBM N
;BBN O
ifCC 
(CC 
securityCC 
.CC 
AnyCC 
(CC 
)CC 
)CC 
{DD 
returnEE 
TaskEE 
.EE 

FromResultEE &
(EE& '
securityEE' /
.EE/ 0
WithTypeEE0 8
(EE8 9
ValidationTypeEE9 G
.EEG H
SecurityEEH P
)EEP Q
)EEQ R
;EER S
}FF 
varHH 
businessHH 
=HH 
thisHH 
.HH  
CheckBusinessRulesHH  2
(HH2 3
instanceHH3 ;
,HH; <
contextHH= D
)HHD E
.HHE F
ToListHHF L
(HHL M
)HHM N
;HHN O
ifII 
(II 
businessII 
.II 
AnyII 
(II 
)II 
)II 
{JJ 
returnKK 
TaskKK 
.KK 

FromResultKK &
(KK& '
businessKK' /
.KK/ 0
WithTypeKK0 8
(KK8 9
ValidationTypeKK9 G
.KKG H
BusinessKKH P
)KKP Q
)KKQ R
;KKR S
}LL 
returnNN 
TaskNN 
.NN 

FromResultNN "
(NN" #

EnumerableNN# -
.NN- .
EmptyNN. 3
<NN3 4
ValidationErrorNN4 C
>NNC D
(NND E
)NNE F
)NNF G
;NNG H
}OO 	
	protectedXX 
virtualXX 
IEnumerableXX %
<XX% &
ValidationErrorXX& 5
>XX5 6
CheckBusinessRulesXX7 I
(XXI J
TCommandXXJ R
commandXXS Z
,XXZ [
ExecutionContextXX\ l
contextXXm t
)XXt u
{YY 	
foreachZZ 
(ZZ 
varZZ 
ruleZZ 
inZZ  
_rulesZZ! '
.ZZ' (
OfTypeZZ( .
<ZZ. /
IBusinessRuleZZ/ <
<ZZ< =
TCommandZZ= E
>ZZE F
>ZZF G
(ZZG H
)ZZH I
)ZZI J
{[[ 
if\\ 
(\\ 
rule\\ 
is\\  
IUseExecutionContext\\ 0
)\\0 1
{]] 
(^^ 
(^^  
IUseExecutionContext^^ *
)^^* +
rule^^, 0
)^^0 1
.^^1 2

UseContext^^2 <
(^^< =
context^^= D
)^^D E
;^^E F
}__ 
var`` 
result`` 
=`` 
rule`` !
.``! "
Validate``" *
(``* +
command``+ 2
)``2 3
.``3 4
ToList``4 :
(``: ;
)``; <
;``< =
ifaa 
(aa 
resultaa 
.aa 
Anyaa 
(aa 
)aa  
)aa  !
{bb 
returncc 
resultcc !
;cc! "
}dd 
}ee 
returnff 

Enumerableff 
.ff 
Emptyff #
<ff# $
ValidationErrorff$ 3
>ff3 4
(ff4 5
)ff5 6
;ff6 7
}gg 	
	protectedoo 
virtualoo 
IEnumerableoo %
<oo% &
ValidationErroroo& 5
>oo5 6
CheckInputRulesoo7 F
(ooF G
TCommandooG O
commandooP W
)ooW X
{pp 	
varqq 
targetqq 
=qq 
newqq 
Listqq !
<qq! "
ValidationErrorqq" 1
>qq1 2
(qq2 3
)qq3 4
;qq4 5
foreachrr 
(rr 
varrr 
propertyrr !
inrr" $
commandrr% ,
.rr, -
GetTyperr- 4
(rr4 5
)rr5 6
.rr6 7
GetPropertiesrr7 D
(rrD E
)rrE F
)rrF G
{ss 
targettt 
.tt 
AddRangett 
(tt  
thistt  $
.tt$ %

CheckRulestt% /
(tt/ 0
propertytt0 8
,tt8 9
(tt: ;
)tt; <
=>tt= ?
propertytt@ H
.ttH I
GetValuettI Q
(ttQ R
commandttR Y
)ttY Z
)ttZ [
)tt[ \
;tt\ ]
}uu 
ifvv 
(vv 
!vv 
targetvv 
.vv 
Anyvv 
(vv 
)vv 
)vv 
{ww 
foreachxx 
(xx 
varxx 
rulexx !
inxx" $
_rulesxx% +
.xx+ ,
OfTypexx, 2
<xx2 3

IInputRulexx3 =
<xx= >
TCommandxx> F
>xxF G
>xxG H
(xxH I
)xxI J
)xxJ K
{yy 
targetzz 
.zz 
AddRangezz #
(zz# $
rulezz$ (
.zz( )
Validatezz) 1
(zz1 2
commandzz2 9
)zz9 :
)zz: ;
;zz; <
}{{ 
}|| 
return}} 
target}} 
.}} 
AsEnumerable}} &
(}}& '
)}}' (
;}}( )
}~~ 	
	protected
áá 
virtual
áá 
IEnumerable
áá %
<
áá% &
ValidationError
áá& 5
>
áá5 6 
CheckSecurityRules
áá7 I
(
ááI J
TCommand
ááJ R
command
ááS Z
,
ááZ [
ExecutionContext
áá\ l
context
áám t
)
áát u
{
àà 	
foreach
ââ 
(
ââ 
var
ââ 
rule
ââ 
in
ââ  
_rules
ââ! '
.
ââ' (
OfType
ââ( .
<
ââ. /
ISecurityRule
ââ/ <
<
ââ< =
TCommand
ââ= E
>
ââE F
>
ââF G
(
ââG H
)
ââH I
)
ââI J
{
ää 
if
ãã 
(
ãã 
rule
ãã 
is
ãã "
IUseExecutionContext
ãã 0
)
ãã0 1
{
åå 
(
çç 
(
çç "
IUseExecutionContext
çç *
)
çç* +
rule
çç, 0
)
çç0 1
.
çç1 2

UseContext
çç2 <
(
çç< =
context
çç= D
)
ççD E
;
ççE F
}
éé 
var
èè 
result
èè 
=
èè 
rule
èè !
.
èè! "
Validate
èè" *
(
èè* +
command
èè+ 2
)
èè2 3
.
èè3 4
ToList
èè4 :
(
èè: ;
)
èè; <
;
èè< =
if
êê 
(
êê 
result
êê 
.
êê 
Any
êê 
(
êê 
)
êê  
)
êê  !
{
ëë 
return
íí 
result
íí !
;
íí! "
}
ìì 
}
îî 
return
ïï 

Enumerable
ïï 
.
ïï 
Empty
ïï #
<
ïï# $
ValidationError
ïï$ 3
>
ïï3 4
(
ïï4 5
)
ïï5 6
;
ïï6 7
}
ññ 	
private
òò 
IEnumerable
òò 
<
òò 
ValidationError
òò +
>
òò+ ,

CheckRules
òò- 7
(
òò7 8
PropertyInfo
òò8 D
property
òòE M
,
òòM N
Func
òòO S
<
òòS T
object
òòT Z
>
òòZ [
value
òò\ a
,
òòa b
string
òòc i
prefix
òòj p
=
òòq r
null
òòs w
)
òòw x
{
ôô 	
foreach
öö 
(
öö 
var
öö 
	attribute
öö "
in
öö# %
property
öö& .
.
öö. /!
GetCustomAttributes
öö/ B
<
ööB C!
ValidationAttribute
ööC V
>
ööV W
(
ööW X
)
ööX Y
)
ööY Z
{
õõ 
if
úú 
(
úú 
!
úú 
	attribute
úú 
.
úú 
IsValid
úú &
(
úú& '
value
úú' ,
(
úú, -
)
úú- .
)
úú. /
)
úú/ 0
{
ùù 
if
ûû 
(
ûû 
	attribute
ûû !
.
ûû! "
Code
ûû" &
==
ûû' )
null
ûû* .
)
ûû. /
{
üü 
	attribute
†† !
.
††! "
Code
††" &
=
††' (
$"
††) +
{
††+ ,
prefix
††, 2
}
††2 3
{
††3 4
property
††4 <
.
††< =
DeclaringType
††= J
.
††J K
Name
††K O
}
††O P
.
††P Q
{
††Q R
property
††R Z
.
††Z [
Name
††[ _
}
††_ `
.
††` a
{
††a b
	attribute
††b k
.
††k l
GetType
††l s
(
††s t
)
††t u
.
††u v
Name
††v z
.
††z {
Replace††{ Ç
(††Ç É
$str††É é
,††é è
$str††ê í
)††í ì
}††ì î
"††î ï
;††ï ñ
}
°° 
yield
¢¢ 
return
¢¢  
	attribute
¢¢! *
.
¢¢* + 
GetValidationError
¢¢+ =
(
¢¢= >
property
¢¢> F
)
¢¢F G
;
¢¢G H
}
££ 
}
§§ 
foreach
¶¶ 
(
¶¶ 
var
¶¶ 
item
¶¶ 
in
¶¶  
property
¶¶! )
.
¶¶) *
PropertyType
¶¶* 6
.
¶¶6 7
GetProperties
¶¶7 D
(
¶¶D E
)
¶¶E F
)
¶¶F G
{
ßß 
if
®® 
(
®® 
item
®® 
.
®® 
DeclaringType
®® &
==
®®' )
property
®®* 2
.
®®2 3
DeclaringType
®®3 @
)
®®@ A
{
©© 
continue
™™ 
;
™™ 
}
´´ 
foreach
¨¨ 
(
¨¨ 
var
¨¨ 
error
¨¨ "
in
¨¨# %
this
¨¨& *
.
¨¨* +

CheckRules
¨¨+ 5
(
¨¨5 6
item
¨¨6 :
,
¨¨: ;
(
¨¨< =
)
¨¨= >
=>
¨¨? A
{
≠≠ 
var
ÆÆ 
target
ÆÆ 
=
ÆÆ  
value
ÆÆ! &
(
ÆÆ& '
)
ÆÆ' (
;
ÆÆ( )
return
ØØ 
target
ØØ !
==
ØØ" $
null
ØØ% )
?
ØØ* +
null
ØØ, 0
:
ØØ1 2
item
ØØ3 7
.
ØØ7 8
GetValue
ØØ8 @
(
ØØ@ A
target
ØØA G
)
ØØG H
;
ØØH I
}
∞∞ 
,
∞∞ 
$"
∞∞ 
{
∞∞ 
prefix
∞∞ 
}
∞∞ 
{
∞∞ 
property
∞∞ &
.
∞∞& '
DeclaringType
∞∞' 4
.
∞∞4 5
Name
∞∞5 9
}
∞∞9 :
.
∞∞: ;
"
∞∞; <
)
∞∞< =
)
∞∞= >
{
±± 
yield
≤≤ 
return
≤≤  
error
≤≤! &
;
≤≤& '
}
≥≥ 
}
¥¥ 
}
µµ 	
}
∂∂ 
}∑∑ õ
KC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\SecurityRule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

abstract 
class 
SecurityRule &
<& '
TCommand' /
>/ 0
:1 2
ISecurityRule3 @
<@ A
TCommandA I
>I J
,J K 
IUseExecutionContextL `
{ 
private 
ExecutionContext  
_context! )
;) *
public 
Request 
Request 
=> !
_context" *
?* +
.+ ,
Request, 3
;3 4
IEnumerable 
< 
ValidationError #
># $
	IValidate% .
<. /
TCommand/ 7
>7 8
.8 9
Validate9 A
(A B
TCommandB J
instanceK S
)S T
{   	
var!! 
target!! 
=!! 
this!! 
.!! 
Validate!! &
(!!& '
instance!!' /
)!!/ 0
;!!0 1
if"" 
("" 
target"" 
=="" 
null"" 
)"" 
{## 
return$$ 

Enumerable$$ !
.$$! "
Empty$$" '
<$$' (
ValidationError$$( 7
>$$7 8
($$8 9
)$$9 :
;$$: ;
}%% 
return&& 
new&& 
[&& 
]&& 
{&& 
target&&  
}&&  !
;&&! "
}'' 	
void))  
IUseExecutionContext)) !
.))! "

UseContext))" ,
()), -
ExecutionContext))- =
context))> E
)))E F
{** 	
_context++ 
=++ 
context++ 
;++ 
},, 	
public55 
virtual55 
ValidationError55 &
Validate55' /
(55/ 0
TCommand550 8
instance559 A
)55A B
{66 	
Argument77 
.77 
NotNull77 
(77 
instance77 %
,77% &
nameof77' -
(77- .
instance77. 6
)776 7
)777 8
;778 9
return99 
this99 
.99 
ValidateAsync99 %
(99% &
instance99& .
)99. /
.99/ 0
Result990 6
;996 7
}:: 	
publicBB 
virtualBB 
TaskBB 
<BB 
ValidationErrorBB +
>BB+ ,
ValidateAsyncBB- :
(BB: ;
TCommandBB; C
instanceBBD L
)BBL M
{CC 	
throwDD 
newDD #
NotImplementedExceptionDD -
(DD- .
)DD. /
;DD/ 0
}EE 	
}FF 
}GG ì
SC:\Source\Stacks\Core\src\Slalom.Stacks\Services\Validation\ValidationExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Services  
.  !

Validation! +
{ 
public 

static 
class  
ValidationExtensions ,
{ 
public 
static 
IEnumerable !
<! "
ValidationError" 1
>1 2
WithType3 ;
(; <
this< @
IEnumerableA L
<L M
ValidationErrorM \
>\ ]
instance^ f
,f g
ValidationTypeh v
typew {
){ |
{ 	
Argument 
. 
NotNull 
( 
instance %
,% &
nameof' -
(- .
instance. 6
)6 7
)7 8
;8 9
var 
	instances 
= 
instance $
as% '
ValidationError( 7
[7 8
]8 9
??: <
instance= E
.E F
ToArrayF M
(M N
)N O
;O P
foreach!! 
(!! 
var!! 
item!! 
in!!  
	instances!!! *
.!!* +
Where!!+ 0
(!!0 1
e!!1 2
=>!!3 5
e!!6 7
.!!7 8
Type!!8 <
==!!= ?
ValidationType!!@ N
.!!N O
None!!O S
)!!S T
)!!T U
{"" 
item## 
.## 
WithType## 
(## 
type## "
)##" #
;### $
}$$ 
return&& 
	instances&& 
;&& 
}'' 	
}(( 
})) ¶à
0C:\Source\Stacks\Core\src\Slalom.Stacks\Stack.cs
	namespace 	
Slalom
 
. 
Stacks 
{ 
public 

class 
Stack 
: 
IDisposable $
{ 
public$$ 
Stack$$ 
($$ 
params$$ 
object$$ "
[$$" #
]$$# $
markers$$% ,
)$$, -
{%% 	
this&& 
.&& 
Include&& 
(&& 
this&& 
.&& 
GetType&& %
(&&% &
)&&& '
)&&' (
;&&( )
this'' 
.'' 
Include'' 
('' 
markers''  
)''  !
;''! "
var)) 
builder)) 
=)) 
new)) 
ContainerBuilder)) .
()). /
)))/ 0
;))0 1
builder++ 
.++ 
RegisterModule++ "
(++" #
new++# &
ConfigurationModule++' :
(++: ;
this++; ?
)++? @
)++@ A
;++A B
foreach-- 
(-- 
var-- 
module-- 
in--  "
this--# '
.--' (

Assemblies--( 2
.--2 3
SafelyGetTypes--3 A
<--A B
Module--B H
>--H I
(--I J
)--J K
.--K L
Where--L Q
(--Q R
e--R S
=>--T V
e--W X
.--X Y
GetAllAttributes--Y i
<--i j
AutoLoadAttribute--j {
>--{ |
(--| }
)--} ~
.--~ 
Any	-- Ç
(
--Ç É
)
--É Ñ
)
--Ñ Ö
)
--Ö Ü
{.. 
if// 
(// 
module// 
.// 
GetConstructors// *
(//* +
)//+ ,
.//, -
SingleOrDefault//- <
(//< =
)//= >
?//> ?
.//? @
GetParameters//@ M
(//M N
)//N O
.//O P
Length//P V
==//W Y
$num//Z [
)//[ \
{00 
builder11 
.11 
RegisterModule11 *
(11* +
(11+ ,
Module11, 2
)112 3
	Activator113 <
.11< =
CreateInstance11= K
(11K L
module11L R
)11R S
)11S T
;11T U
}22 
if33 
(33 
module33 
.33 
GetConstructors33 *
(33* +
)33+ ,
.33, -
SingleOrDefault33- <
(33< =
)33= >
?33> ?
.33? @
GetParameters33@ M
(33M N
)33N O
.33O P
SingleOrDefault33P _
(33_ `
)33` a
?33a b
.33b c
ParameterType33c p
==33q s
typeof33t z
(33z {
Stack	33{ Ä
)
33Ä Å
)
33Å Ç
{44 
builder55 
.55 
RegisterModule55 *
(55* +
(55+ ,
Module55, 2
)552 3
	Activator553 <
.55< =
CreateInstance55= K
(55K L
module55L R
,55R S
this55T X
)55X Y
)55Y Z
;55Z [
}66 
}77 
this99 
.99 
	Container99 
=99 
builder99 $
.99$ %
Build99% *
(99* +
)99+ ,
;99, -
}:: 	
public@@  
ObservableCollection@@ #
<@@# $
Assembly@@$ ,
>@@, -

Assemblies@@. 8
{@@9 :
get@@; >
;@@> ?
}@@@ A
=@@B C
new@@D G 
ObservableCollection@@H \
<@@\ ]
Assembly@@] e
>@@e f
(@@f g
)@@g h
;@@h i
publicFF 
IConfigurationFF 
ConfigurationFF +
=>FF, .
thisFF/ 3
.FF3 4
	ContainerFF4 =
.FF= >
ResolveFF> E
<FFE F
IConfigurationFFF T
>FFT U
(FFU V
)FFV W
;FFW X
publicKK 

IContainerKK 
	ContainerKK #
{KK$ %
getKK& )
;KK) *
}KK+ ,
publicQQ 
IDomainFacadeQQ 
DomainQQ #
=>QQ$ &
thisQQ' +
.QQ+ ,
	ContainerQQ, 5
.QQ5 6
ResolveQQ6 =
<QQ= >
IDomainFacadeQQ> K
>QQK L
(QQL M
)QQM N
;QQN O
publicWW 
ILoggerWW 
LoggerWW 
=>WW  
thisWW! %
.WW% &
	ContainerWW& /
.WW/ 0
ResolveWW0 7
<WW7 8
ILoggerWW8 ?
>WW? @
(WW@ A
)WWA B
;WWB C
public]] 
ISearchFacade]] 
Search]] #
=>]]$ &
this]]' +
.]]+ ,
	Container]], 5
.]]5 6
Resolve]]6 =
<]]= >
ISearchFacade]]> K
>]]K L
(]]L M
)]]M N
;]]N O
publiccc 
voidcc 
Includecc 
(cc 
paramscc "
objectcc# )
[cc) *
]cc* +
markerscc, 3
)cc3 4
{dd 	
ifee 
(ee 
!ee 
markersee 
?ee 
.ee 
Anyee 
(ee 
)ee 
??ee  "
trueee# '
)ee' (
{ff 
vargg 
listgg 
=gg 
newgg 
Listgg #
<gg# $
Assemblygg$ ,
>gg, -
(gg- .
)gg. /
;gg/ 0
varss 
dependenciesss  
=ss! "
DependencyContextss# 4
.ss4 5
Defaultss5 <
;ss< =
foreachtt 
(tt 
vartt 
compilationLibrarytt /
intt0 2
dependenciestt3 ?
.tt? @
RuntimeLibrariestt@ P
)ttP Q
{uu 
tryvv 
{ww 
ifxx 
(xx 
DiscoveryServicexx ,
.xx, -
Ignoresxx- 4
.xx4 5
Anyxx5 8
(xx8 9
exx9 :
=>xx; =
compilationLibraryxx> P
.xxP Q
NamexxQ U
.xxU V

StartsWithxxV `
(xx` a
exxa b
)xxb c
)xxc d
)xxd e
{yy 
continuezz $
;zz$ %
}{{ 
var}} 
assemblyName}} (
=}}) *
new}}+ .
AssemblyName}}/ ;
(}}; <
compilationLibrary}}< N
.}}N O
Name}}O S
)}}S T
;}}T U
var 
assembly $
=% &
Assembly' /
./ 0
Load0 4
(4 5
assemblyName5 A
)A B
;B C
list
ÅÅ 
.
ÅÅ 
Add
ÅÅ  
(
ÅÅ  !
assembly
ÅÅ! )
)
ÅÅ) *
;
ÅÅ* +
}
ÇÇ 
catch
ÉÉ 
{
ÑÑ 
}
ÖÖ 
}
ÜÜ 
foreach
àà 
(
àà 
var
àà 
source
àà #
in
àà$ &
list
àà' +
.
àà+ ,
Distinct
àà, 4
(
àà4 5
)
àà5 6
.
àà6 7
Except
àà7 =
(
àà= >
this
àà> B
.
ààB C

Assemblies
ààC M
)
ààM N
)
ààN O
{
ââ 
this
ää 
.
ää 

Assemblies
ää #
.
ää# $
Add
ää$ '
(
ää' (
source
ää( .
)
ää. /
;
ää/ 0
}
ãã 
}
åå 
else
çç 
{
éé 
var
èè 
current
èè 
=
èè 
markers
èè %
.
èè% &
Select
èè& ,
(
èè, -
e
èè- .
=>
èè/ 1
{
êê% &
var
ëë) ,
type
ëë- 1
=
ëë2 3
e
ëë4 5
as
ëë6 8
Type
ëë9 =
;
ëë= >
if
íí) +
(
íí, -
type
íí- 1
!=
íí2 4
null
íí5 9
)
íí9 :
{
ìì) *
return
îî- 3
type
îî4 8
.
îî8 9
GetTypeInfo
îî9 D
(
îîD E
)
îîE F
.
îîF G
Assembly
îîG O
;
îîO P
}
ïï) *
var
ññ) ,
assembly
ññ- 5
=
ññ6 7
e
ññ8 9
as
ññ: <
Assembly
ññ= E
;
ññE F
if
óó) +
(
óó, -
assembly
óó- 5
!=
óó6 8
null
óó9 =
)
óó= >
{
òò) *
return
ôô- 3
assembly
ôô4 <
;
ôô< =
}
öö) *
return
õõ) /
e
õõ0 1
.
õõ1 2
GetType
õõ2 9
(
õõ9 :
)
õõ: ;
.
õõ; <
GetTypeInfo
õõ< G
(
õõG H
)
õõH I
.
õõI J
Assembly
õõJ R
;
õõR S
}
úú% &
)
úú& '
.
ùù% &
Distinct
ùù& .
(
ùù. /
)
ùù/ 0
;
ùù0 1
foreach
ûû 
(
ûû 
var
ûû 
item
ûû !
in
ûû" $
current
ûû% ,
)
ûû, -
{
üü 
this
†† 
.
†† 

Assemblies
†† #
.
††# $
Add
††$ '
(
††' (
item
††( ,
)
††, -
;
††- .
}
°° 
}
¢¢ 
}
££ 	
public
´´ 
void
´´ 
Publish
´´ 
(
´´ 
string
´´ "
channel
´´# *
,
´´* +
string
´´, 2
message
´´3 :
)
´´: ;
{
¨¨ 	
this
≠≠ 
.
≠≠ 
	Container
≠≠ 
.
≠≠ 
Resolve
≠≠ "
<
≠≠" #
IMessageGateway
≠≠# 2
>
≠≠2 3
(
≠≠3 4
)
≠≠4 5
.
≠≠5 6
Publish
≠≠6 =
(
≠≠= >
channel
≠≠> E
,
≠≠E F
message
≠≠G N
)
≠≠N O
;
≠≠O P
}
ÆÆ 	
public
¥¥ 
void
¥¥ 
Publish
¥¥ 
(
¥¥ 
Event
¥¥ !
instance
¥¥" *
)
¥¥* +
{
µµ 	
this
∂∂ 
.
∂∂ 
	Container
∂∂ 
.
∂∂ 
Resolve
∂∂ "
<
∂∂" #
IMessageGateway
∂∂# 2
>
∂∂2 3
(
∂∂3 4
)
∂∂4 5
.
∂∂5 6
Publish
∂∂6 =
(
∂∂= >
instance
∂∂> F
)
∂∂F G
;
∂∂G H
}
∑∑ 	
public
øø 
Task
øø 
<
øø 
MessageResult
øø !
>
øø! "
Send
øø# '
(
øø' (
object
øø( .
message
øø/ 6
,
øø6 7
TimeSpan
øø8 @
?
øø@ A
timeout
øøB I
=
øøJ K
null
øøL P
)
øøP Q
{
¿¿ 	
return
¡¡ 
this
¡¡ 
.
¡¡ 
	Container
¡¡ !
.
¡¡! "
Resolve
¡¡" )
<
¡¡) *
IMessageGateway
¡¡* 9
>
¡¡9 :
(
¡¡: ;
)
¡¡; <
.
¡¡< =
Send
¡¡= A
(
¡¡A B
message
¡¡B I
,
¡¡I J
timeout
¡¡K R
:
¡¡R S
timeout
¡¡T [
)
¡¡[ \
;
¡¡\ ]
}
¬¬ 	
public
   
async
   
Task
   
<
   
MessageResult
   '
<
  ' (
T
  ( )
>
  ) *
>
  * +
Send
  , 0
<
  0 1
T
  1 2
>
  2 3
(
  3 4
string
  4 :
path
  ; ?
,
  ? @
TimeSpan
  A I
?
  I J
timeout
  K R
=
  S T
null
  U Y
)
  Y Z
{
ÀÀ 	
var
ÃÃ 
result
ÃÃ 
=
ÃÃ 
await
ÃÃ 
this
ÃÃ #
.
ÃÃ# $
	Container
ÃÃ$ -
.
ÃÃ- .
Resolve
ÃÃ. 5
<
ÃÃ5 6
IMessageGateway
ÃÃ6 E
>
ÃÃE F
(
ÃÃF G
)
ÃÃG H
.
ÃÃH I
Send
ÃÃI M
(
ÃÃM N
path
ÃÃN R
,
ÃÃR S
timeout
ÃÃT [
:
ÃÃ[ \
timeout
ÃÃ] d
)
ÃÃd e
;
ÃÃe f
return
ŒŒ 
new
ŒŒ 
MessageResult
ŒŒ $
<
ŒŒ$ %
T
ŒŒ% &
>
ŒŒ& '
(
ŒŒ' (
result
ŒŒ( .
)
ŒŒ. /
;
ŒŒ/ 0
}
œœ 	
public
◊◊ 
async
◊◊ 
Task
◊◊ 
<
◊◊ 
MessageResult
◊◊ '
<
◊◊' (
T
◊◊( )
>
◊◊) *
>
◊◊* +
Send
◊◊, 0
<
◊◊0 1
T
◊◊1 2
>
◊◊2 3
(
◊◊3 4
object
◊◊4 :
message
◊◊; B
,
◊◊B C
TimeSpan
◊◊D L
?
◊◊L M
timeout
◊◊N U
=
◊◊V W
null
◊◊X \
)
◊◊\ ]
{
ÿÿ 	
var
ŸŸ 
result
ŸŸ 
=
ŸŸ 
await
ŸŸ 
this
ŸŸ #
.
ŸŸ# $
	Container
ŸŸ$ -
.
ŸŸ- .
Resolve
ŸŸ. 5
<
ŸŸ5 6
IMessageGateway
ŸŸ6 E
>
ŸŸE F
(
ŸŸF G
)
ŸŸG H
.
ŸŸH I
Send
ŸŸI M
(
ŸŸM N
message
ŸŸN U
,
ŸŸU V
timeout
ŸŸW ^
:
ŸŸ^ _
timeout
ŸŸ` g
)
ŸŸg h
;
ŸŸh i
return
€€ 
new
€€ 
MessageResult
€€ $
<
€€$ %
T
€€% &
>
€€& '
(
€€' (
result
€€( .
)
€€. /
;
€€/ 0
}
‹‹ 	
public
ÂÂ 
Task
ÂÂ 
<
ÂÂ 
MessageResult
ÂÂ !
>
ÂÂ! "
Send
ÂÂ# '
(
ÂÂ' (
string
ÂÂ( .
path
ÂÂ/ 3
,
ÂÂ3 4
object
ÂÂ5 ;
message
ÂÂ< C
,
ÂÂC D
TimeSpan
ÂÂE M
?
ÂÂM N
timeout
ÂÂO V
=
ÂÂW X
null
ÂÂY ]
)
ÂÂ] ^
{
ÊÊ 	
return
ÁÁ 
this
ÁÁ 
.
ÁÁ 
	Container
ÁÁ !
.
ÁÁ! "
Resolve
ÁÁ" )
<
ÁÁ) *
IMessageGateway
ÁÁ* 9
>
ÁÁ9 :
(
ÁÁ: ;
)
ÁÁ; <
.
ÁÁ< =
Send
ÁÁ= A
(
ÁÁA B
path
ÁÁB F
,
ÁÁF G
message
ÁÁH O
,
ÁÁO P
timeout
ÁÁQ X
:
ÁÁX Y
timeout
ÁÁZ a
)
ÁÁa b
;
ÁÁb c
}
ËË 	
public
 
Task
 
<
 
MessageResult
 !
>
! "
Send
# '
(
' (
string
( .
path
/ 3
,
3 4
TimeSpan
5 =
?
= >
timeout
? F
=
G H
null
I M
)
M N
{
ÒÒ 	
return
ÚÚ 
this
ÚÚ 
.
ÚÚ 
	Container
ÚÚ !
.
ÚÚ! "
Resolve
ÚÚ" )
<
ÚÚ) *
IMessageGateway
ÚÚ* 9
>
ÚÚ9 :
(
ÚÚ: ;
)
ÚÚ; <
.
ÚÚ< =
Send
ÚÚ= A
(
ÚÚA B
path
ÚÚB F
,
ÚÚF G
null
ÚÚH L
,
ÚÚL M
timeout
ÚÚN U
)
ÚÚU V
;
ÚÚV W
}
ÛÛ 	
public
¸¸ 
Task
¸¸ 
<
¸¸ 
MessageResult
¸¸ !
>
¸¸! "
Send
¸¸# '
(
¸¸' (
string
¸¸( .
path
¸¸/ 3
,
¸¸3 4
string
¸¸5 ;
command
¸¸< C
,
¸¸C D
TimeSpan
¸¸E M
?
¸¸M N
timeout
¸¸O V
=
¸¸W X
null
¸¸Y ]
)
¸¸] ^
{
˝˝ 	
return
˛˛ 
this
˛˛ 
.
˛˛ 
	Container
˛˛ !
.
˛˛! "
Resolve
˛˛" )
<
˛˛) *
IMessageGateway
˛˛* 9
>
˛˛9 :
(
˛˛: ;
)
˛˛; <
.
˛˛< =
Send
˛˛= A
(
˛˛A B
path
˛˛B F
,
˛˛F G
command
˛˛H O
,
˛˛O P
timeout
˛˛Q X
:
˛˛X Y
timeout
˛˛Z a
)
˛˛a b
;
˛˛b c
}
ˇˇ 	
private
ÉÉ 
bool
ÉÉ 
	_disposed
ÉÉ 
;
ÉÉ 
public
àà 
void
àà 
Dispose
àà 
(
àà 
)
àà 
{
ââ 	
this
ää 
.
ää 
Dispose
ää 
(
ää 
true
ää 
)
ää 
;
ää 
GC
ãã 
.
ãã 
SuppressFinalize
ãã 
(
ãã  
this
ãã  $
)
ãã$ %
;
ãã% &
}
åå 	
~
ëë 	
Stack
ëë	 
(
ëë 
)
ëë 
{
íí 	
this
ìì 
.
ìì 
Dispose
ìì 
(
ìì 
false
ìì 
)
ìì 
;
ìì  
}
îî 	
	protected
öö 
virtual
öö 
void
öö 
Dispose
öö &
(
öö& '
bool
öö' +
	disposing
öö, 5
)
öö5 6
{
õõ 	
if
úú 
(
úú 
	_disposed
úú 
)
úú 
{
ùù 
return
ûû 
;
ûû 
}
üü 
if
°° 
(
°° 
	disposing
°° 
)
°° 
{
¢¢ 
this
•• 
.
•• 
	Container
•• 
.
•• 
Dispose
•• &
(
••& '
)
••' (
;
••( )
}
¶¶ 
	_disposed
´´ 
=
´´ 
true
´´ 
;
´´ 
}
¨¨ 	
}
ØØ 
}∞∞ ¶W
:C:\Source\Stacks\Core\src\Slalom.Stacks\StackExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
{ 
public 

static 
class 
StackExtensions '
{ 
public## 
static##  
IRegistrationBuilder## *
<##* +
TLimit##+ 1
,##1 2
TActivatorData##3 A
,##A B
TRegistrationStyle##C U
>##U V"
AllPropertiesAutowired##W m
<##m n
TLimit##n t
,##t u
TActivatorData	##v Ñ
,
##Ñ Ö 
TRegistrationStyle
##Ü ò
>
##ò ô
(
##ô ö
this$$  
IRegistrationBuilder$$ %
<$$% &
TLimit$$& ,
,$$, -
TActivatorData$$. <
,$$< =
TRegistrationStyle$$> P
>$$P Q
registration$$R ^
)$$^ _
{%% 	
return&& 
registration&& 
.&&  
OnActivated&&  +
(&&+ ,
args&&, 0
=>&&1 3
InjectProperties&&4 D
(&&D E
args&&E I
.&&I J
Context&&J Q
,&&Q R
args&&S W
.&&W X
Instance&&X `
,&&` a
true&&b f
)&&f g
)&&g h
;&&h i
}'' 	
public11 
static11  
IRegistrationBuilder11 *
<11* +
TLimit11+ 1
,111 2"
TScanningActivatorData113 I
,11I J
TRegistrationStyle11K ]
>11] ^"
AsBaseAndContractTypes11_ u
<11u v
TLimit11v |
,11| }#
TScanningActivatorData	11~ î
,
11î ï 
TRegistrationStyle
11ñ ®
>
11® ©
(
11© ™
this22  
IRegistrationBuilder22 %
<22% &
TLimit22& ,
,22, -"
TScanningActivatorData22. D
,22D E
TRegistrationStyle22F X
>22X Y
registration22Z f
)22f g
where22h m#
TScanningActivatorData	22n Ñ
:
22Ö Ü#
ScanningActivatorData
22á ú
{33 	
return44 
registration44 
.44  
As44  "
(44" #
instance44# +
=>44, .
instance44/ 7
.447 8#
GetBaseAndContractTypes448 O
(44O P
)44P Q
)44Q R
;44R S
}55 	
public== 
static== 
void== 
InjectProperties== +
(==+ ,
IComponentContext==, =
context==> E
,==E F
object==G M
instance==N V
,==V W
bool==X \
overrideSetValues==] n
)==n o
{>> 	
Argument?? 
.?? 
NotNull?? 
(?? 
context?? $
,??$ %
nameof??& ,
(??, -
context??- 4
)??4 5
)??5 6
;??6 7
Argument@@ 
.@@ 
NotNull@@ 
(@@ 
instance@@ %
,@@% &
nameof@@' -
(@@- .
instance@@. 6
)@@6 7
)@@7 8
;@@8 9
foreachBB 
(BB 
varBB 
propertyInfoBB %
inBB& (
instanceBB) 1
.BB1 2
GetTypeBB2 9
(BB9 :
)BB: ;
.BB; <
GetPropertiesBB< I
(BBI J
BindingFlagsBBJ V
.BBV W
InstanceBBW _
|BB` a
BindingFlagsBBb n
.BBn o
PublicBBo u
|BBv w
BindingFlags	BBx Ñ
.
BBÑ Ö
	NonPublic
BBÖ é
)
BBé è
)
BBè ê
{CC 
varDD 
propertyTypeDD  
=DD! "
propertyInfoDD# /
.DD/ 0
PropertyTypeDD0 <
;DD< =
ifFF 
(FF 
(FF 
!FF 
propertyTypeFF "
.FF" #
GetTypeInfoFF# .
(FF. /
)FF/ 0
.FF0 1
IsValueTypeFF1 <
||FF= ?
propertyTypeFF@ L
.FFL M
GetTypeInfoFFM X
(FFX Y
)FFY Z
.FFZ [
IsEnumFF[ a
)FFa b
&&FFc e
propertyInfoFFf r
.FFr s
GetIndexParameters	FFs Ö
(
FFÖ Ü
)
FFÜ á
.
FFá à
Length
FFà é
==
FFè ë
$num
FFí ì
&&
FFî ñ
context
FFó û
.
FFû ü
IsRegistered
FFü ´
(
FF´ ¨
propertyType
FF¨ ∏
)
FF∏ π
)
FFπ ∫
{GG 
varHH 
	accessorsHH !
=HH" #
propertyInfoHH$ 0
.HH0 1
GetAccessorsHH1 =
(HH= >
trueHH> B
)HHB C
;HHC D
ifII 
(II 
(II 
	accessorsII "
.II" #
LengthII# )
!=II* ,
$numII- .
||II/ 1
!JJ 
(JJ 
	accessorsJJ $
[JJ$ %
$numJJ% &
]JJ& '
.JJ' (

ReturnTypeJJ( 2
!=JJ3 5
typeofJJ6 <
(JJ< =
voidJJ= A
)JJA B
)JJB C
)JJC D
&&JJE G
(KK 
overrideSetValuesKK *
||KK+ -
	accessorsKK. 7
.KK7 8
LengthKK8 >
!=KK? A
$numKKB C
||KKD F
propertyInfoLL %
.LL% &
GetValueLL& .
(LL. /
instanceLL/ 7
,LL7 8
nullLL9 =
)LL= >
==LL? A
nullLLB F
)LLF G
)LLG H
{MM 
varNN 
objNN 
=NN  !
contextNN" )
.NN) *
ResolveNN* 1
(NN1 2
propertyTypeNN2 >
)NN> ?
;NN? @
propertyInfoOO $
.OO$ %
SetValueOO% -
(OO- .
instanceOO. 6
,OO6 7
objOO8 ;
,OO; <
nullOO= A
)OOA B
;OOB C
}PP 
}QQ 
}RR 
}SS 	
publicZZ 
staticZZ 
IEnumerableZZ !
<ZZ! "
TZZ" #
>ZZ# $

ResolveAllZZ% /
<ZZ/ 0
TZZ0 1
>ZZ1 2
(ZZ2 3
thisZZ3 7
IComponentContextZZ8 I
instanceZZJ R
)ZZR S
{[[ 	
Argument\\ 
.\\ 
NotNull\\ 
(\\ 
instance\\ %
,\\% &
nameof\\' -
(\\- .
instance\\. 6
)\\6 7
)\\7 8
;\\8 9
return^^ 
instance^^ 
.^^ 
Resolve^^ #
<^^# $
IEnumerable^^$ /
<^^/ 0
T^^0 1
>^^1 2
>^^2 3
(^^3 4
)^^4 5
;^^5 6
}__ 	
publicii 
staticii 
IEnumerableii !
<ii! "
objectii" (
>ii( )

ResolveAllii* 4
(ii4 5
thisii5 9
IComponentContextii: K
instanceiiL T
,iiT U
TypeiiV Z
typeii[ _
)ii_ `
{jj 	
Argumentkk 
.kk 
NotNullkk 
(kk 
instancekk %
,kk% &
nameofkk' -
(kk- .
instancekk. 6
)kk6 7
)kk7 8
;kk8 9
Argumentll 
.ll 
NotNullll 
(ll 
typell !
,ll! "
nameofll# )
(ll) *
typell* .
)ll. /
)ll/ 0
;ll0 1
varnn 
targetnn 
=nn 
(nn 
(nn 
IEnumerablenn &
<nn& '
objectnn' -
>nn- .
)nn. /
instancenn0 8
.nn8 9
Resolvenn9 @
(nn@ A
typeofnnA G
(nnG H
IEnumerablennH S
<nnS T
>nnT U
)nnU V
.nnV W
MakeGenericTypennW f
(nnf g
typenng k
)nnk l
)nnl m
)nnm n
.nnn o
ToListnno u
(nnu v
)nnv w
;nnw x
foreachpp 
(pp 
varpp 
itempp 
inpp  
targetpp! '
)pp' (
{qq 
instancerr 
.rr 
InjectPropertiesrr )
(rr) *
itemrr* .
)rr. /
;rr/ 0
}ss 
returnuu 
targetuu 
;uu 
}vv 	
public
ÄÄ 
static
ÄÄ 

IContainer
ÄÄ  
Update
ÄÄ! '
(
ÄÄ' (
this
ÄÄ( ,

IContainer
ÄÄ- 7
instance
ÄÄ8 @
,
ÄÄ@ A
Action
ÄÄB H
<
ÄÄH I
ContainerBuilder
ÄÄI Y
>
ÄÄY Z
configuration
ÄÄ[ h
)
ÄÄh i
{
ÅÅ 	
Argument
ÇÇ 
.
ÇÇ 
NotNull
ÇÇ 
(
ÇÇ 
instance
ÇÇ %
,
ÇÇ% &
nameof
ÇÇ' -
(
ÇÇ- .
instance
ÇÇ. 6
)
ÇÇ6 7
)
ÇÇ7 8
;
ÇÇ8 9
Argument
ÉÉ 
.
ÉÉ 
NotNull
ÉÉ 
(
ÉÉ 
configuration
ÉÉ *
,
ÉÉ* +
nameof
ÉÉ, 2
(
ÉÉ2 3
configuration
ÉÉ3 @
)
ÉÉ@ A
)
ÉÉA B
;
ÉÉB C
var
ÖÖ 
builder
ÖÖ 
=
ÖÖ 
new
ÖÖ 
ContainerBuilder
ÖÖ .
(
ÖÖ. /
)
ÖÖ/ 0
;
ÖÖ0 1
configuration
ÜÜ 
.
ÜÜ 
Invoke
ÜÜ  
(
ÜÜ  !
builder
ÜÜ! (
)
ÜÜ( )
;
ÜÜ) *
builder
áá 
.
áá 
Update
áá 
(
áá 
instance
áá #
.
áá# $
ComponentRegistry
áá$ 5
)
áá5 6
;
áá6 7
return
àà 
instance
àà 
;
àà 
}
ââ 	
public
ìì 
static
ìì 
Stack
ìì 
Use
ìì 
(
ìì  
this
ìì  $
Stack
ìì% *
instance
ìì+ 3
,
ìì3 4
Action
ìì5 ;
<
ìì; <
ContainerBuilder
ìì< L
>
ììL M
configuration
ììN [
)
ìì[ \
{
îî 	
Argument
ïï 
.
ïï 
NotNull
ïï 
(
ïï 
instance
ïï %
,
ïï% &
nameof
ïï' -
(
ïï- .
instance
ïï. 6
)
ïï6 7
)
ïï7 8
;
ïï8 9
Argument
ññ 
.
ññ 
NotNull
ññ 
(
ññ 
configuration
ññ *
,
ññ* +
nameof
ññ, 2
(
ññ2 3
configuration
ññ3 @
)
ññ@ A
)
ññA B
;
ññB C
var
òò 
builder
òò 
=
òò 
new
òò 
ContainerBuilder
òò .
(
òò. /
)
òò/ 0
;
òò0 1
configuration
ôô 
.
ôô 
Invoke
ôô  
(
ôô  !
builder
ôô! (
)
ôô( )
;
ôô) *
builder
öö 
.
öö 
Update
öö 
(
öö 
instance
öö #
.
öö# $
	Container
öö$ -
.
öö- .
ComponentRegistry
öö. ?
)
öö? @
;
öö@ A
return
õõ 
instance
õõ 
;
õõ 
}
úú 	
}
ùù 
}ûû àW
@C:\Source\Stacks\Core\src\Slalom.Stacks\Text\StringExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
Text 
{ 
public 

static 
class 
StringExtensions (
{ 
public!! 
static!! 
string!! 
Compress!! %
(!!% &
this!!& *
string!!+ 1
instance!!2 :
)!!: ;
{"" 	
var## 
bytes## 
=## 
Encoding##  
.##  !
UTF8##! %
.##% &
GetBytes##& .
(##. /
instance##/ 7
)##7 8
;##8 9
using%% 
(%% 
var%% 
inStream%% 
=%%  !
new%%" %
MemoryStream%%& 2
(%%2 3
bytes%%3 8
)%%8 9
)%%9 :
{&& 
using'' 
('' 
var'' 
	outStream'' $
=''% &
new''' *
MemoryStream''+ 7
(''7 8
)''8 9
)''9 :
{(( 
using)) 
()) 
var)) 
zip)) "
=))# $
new))% (

GZipStream))) 3
())3 4
	outStream))4 =
,))= >
CompressionMode))? N
.))N O
Compress))O W
)))W X
)))X Y
{** 
inStream++  
.++  !
CopyTo++! '
(++' (
zip++( +
)+++ ,
;++, -
},, 
return.. 
Convert.. "
..." #
ToBase64String..# 1
(..1 2
	outStream..2 ;
...; <
ToArray..< C
(..C D
)..D E
)..E F
;..F G
}// 
}00 
}11 	
public88 
static88 
void88 
OutputToFile88 '
(88' (
this88( ,
object88- 3
instance884 <
,88< =
string88> D
path88E I
)88I J
{99 	
File:: 
.:: 
WriteAllText:: 
(:: 
path:: "
,::" #
instance::$ ,
.::, -
ToJson::- 3
(::3 4
)::4 5
)::5 6
;::6 7
};; 	
publicAA 
staticAA 
voidAA 
OutputToJsonAA '
(AA' (
thisAA( ,
objectAA- 3
instanceAA4 <
)AA< =
{BB 	
ConsoleCC 
.CC 
	WriteLineCC 
(CC 
instanceCC &
.CC& '
ToJsonCC' -
(CC- .
)CC. /
)CC/ 0
;CC0 1
}DD 	
publicMM 
staticMM 
stringMM 
ResizeMM #
(MM# $
thisMM$ (
stringMM) /
textMM0 4
,MM4 5
intMM6 9
countMM: ?
,MM? @
charMMA E
padMMF I
)MMI J
{NN 	
ifOO 
(OO 
textOO 
==OO 
nullOO 
)OO 
{PP 
throwQQ 
newQQ !
ArgumentNullExceptionQQ /
(QQ/ 0
nameofQQ0 6
(QQ6 7
textQQ7 ;
)QQ; <
)QQ< =
;QQ= >
}RR 
returnTT 
newTT 
stringTT 
(TT 
textTT "
.TT" #
TakeTT# '
(TT' (
countTT( -
)TT- .
.TT. /
ToArrayTT/ 6
(TT6 7
)TT7 8
)TT8 9
.TT9 :
PadRightTT: B
(TTB C
countTTC H
,TTH I
padTTJ M
)TTM N
;TTN O
}UU 	
public\\ 
static\\ 
string\\ 
ToCamelCase\\ (
(\\( )
this\\) -
string\\. 4
instance\\5 =
)\\= >
{]] 	
if^^ 
(^^ 
instance^^ 
==^^ 
null^^  
)^^  !
{__ 
throw`` 
new`` !
ArgumentNullException`` /
(``/ 0
nameof``0 6
(``6 7
instance``7 ?
)``? @
)``@ A
;``A B
}aa 
returnbb 
instancebb 
.bb 
	Substringbb %
(bb% &
$numbb& '
,bb' (
$numbb) *
)bb* +
.bb+ ,
ToLowerInvariantbb, <
(bb< =
)bb= >
+bb? @
instancebbA I
.bbI J
	SubstringbbJ S
(bbS T
$numbbT U
)bbU V
;bbV W
}cc 	
publicll 
staticll 
stringll 
ToDelimitedll (
(ll( )
thisll) -
stringll. 4
instancell5 =
,ll= >
stringll? E
	delimiterllF O
)llO P
{mm 	
ifnn 
(nn 
instancenn 
==nn 
nullnn  
)nn  !
{oo 
throwpp 
newpp !
ArgumentNullExceptionpp /
(pp/ 0
nameofpp0 6
(pp6 7
instancepp7 ?
)pp? @
)pp@ A
;ppA B
}qq 
returnrr 
stringrr 
.rr 
Concatrr  
(rr  !
instancerr! )
.rr) *
Selectrr* 0
(rr0 1
(rr1 2
xrr2 3
,rr3 4
irr5 6
)rr6 7
=>rr8 :
irr; <
>rr= >
$numrr? @
&&rrA C
charrrD H
.rrH I
IsUpperrrI P
(rrP Q
xrrQ R
)rrR S
?rrT U
	delimiterrrV _
+rr` a
xrrb c
.rrc d
ToStringrrd l
(rrl m
)rrm n
:rro p
xrrq r
.rrr s
ToStringrrs {
(rr{ |
)rr| }
)rr} ~
)rr~ 
.	rr Ä
ToLowerInvariant
rrÄ ê
(
rrê ë
)
rrë í
;
rrí ì
}ss 	
publiczz 
staticzz 
stringzz 
ToJsonzz #
(zz# $
thiszz$ (
objectzz) /
instancezz0 8
)zz8 9
{{{ 	
return|| 
JsonConvert|| 
.|| 
SerializeObject|| .
(||. /
instance||/ 7
,||7 8(
DefaultSerializationSettings||9 U
.||U V
Instance||V ^
)||^ _
;||_ `
}}} 	
public
ÑÑ 
static
ÑÑ 
string
ÑÑ 
ToPascalCase
ÑÑ )
(
ÑÑ) *
this
ÑÑ* .
string
ÑÑ/ 5
instance
ÑÑ6 >
)
ÑÑ> ?
{
ÖÖ 	
if
ÜÜ 
(
ÜÜ 
instance
ÜÜ 
==
ÜÜ 
null
ÜÜ  
)
ÜÜ  !
{
áá 
throw
àà 
new
àà #
ArgumentNullException
àà /
(
àà/ 0
nameof
àà0 6
(
àà6 7
instance
àà7 ?
)
àà? @
)
àà@ A
;
ààA B
}
ââ 
return
ää 
instance
ää 
.
ää 
	Substring
ää %
(
ää% &
$num
ää& '
,
ää' (
$num
ää) *
)
ää* +
.
ää+ ,
ToUpperInvariant
ää, <
(
ää< =
)
ää= >
+
ää? @
instance
ääA I
.
ääI J
	Substring
ääJ S
(
ääS T
$num
ääT U
)
ääU V
;
ääV W
}
ãã 	
public
íí 
static
íí 
string
íí 

ToSentence
íí '
(
íí' (
this
íí( ,
string
íí- 3
instance
íí4 <
)
íí< =
{
ìì 	
return
îî 
instance
îî 
.
îî 
Replace
îî #
(
îî# $
$str
îî$ '
,
îî' (
$str
îî) ,
)
îî, -
;
îî- .
}
ïï 	
public
úú 
static
úú 
string
úú 
ToSnakeCase
úú (
(
úú( )
this
úú) -
string
úú. 4
instance
úú5 =
)
úú= >
{
ùù 	
return
ûû 
instance
ûû 
.
ûû 
ToDelimited
ûû '
(
ûû' (
$str
ûû( +
)
ûû+ ,
;
ûû, -
}
üü 	
public
¶¶ 
static
¶¶ 
string
¶¶ 
ToTitle
¶¶ $
(
¶¶$ %
this
¶¶% )
string
¶¶* 0
instance
¶¶1 9
)
¶¶9 :
{
ßß 	
instance
®® 
=
®® 
Regex
®® 
.
®® 
Replace
®® $
(
®®$ %
instance
®®% -
,
®®- .
$str
®®/ ;
,
®®; <
m
®®= >
=>
®®? A
$"
®®B D
{
®®D E
m
®®E F
.
®®F G
Value
®®G L
[
®®L M
$num
®®M N
]
®®N O
}
®®O P
{
®®Q R
char
®®R V
.
®®V W
ToUpper
®®W ^
(
®®^ _
m
®®_ `
.
®®` a
Value
®®a f
[
®®f g
$num
®®g h
]
®®h i
)
®®i j
}
®®j k
"
®®k l
)
®®l m
;
®®m n
return
´´ 
Regex
´´ 
.
´´ 
Replace
´´  
(
´´  !
instance
´´! )
,
´´) *
$str
´´+ A
,
´´A B
m
´´C D
=>
´´E G
m
´´H I
.
´´I J
Value
´´J O
.
´´O P
ToUpperInvariant
´´P `
(
´´` a
)
´´a b
)
´´b c
;
´´c d
}
ØØ 	
public
∂∂ 
static
∂∂ 
string
∂∂ 

Uncompress
∂∂ '
(
∂∂' (
this
∂∂( ,
string
∂∂- 3
instance
∂∂4 <
)
∂∂< =
{
∑∑ 	
var
∏∏ 
bytes
∏∏ 
=
∏∏ 
Convert
∏∏ 
.
∏∏  
FromBase64String
∏∏  0
(
∏∏0 1
instance
∏∏1 9
)
∏∏9 :
;
∏∏: ;
using
ππ 
(
ππ 
var
ππ 
inStream
ππ 
=
ππ  !
new
ππ" %
MemoryStream
ππ& 2
(
ππ2 3
bytes
ππ3 8
)
ππ8 9
)
ππ9 :
{
∫∫ 
using
ªª 
(
ªª 
var
ªª 
	outStream
ªª $
=
ªª% &
new
ªª' *
MemoryStream
ªª+ 7
(
ªª7 8
)
ªª8 9
)
ªª9 :
{
ºº 
using
ΩΩ 
(
ΩΩ 
var
ΩΩ 
zip
ΩΩ "
=
ΩΩ# $
new
ΩΩ% (

GZipStream
ΩΩ) 3
(
ΩΩ3 4
inStream
ΩΩ4 <
,
ΩΩ< =
CompressionMode
ΩΩ> M
.
ΩΩM N

Decompress
ΩΩN X
)
ΩΩX Y
)
ΩΩY Z
{
ææ 
zip
øø 
.
øø 
CopyTo
øø "
(
øø" #
	outStream
øø# ,
)
øø, -
;
øø- .
}
¿¿ 
return
¬¬ 
Encoding
¬¬ #
.
¬¬# $
UTF8
¬¬$ (
.
¬¬( )
	GetString
¬¬) 2
(
¬¬2 3
	outStream
¬¬3 <
.
¬¬< =
ToArray
¬¬= D
(
¬¬D E
)
¬¬E F
)
¬¬F G
;
¬¬G H
}
√√ 
}
ƒƒ 
}
≈≈ 	
}
∆∆ 
}«« ˙
JC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\INewIdFormatter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
	interface 
INewIdFormatter &
{ 
string 
Format 
( 
byte 
[ 
] 
bytes "
)" #
;# $
} 
} ’
GC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\INewIdParser.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
	interface 
INewIdParser #
{ 
NewId 
Parse 
( 
string 
text 
)  
;  !
} 
} ﬂ
MC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\IProcessIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
	interface 
IProcessIdProvider )
{ 
byte 
[ 
] 
GetProcessId 
( 
) 
; 
} 
} ¡
HC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\ITickProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
	interface 
ITickProvider $
{ 
long 
Ticks 
{ 
get 
; 
} 
} 
} Ä
LC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\IWorkerIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
	interface 
IWorkerIdProvider (
{ 
byte 
[ 
] 
GetWorkerId 
( 
int 
index $
)$ %
;% &
} 
} ∑“
@C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewId.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{		 
internal 
struct 
NewId 
: 

IEquatable 
< 
NewId 
> 
, 
IComparable 
< 
NewId 
> 
, 
IComparable 
, 
IFormattable 
{ 
public 
static 
readonly 
NewId $
Empty% *
=+ ,
new- 0
NewId1 6
(6 7
$num7 8
,8 9
$num: ;
,; <
$num= >
,> ?
$num@ A
)A B
;B C
static 
readonly 
INewIdFormatter '
_braceFormatter( 7
=8 9
new: =
DashedHexFormatter> P
(P Q
$charQ T
,T U
$charV Y
)Y Z
;Z [
static 
readonly 
INewIdFormatter '
_dashedHexFormatter( ;
=< =
new> A
DashedHexFormatterB T
(T U
)U V
;V W
static 
readonly 
INewIdFormatter '
_hexFormatter( 5
=6 7
new8 ;
HexFormatter< H
(H I
)I J
;J K
static 
readonly 
INewIdFormatter '
_parenFormatter( 7
=8 9
new: =
DashedHexFormatter> P
(P Q
$charQ T
,T U
$charV Y
)Y Z
;Z [
static 
NewIdGenerator 

_generator (
;( )
static 
ITickProvider 
_tickProvider *
;* +
static 
IWorkerIdProvider  
_workerIdProvider! 2
;2 3
static 
IProcessIdProvider !
_processIdProvider" 4
;4 5
readonly   
Int32   
_a   
;   
readonly!! 
Int32!! 
_b!! 
;!! 
readonly"" 
Int32"" 
_c"" 
;"" 
readonly## 
Int32## 
_d## 
;## 
public)) 
NewId)) 
()) 
byte)) 
[)) 
])) 
bytes)) !
)))! "
{** 	
if++ 
(++ 
bytes++ 
==++ 
null++ 
)++ 
throw,, 
new,, !
ArgumentNullException,, /
(,,/ 0
$str,,0 7
),,7 8
;,,8 9
if-- 
(-- 
bytes-- 
.-- 
Length-- 
!=-- 
$num--  "
)--" #
throw.. 
new.. 
ArgumentException.. +
(..+ ,
$str.., G
,..G H
$str..I P
)..P Q
;..Q R
FromByteArray00 
(00 
bytes00 
,00  
out00! $
_a00% '
,00' (
out00) ,
_b00- /
,00/ 0
out001 4
_c005 7
,007 8
out009 <
_d00= ?
)00? @
;00@ A
}11 	
public33 
NewId33 
(33 
string33 
value33 !
)33! "
{44 	
if55 
(55 
string55 
.55 
IsNullOrEmpty55 $
(55$ %
value55% *
)55* +
)55+ ,
throw66 
new66 
ArgumentException66 +
(66+ ,
$str66, G
,66G H
$str66I P
)66P Q
;66Q R
var88 
guid88 
=88 
new88 
Guid88 
(88  
value88  %
)88% &
;88& '
byte:: 
[:: 
]:: 
bytes:: 
=:: 
guid:: 
.::  
ToByteArray::  +
(::+ ,
)::, -
;::- .
FromByteArray<< 
(<< 
bytes<< 
,<<  
out<<! $
_a<<% '
,<<' (
out<<) ,
_b<<- /
,<</ 0
out<<1 4
_c<<5 7
,<<7 8
out<<9 <
_d<<= ?
)<<? @
;<<@ A
}== 	
public?? 
NewId?? 
(?? 
int?? 
a?? 
,?? 
int?? 
b??  !
,??! "
int??# &
c??' (
,??( )
int??* -
d??. /
)??/ 0
{@@ 	
_aAA 
=AA 
aAA 
;AA 
_bBB 
=BB 
bBB 
;BB 
_cCC 
=CC 
cCC 
;CC 
_dDD 
=DD 
dDD 
;DD 
}EE 	
publicGG 
NewIdGG 
(GG 
intGG 
aGG 
,GG 
shortGG !
bGG" #
,GG# $
shortGG% *
cGG+ ,
,GG, -
byteGG. 2
dGG3 4
,GG4 5
byteGG6 :
eGG; <
,GG< =
byteGG> B
fGGC D
,GGD E
byteGGF J
gGGK L
,GGL M
byteGGN R
hGGS T
,GGT U
byteGGV Z
iGG[ \
,GG\ ]
byteGG^ b
jGGc d
,GGd e
byteGGf j
kGGk l
)GGl m
{HH 	
_aII 
=II 
(II 
fII 
<<II 
$numII 
)II 
|II 
(II 
gII 
<<II  "
$numII# %
)II% &
|II' (
(II) *
hII* +
<<II, .
$numII/ 0
)II0 1
|II2 3
iII4 5
;II5 6
_bJJ 
=JJ 
(JJ 
jJJ 
<<JJ 
$numJJ 
)JJ 
|JJ 
(JJ 
kJJ 
<<JJ  "
$numJJ# %
)JJ% &
|JJ' (
(JJ) *
dJJ* +
<<JJ, .
$numJJ/ 0
)JJ0 1
|JJ2 3
eJJ4 5
;JJ5 6
_cLL 
=LL 
(LL 
cLL 
<<LL 
$numLL 
)LL 
|LL 
bLL 
;LL 
_dNN 
=NN 
aNN 
;NN 
}OO 	
staticQQ 
NewIdGeneratorQQ 
	GeneratorQQ '
{RR 	
getSS 
{SS 
returnSS 

_generatorSS #
??SS$ &
(SS' (

_generatorSS( 2
=SS3 4
newSS5 8
NewIdGeneratorSS9 G
(SSG H
TickProviderSSH T
,SST U
WorkerIdProviderSSV f
,SSf g
ProcessIdProviderSSh y
)SSy z
)SSz {
;SS{ |
}SS} ~
}TT 	
staticVV 
IWorkerIdProviderVV  
WorkerIdProviderVV! 1
{WW 	
getXX 
{XX 
returnXX 
_workerIdProviderXX *
??XX+ -
(XX. /
_workerIdProviderXX/ @
=XXA B
newXXC F(
BestPossibleWorkerIdProviderXXG c
(XXc d
)XXd e
)XXe f
;XXf g
}XXh i
}YY 	
static[[ 
IProcessIdProvider[[ !
ProcessIdProvider[[" 3
{\\ 	
get]] 
{]] 
return]] 
_processIdProvider]] +
;]]+ ,
}]]- .
}^^ 	
static`` 
ITickProvider`` 
TickProvider`` )
{aa 	
getbb 
{bb 
returnbb 
_tickProviderbb &
??bb' )
(bb* +
_tickProviderbb+ 8
=bb9 :
newbb; >!
StopwatchTickProviderbb? T
(bbT U
)bbU V
)bbV W
;bbW X
}bbY Z
}cc 	
publicee 
DateTimeee 
	Timestampee !
{ff 	
getgg 
{hh 
varii 
ticksii 
=ii 
(ii 
longii !
)ii! "
(ii" #
(ii# $
(ii$ %
ulongii% *
)ii* +
_aii+ -
)ii- .
<<ii/ 1
$numii2 4
|ii5 6
(ii7 8
uintii8 <
)ii< =
_bii= ?
)ii? @
;ii@ A
returnkk 
newkk 
DateTimekk #
(kk# $
tickskk$ )
,kk) *
DateTimeKindkk+ 7
.kk7 8
Utckk8 ;
)kk; <
;kk< =
}ll 
}mm 	
publicoo 
intoo 
	CompareTooo 
(oo 
objectoo #
objoo$ '
)oo' (
{pp 	
ifqq 
(qq 
objqq 
==qq 
nullqq 
)qq 
returnrr 
$numrr 
;rr 
ifss 
(ss 
!ss 
(ss 
objss 
isss 
NewIdss 
)ss 
)ss  
throwtt 
newtt 
ArgumentExceptiontt +
(tt+ ,
$strtt, F
)ttF G
;ttG H
returnvv 
thisvv 
.vv 
	CompareTovv !
(vv! "
(vv" #
NewIdvv# (
)vv( )
objvv) ,
)vv, -
;vv- .
}ww 	
publicyy 
intyy 
	CompareToyy 
(yy 
NewIdyy "
otheryy# (
)yy( )
{zz 	
if{{ 
({{ 
_a{{ 
!={{ 
other{{ 
.{{ 
_a{{ 
){{ 
return|| 
(|| 
(|| 
uint|| 
)|| 
_a||  
<||! "
(||# $
uint||$ (
)||( )
other||) .
.||. /
_a||/ 1
)||1 2
?||3 4
-||5 6
$num||6 7
:||8 9
$num||: ;
;||; <
if}} 
(}} 
_b}} 
!=}} 
other}} 
.}} 
_b}} 
)}} 
return~~ 
(~~ 
(~~ 
uint~~ 
)~~ 
_b~~  
<~~! "
(~~# $
uint~~$ (
)~~( )
other~~) .
.~~. /
_b~~/ 1
)~~1 2
?~~3 4
-~~5 6
$num~~6 7
:~~8 9
$num~~: ;
;~~; <
if 
( 
_c 
!= 
other 
. 
_c 
) 
return
ÄÄ 
(
ÄÄ 
(
ÄÄ 
uint
ÄÄ 
)
ÄÄ 
_c
ÄÄ  
<
ÄÄ! "
(
ÄÄ# $
uint
ÄÄ$ (
)
ÄÄ( )
other
ÄÄ) .
.
ÄÄ. /
_c
ÄÄ/ 1
)
ÄÄ1 2
?
ÄÄ3 4
-
ÄÄ5 6
$num
ÄÄ6 7
:
ÄÄ8 9
$num
ÄÄ: ;
;
ÄÄ; <
if
ÅÅ 
(
ÅÅ 
_d
ÅÅ 
!=
ÅÅ 
other
ÅÅ 
.
ÅÅ 
_d
ÅÅ 
)
ÅÅ 
return
ÇÇ 
(
ÇÇ 
(
ÇÇ 
uint
ÇÇ 
)
ÇÇ 
_d
ÇÇ  
<
ÇÇ! "
(
ÇÇ# $
uint
ÇÇ$ (
)
ÇÇ( )
other
ÇÇ) .
.
ÇÇ. /
_d
ÇÇ/ 1
)
ÇÇ1 2
?
ÇÇ3 4
-
ÇÇ5 6
$num
ÇÇ6 7
:
ÇÇ8 9
$num
ÇÇ: ;
;
ÇÇ; <
return
ÑÑ 
$num
ÑÑ 
;
ÑÑ 
}
ÖÖ 	
public
áá 
bool
áá 
Equals
áá 
(
áá 
NewId
áá  
other
áá! &
)
áá& '
{
àà 	
return
ââ 
other
ââ 
.
ââ 
_a
ââ 
==
ââ 
_a
ââ !
&&
ââ" $
other
ââ% *
.
ââ* +
_b
ââ+ -
==
ââ. 0
_b
ââ1 3
&&
ââ4 6
other
ââ7 <
.
ââ< =
_c
ââ= ?
==
ââ@ B
_c
ââC E
&&
ââF H
other
ââI N
.
ââN O
_d
ââO Q
==
ââR T
_d
ââU W
;
ââW X
}
ää 	
public
åå 
string
åå 
ToString
åå 
(
åå 
string
åå %
format
åå& ,
,
åå, -
IFormatProvider
åå. =
formatProvider
åå> L
)
ååL M
{
çç 	
if
éé 
(
éé 
string
éé 
.
éé 
IsNullOrEmpty
éé $
(
éé$ %
format
éé% +
)
éé+ ,
)
éé, -
format
èè 
=
èè 
$str
èè 
;
èè 
bool
ëë 

sequential
ëë 
=
ëë 
false
ëë #
;
ëë# $
if
íí 
(
íí 
format
íí 
.
íí 
Length
íí 
==
íí  
$num
íí! "
&&
íí# %
(
íí& '
format
íí' -
[
íí- .
$num
íí. /
]
íí/ 0
==
íí1 3
$char
íí4 7
||
íí8 :
format
íí; A
[
ííA B
$num
ííB C
]
ííC D
==
ííE G
$char
ííH K
)
ííK L
)
ííL M

sequential
ìì 
=
ìì 
true
ìì !
;
ìì! "
else
îî 
if
îî 
(
îî 
format
îî 
.
îî 
Length
îî "
!=
îî# %
$num
îî& '
)
îî' (
throw
ïï 
new
ïï 
FormatException
ïï )
(
ïï) *
$str
ïï* c
)
ïïc d
;
ïïd e
char
óó 
formatCh
óó 
=
óó 
format
óó "
[
óó" #
$num
óó# $
]
óó$ %
;
óó% &
byte
òò 
[
òò 
]
òò 
bytes
òò 
=
òò 

sequential
òò %
?
òò& '
this
òò( ,
.
òò, -*
GetSequentialFormatteryArray
òò- I
(
òòI J
)
òòJ K
:
òòL M
this
òòN R
.
òòR S 
GetFormatteryArray
òòS e
(
òòe f
)
òòf g
;
òòg h
if
öö 
(
öö 
formatCh
öö 
==
öö 
$char
öö 
||
öö  "
formatCh
öö# +
==
öö, .
$char
öö/ 2
)
öö2 3
return
õõ 
_braceFormatter
õõ &
.
õõ& '
Format
õõ' -
(
õõ- .
bytes
õõ. 3
)
õõ3 4
;
õõ4 5
if
úú 
(
úú 
formatCh
úú 
==
úú 
$char
úú 
||
úú  "
formatCh
úú# +
==
úú, .
$char
úú/ 2
)
úú2 3
return
ùù 
_parenFormatter
ùù &
.
ùù& '
Format
ùù' -
(
ùù- .
bytes
ùù. 3
)
ùù3 4
;
ùù4 5
if
ûû 
(
ûû 
formatCh
ûû 
==
ûû 
$char
ûû 
||
ûû  "
formatCh
ûû# +
==
ûû, .
$char
ûû/ 2
)
ûû2 3
return
üü !
_dashedHexFormatter
üü *
.
üü* +
Format
üü+ 1
(
üü1 2
bytes
üü2 7
)
üü7 8
;
üü8 9
if
†† 
(
†† 
formatCh
†† 
==
†† 
$char
†† 
||
††  "
formatCh
††# +
==
††, .
$char
††/ 2
)
††2 3
return
°° 
_hexFormatter
°° $
.
°°$ %
Format
°°% +
(
°°+ ,
bytes
°°, 1
)
°°1 2
;
°°2 3
throw
££ 
new
££ 
FormatException
££ %
(
££% &
$str
££& G
)
££G H
;
££H I
}
§§ 	
public
¶¶ 
string
¶¶ 
ToString
¶¶ 
(
¶¶ 
INewIdFormatter
¶¶ .
	formatter
¶¶/ 8
,
¶¶8 9
bool
¶¶: >

sequential
¶¶? I
=
¶¶J K
false
¶¶L Q
)
¶¶Q R
{
ßß 	
byte
®® 
[
®® 
]
®® 
bytes
®® 
=
®® 

sequential
®® %
?
®®& '
this
®®( ,
.
®®, -*
GetSequentialFormatteryArray
®®- I
(
®®I J
)
®®J K
:
®®L M
this
®®N R
.
®®R S 
GetFormatteryArray
®®S e
(
®®e f
)
®®f g
;
®®g h
return
™™ 
	formatter
™™ 
.
™™ 
Format
™™ #
(
™™# $
bytes
™™$ )
)
™™) *
;
™™* +
}
´´ 	
byte
≠≠ 
[
≠≠ 
]
≠≠  
GetFormatteryArray
≠≠ !
(
≠≠! "
)
≠≠" #
{
ÆÆ 	
var
ØØ 
bytes
ØØ 
=
ØØ 
new
ØØ 
byte
ØØ  
[
ØØ  !
$num
ØØ! #
]
ØØ# $
;
ØØ$ %
bytes
∞∞ 
[
∞∞ 
$num
∞∞ 
]
∞∞ 
=
∞∞ 
(
∞∞ 
byte
∞∞ 
)
∞∞ 
(
∞∞ 
_d
∞∞  
>>
∞∞! #
$num
∞∞$ &
)
∞∞& '
;
∞∞' (
bytes
±± 
[
±± 
$num
±± 
]
±± 
=
±± 
(
±± 
byte
±± 
)
±± 
(
±± 
_d
±±  
>>
±±! #
$num
±±$ &
)
±±& '
;
±±' (
bytes
≤≤ 
[
≤≤ 
$num
≤≤ 
]
≤≤ 
=
≤≤ 
(
≤≤ 
byte
≤≤ 
)
≤≤ 
(
≤≤ 
_d
≤≤  
>>
≤≤! #
$num
≤≤$ %
)
≤≤% &
;
≤≤& '
bytes
≥≥ 
[
≥≥ 
$num
≥≥ 
]
≥≥ 
=
≥≥ 
(
≥≥ 
byte
≥≥ 
)
≥≥ 
_d
≥≥ 
;
≥≥  
bytes
¥¥ 
[
¥¥ 
$num
¥¥ 
]
¥¥ 
=
¥¥ 
(
¥¥ 
byte
¥¥ 
)
¥¥ 
(
¥¥ 
_c
¥¥  
>>
¥¥! #
$num
¥¥$ %
)
¥¥% &
;
¥¥& '
bytes
µµ 
[
µµ 
$num
µµ 
]
µµ 
=
µµ 
(
µµ 
byte
µµ 
)
µµ 
_c
µµ 
;
µµ  
bytes
∂∂ 
[
∂∂ 
$num
∂∂ 
]
∂∂ 
=
∂∂ 
(
∂∂ 
byte
∂∂ 
)
∂∂ 
(
∂∂ 
_c
∂∂  
>>
∂∂! #
$num
∂∂$ &
)
∂∂& '
;
∂∂' (
bytes
∑∑ 
[
∑∑ 
$num
∑∑ 
]
∑∑ 
=
∑∑ 
(
∑∑ 
byte
∑∑ 
)
∑∑ 
(
∑∑ 
_c
∑∑  
>>
∑∑! #
$num
∑∑$ &
)
∑∑& '
;
∑∑' (
bytes
∏∏ 
[
∏∏ 
$num
∏∏ 
]
∏∏ 
=
∏∏ 
(
∏∏ 
byte
∏∏ 
)
∏∏ 
(
∏∏ 
_b
∏∏  
>>
∏∏! #
$num
∏∏$ %
)
∏∏% &
;
∏∏& '
bytes
ππ 
[
ππ 
$num
ππ 
]
ππ 
=
ππ 
(
ππ 
byte
ππ 
)
ππ 
_b
ππ 
;
ππ  
bytes
∫∫ 
[
∫∫ 
$num
∫∫ 
]
∫∫ 
=
∫∫ 
(
∫∫ 
byte
∫∫ 
)
∫∫ 
(
∫∫ 
_a
∫∫ !
>>
∫∫" $
$num
∫∫% '
)
∫∫' (
;
∫∫( )
bytes
ªª 
[
ªª 
$num
ªª 
]
ªª 
=
ªª 
(
ªª 
byte
ªª 
)
ªª 
(
ªª 
_a
ªª !
>>
ªª" $
$num
ªª% '
)
ªª' (
;
ªª( )
bytes
ºº 
[
ºº 
$num
ºº 
]
ºº 
=
ºº 
(
ºº 
byte
ºº 
)
ºº 
(
ºº 
_a
ºº !
>>
ºº" $
$num
ºº% &
)
ºº& '
;
ºº' (
bytes
ΩΩ 
[
ΩΩ 
$num
ΩΩ 
]
ΩΩ 
=
ΩΩ 
(
ΩΩ 
byte
ΩΩ 
)
ΩΩ 
_a
ΩΩ  
;
ΩΩ  !
bytes
ææ 
[
ææ 
$num
ææ 
]
ææ 
=
ææ 
(
ææ 
byte
ææ 
)
ææ 
(
ææ 
_b
ææ !
>>
ææ" $
$num
ææ% '
)
ææ' (
;
ææ( )
bytes
øø 
[
øø 
$num
øø 
]
øø 
=
øø 
(
øø 
byte
øø 
)
øø 
(
øø 
_b
øø !
>>
øø" $
$num
øø% '
)
øø' (
;
øø( )
return
¡¡ 
bytes
¡¡ 
;
¡¡ 
}
¬¬ 	
byte
ƒƒ 
[
ƒƒ 
]
ƒƒ *
GetSequentialFormatteryArray
ƒƒ +
(
ƒƒ+ ,
)
ƒƒ, -
{
≈≈ 	
var
∆∆ 
bytes
∆∆ 
=
∆∆ 
new
∆∆ 
byte
∆∆  
[
∆∆  !
$num
∆∆! #
]
∆∆# $
;
∆∆$ %
bytes
«« 
[
«« 
$num
«« 
]
«« 
=
«« 
(
«« 
byte
«« 
)
«« 
(
«« 
_a
««  
>>
««! #
$num
««$ &
)
««& '
;
««' (
bytes
»» 
[
»» 
$num
»» 
]
»» 
=
»» 
(
»» 
byte
»» 
)
»» 
(
»» 
_a
»»  
>>
»»! #
$num
»»$ &
)
»»& '
;
»»' (
bytes
…… 
[
…… 
$num
…… 
]
…… 
=
…… 
(
…… 
byte
…… 
)
…… 
(
…… 
_a
……  
>>
……! #
$num
……$ %
)
……% &
;
……& '
bytes
   
[
   
$num
   
]
   
=
   
(
   
byte
   
)
   
_a
   
;
    
bytes
ÀÀ 
[
ÀÀ 
$num
ÀÀ 
]
ÀÀ 
=
ÀÀ 
(
ÀÀ 
byte
ÀÀ 
)
ÀÀ 
(
ÀÀ 
_b
ÀÀ  
>>
ÀÀ! #
$num
ÀÀ$ &
)
ÀÀ& '
;
ÀÀ' (
bytes
ÃÃ 
[
ÃÃ 
$num
ÃÃ 
]
ÃÃ 
=
ÃÃ 
(
ÃÃ 
byte
ÃÃ 
)
ÃÃ 
(
ÃÃ 
_b
ÃÃ  
>>
ÃÃ! #
$num
ÃÃ$ &
)
ÃÃ& '
;
ÃÃ' (
bytes
ÕÕ 
[
ÕÕ 
$num
ÕÕ 
]
ÕÕ 
=
ÕÕ 
(
ÕÕ 
byte
ÕÕ 
)
ÕÕ 
(
ÕÕ 
_b
ÕÕ  
>>
ÕÕ! #
$num
ÕÕ$ %
)
ÕÕ% &
;
ÕÕ& '
bytes
ŒŒ 
[
ŒŒ 
$num
ŒŒ 
]
ŒŒ 
=
ŒŒ 
(
ŒŒ 
byte
ŒŒ 
)
ŒŒ 
_b
ŒŒ 
;
ŒŒ  
bytes
œœ 
[
œœ 
$num
œœ 
]
œœ 
=
œœ 
(
œœ 
byte
œœ 
)
œœ 
(
œœ 
_c
œœ  
>>
œœ! #
$num
œœ$ &
)
œœ& '
;
œœ' (
bytes
–– 
[
–– 
$num
–– 
]
–– 
=
–– 
(
–– 
byte
–– 
)
–– 
(
–– 
_c
––  
>>
––! #
$num
––$ &
)
––& '
;
––' (
bytes
—— 
[
—— 
$num
—— 
]
—— 
=
—— 
(
—— 
byte
—— 
)
—— 
(
—— 
_c
—— !
>>
——" $
$num
——% &
)
——& '
;
——' (
bytes
““ 
[
““ 
$num
““ 
]
““ 
=
““ 
(
““ 
byte
““ 
)
““ 
_c
““  
;
““  !
bytes
”” 
[
”” 
$num
”” 
]
”” 
=
”” 
(
”” 
byte
”” 
)
”” 
(
”” 
_d
”” !
>>
””" $
$num
””% '
)
””' (
;
””( )
bytes
‘‘ 
[
‘‘ 
$num
‘‘ 
]
‘‘ 
=
‘‘ 
(
‘‘ 
byte
‘‘ 
)
‘‘ 
(
‘‘ 
_d
‘‘ !
>>
‘‘" $
$num
‘‘% '
)
‘‘' (
;
‘‘( )
bytes
’’ 
[
’’ 
$num
’’ 
]
’’ 
=
’’ 
(
’’ 
byte
’’ 
)
’’ 
(
’’ 
_d
’’ !
>>
’’" $
$num
’’% &
)
’’& '
;
’’' (
bytes
÷÷ 
[
÷÷ 
$num
÷÷ 
]
÷÷ 
=
÷÷ 
(
÷÷ 
byte
÷÷ 
)
÷÷ 
_d
÷÷  
;
÷÷  !
return
ÿÿ 
bytes
ÿÿ 
;
ÿÿ 
}
ŸŸ 	
public
€€ 
Guid
€€ 
ToGuid
€€ 
(
€€ 
)
€€ 
{
‹‹ 	
int
›› 
a
›› 
=
›› 
_d
›› 
;
›› 
var
ﬁﬁ 
b
ﬁﬁ 
=
ﬁﬁ 
(
ﬁﬁ 
short
ﬁﬁ 
)
ﬁﬁ 
_c
ﬁﬁ 
;
ﬁﬁ 
var
ﬂﬂ 
c
ﬂﬂ 
=
ﬂﬂ 
(
ﬂﬂ 
short
ﬂﬂ 
)
ﬂﬂ 
(
ﬂﬂ 
_c
ﬂﬂ 
>>
ﬂﬂ !
$num
ﬂﬂ" $
)
ﬂﬂ$ %
;
ﬂﬂ% &
var
‡‡ 
d
‡‡ 
=
‡‡ 
(
‡‡ 
byte
‡‡ 
)
‡‡ 
(
‡‡ 
_b
‡‡ 
>>
‡‡  
$num
‡‡! "
)
‡‡" #
;
‡‡# $
var
·· 
e
·· 
=
·· 
(
·· 
byte
·· 
)
·· 
_b
·· 
;
·· 
var
‚‚ 
f
‚‚ 
=
‚‚ 
(
‚‚ 
byte
‚‚ 
)
‚‚ 
(
‚‚ 
_a
‚‚ 
>>
‚‚  
$num
‚‚! #
)
‚‚# $
;
‚‚$ %
var
„„ 
g
„„ 
=
„„ 
(
„„ 
byte
„„ 
)
„„ 
(
„„ 
_a
„„ 
>>
„„  
$num
„„! #
)
„„# $
;
„„$ %
var
‰‰ 
h
‰‰ 
=
‰‰ 
(
‰‰ 
byte
‰‰ 
)
‰‰ 
(
‰‰ 
_a
‰‰ 
>>
‰‰  
$num
‰‰! "
)
‰‰" #
;
‰‰# $
var
ÂÂ 
i
ÂÂ 
=
ÂÂ 
(
ÂÂ 
byte
ÂÂ 
)
ÂÂ 
_a
ÂÂ 
;
ÂÂ 
var
ÊÊ 
j
ÊÊ 
=
ÊÊ 
(
ÊÊ 
byte
ÊÊ 
)
ÊÊ 
(
ÊÊ 
_b
ÊÊ 
>>
ÊÊ  
$num
ÊÊ! #
)
ÊÊ# $
;
ÊÊ$ %
var
ÁÁ 
k
ÁÁ 
=
ÁÁ 
(
ÁÁ 
byte
ÁÁ 
)
ÁÁ 
(
ÁÁ 
_b
ÁÁ 
>>
ÁÁ  
$num
ÁÁ! #
)
ÁÁ# $
;
ÁÁ$ %
return
ÈÈ 
new
ÈÈ 
Guid
ÈÈ 
(
ÈÈ 
a
ÈÈ 
,
ÈÈ 
b
ÈÈ  
,
ÈÈ  !
c
ÈÈ" #
,
ÈÈ# $
d
ÈÈ% &
,
ÈÈ& '
e
ÈÈ( )
,
ÈÈ) *
f
ÈÈ+ ,
,
ÈÈ, -
g
ÈÈ. /
,
ÈÈ/ 0
h
ÈÈ1 2
,
ÈÈ2 3
i
ÈÈ4 5
,
ÈÈ5 6
j
ÈÈ7 8
,
ÈÈ8 9
k
ÈÈ: ;
)
ÈÈ; <
;
ÈÈ< =
}
ÍÍ 	
public
ÏÏ 
Guid
ÏÏ 
ToSequentialGuid
ÏÏ $
(
ÏÏ$ %
)
ÏÏ% &
{
ÌÌ 	
int
ÓÓ 
a
ÓÓ 
=
ÓÓ 
_a
ÓÓ 
;
ÓÓ 
var
ÔÔ 
b
ÔÔ 
=
ÔÔ 
(
ÔÔ 
short
ÔÔ 
)
ÔÔ 
(
ÔÔ 
_b
ÔÔ 
>>
ÔÔ !
$num
ÔÔ" $
)
ÔÔ$ %
;
ÔÔ% &
var
 
c
 
=
 
(
 
short
 
)
 
_b
 
;
 
var
ÒÒ 
d
ÒÒ 
=
ÒÒ 
(
ÒÒ 
byte
ÒÒ 
)
ÒÒ 
(
ÒÒ 
_c
ÒÒ 
>>
ÒÒ  
$num
ÒÒ! #
)
ÒÒ# $
;
ÒÒ$ %
var
ÚÚ 
e
ÚÚ 
=
ÚÚ 
(
ÚÚ 
byte
ÚÚ 
)
ÚÚ 
(
ÚÚ 
_c
ÚÚ 
>>
ÚÚ  
$num
ÚÚ! #
)
ÚÚ# $
;
ÚÚ$ %
var
ÛÛ 
f
ÛÛ 
=
ÛÛ 
(
ÛÛ 
byte
ÛÛ 
)
ÛÛ 
(
ÛÛ 
_c
ÛÛ 
>>
ÛÛ  
$num
ÛÛ! "
)
ÛÛ" #
;
ÛÛ# $
var
ÙÙ 
g
ÙÙ 
=
ÙÙ 
(
ÙÙ 
byte
ÙÙ 
)
ÙÙ 
(
ÙÙ 
_c
ÙÙ 
)
ÙÙ 
;
ÙÙ 
var
ıı 
h
ıı 
=
ıı 
(
ıı 
byte
ıı 
)
ıı 
(
ıı 
_d
ıı 
>>
ıı  
$num
ıı! #
)
ıı# $
;
ıı$ %
var
ˆˆ 
i
ˆˆ 
=
ˆˆ 
(
ˆˆ 
byte
ˆˆ 
)
ˆˆ 
(
ˆˆ 
_d
ˆˆ 
>>
ˆˆ  
$num
ˆˆ! #
)
ˆˆ# $
;
ˆˆ$ %
var
˜˜ 
j
˜˜ 
=
˜˜ 
(
˜˜ 
byte
˜˜ 
)
˜˜ 
(
˜˜ 
_d
˜˜ 
>>
˜˜  
$num
˜˜! "
)
˜˜" #
;
˜˜# $
var
¯¯ 
k
¯¯ 
=
¯¯ 
(
¯¯ 
byte
¯¯ 
)
¯¯ 
(
¯¯ 
_d
¯¯ 
)
¯¯ 
;
¯¯ 
return
˙˙ 
new
˙˙ 
Guid
˙˙ 
(
˙˙ 
a
˙˙ 
,
˙˙ 
b
˙˙  
,
˙˙  !
c
˙˙" #
,
˙˙# $
d
˙˙% &
,
˙˙& '
e
˙˙( )
,
˙˙) *
f
˙˙+ ,
,
˙˙, -
g
˙˙. /
,
˙˙/ 0
h
˙˙1 2
,
˙˙2 3
i
˙˙4 5
,
˙˙5 6
j
˙˙7 8
,
˙˙8 9
k
˙˙: ;
)
˙˙; <
;
˙˙< =
}
˚˚ 	
public
˝˝ 
byte
˝˝ 
[
˝˝ 
]
˝˝ 
ToByteArray
˝˝ !
(
˝˝! "
)
˝˝" #
{
˛˛ 	
var
ˇˇ 
bytes
ˇˇ 
=
ˇˇ 
new
ˇˇ 
byte
ˇˇ  
[
ˇˇ  !
$num
ˇˇ! #
]
ˇˇ# $
;
ˇˇ$ %
bytes
ÅÅ 
[
ÅÅ 
$num
ÅÅ 
]
ÅÅ 
=
ÅÅ 
(
ÅÅ 
byte
ÅÅ 
)
ÅÅ 
(
ÅÅ 
_d
ÅÅ  
)
ÅÅ  !
;
ÅÅ! "
bytes
ÇÇ 
[
ÇÇ 
$num
ÇÇ 
]
ÇÇ 
=
ÇÇ 
(
ÇÇ 
byte
ÇÇ 
)
ÇÇ 
(
ÇÇ 
_d
ÇÇ  
>>
ÇÇ! #
$num
ÇÇ$ %
)
ÇÇ% &
;
ÇÇ& '
bytes
ÉÉ 
[
ÉÉ 
$num
ÉÉ 
]
ÉÉ 
=
ÉÉ 
(
ÉÉ 
byte
ÉÉ 
)
ÉÉ 
(
ÉÉ 
_d
ÉÉ  
>>
ÉÉ! #
$num
ÉÉ$ &
)
ÉÉ& '
;
ÉÉ' (
bytes
ÑÑ 
[
ÑÑ 
$num
ÑÑ 
]
ÑÑ 
=
ÑÑ 
(
ÑÑ 
byte
ÑÑ 
)
ÑÑ 
(
ÑÑ 
_d
ÑÑ  
>>
ÑÑ! #
$num
ÑÑ$ &
)
ÑÑ& '
;
ÑÑ' (
bytes
ÖÖ 
[
ÖÖ 
$num
ÖÖ 
]
ÖÖ 
=
ÖÖ 
(
ÖÖ 
byte
ÖÖ 
)
ÖÖ 
(
ÖÖ 
_c
ÖÖ  
)
ÖÖ  !
;
ÖÖ! "
bytes
ÜÜ 
[
ÜÜ 
$num
ÜÜ 
]
ÜÜ 
=
ÜÜ 
(
ÜÜ 
byte
ÜÜ 
)
ÜÜ 
(
ÜÜ 
_c
ÜÜ  
>>
ÜÜ! #
$num
ÜÜ$ %
)
ÜÜ% &
;
ÜÜ& '
bytes
áá 
[
áá 
$num
áá 
]
áá 
=
áá 
(
áá 
byte
áá 
)
áá 
(
áá 
_c
áá  
>>
áá! #
$num
áá$ &
)
áá& '
;
áá' (
bytes
àà 
[
àà 
$num
àà 
]
àà 
=
àà 
(
àà 
byte
àà 
)
àà 
(
àà 
_c
àà  
>>
àà! #
$num
àà$ &
)
àà& '
;
àà' (
bytes
ââ 
[
ââ 
$num
ââ 
]
ââ 
=
ââ 
(
ââ 
byte
ââ 
)
ââ 
(
ââ 
_b
ââ  
>>
ââ! #
$num
ââ$ %
)
ââ% &
;
ââ& '
bytes
ää 
[
ää 
$num
ää 
]
ää 
=
ää 
(
ää 
byte
ää 
)
ää 
(
ää 
_b
ää  
)
ää  !
;
ää! "
bytes
ãã 
[
ãã 
$num
ãã 
]
ãã 
=
ãã 
(
ãã 
byte
ãã 
)
ãã 
(
ãã 
_a
ãã !
>>
ãã" $
$num
ãã% '
)
ãã' (
;
ãã( )
bytes
åå 
[
åå 
$num
åå 
]
åå 
=
åå 
(
åå 
byte
åå 
)
åå 
(
åå 
_a
åå !
>>
åå" $
$num
åå% '
)
åå' (
;
åå( )
bytes
çç 
[
çç 
$num
çç 
]
çç 
=
çç 
(
çç 
byte
çç 
)
çç 
(
çç 
_a
çç !
>>
çç" $
$num
çç% &
)
çç& '
;
çç' (
bytes
éé 
[
éé 
$num
éé 
]
éé 
=
éé 
(
éé 
byte
éé 
)
éé 
(
éé 
_a
éé !
)
éé! "
;
éé" #
bytes
èè 
[
èè 
$num
èè 
]
èè 
=
èè 
(
èè 
byte
èè 
)
èè 
(
èè 
_b
èè !
>>
èè" $
$num
èè% '
)
èè' (
;
èè( )
bytes
êê 
[
êê 
$num
êê 
]
êê 
=
êê 
(
êê 
byte
êê 
)
êê 
(
êê 
_b
êê !
>>
êê" $
$num
êê% '
)
êê' (
;
êê( )
return
íí 
bytes
íí 
;
íí 
}
ìì 	
public
ïï 
override
ïï 
string
ïï 
ToString
ïï '
(
ïï' (
)
ïï( )
{
ññ 	
return
óó 
this
óó 
.
óó 
ToString
óó  
(
óó  !
$str
óó! $
,
óó$ %
null
óó& *
)
óó* +
;
óó+ ,
}
òò 	
public
öö 
string
öö 
ToString
öö 
(
öö 
string
öö %
format
öö& ,
)
öö, -
{
õõ 	
return
úú 
this
úú 
.
úú 
ToString
úú  
(
úú  !
format
úú! '
,
úú' (
null
úú) -
)
úú- .
;
úú. /
}
ùù 	
public
üü 
override
üü 
bool
üü 
Equals
üü #
(
üü# $
object
üü$ *
obj
üü+ .
)
üü. /
{
†† 	
if
°° 
(
°° 
ReferenceEquals
°° 
(
°°  
null
°°  $
,
°°$ %
obj
°°& )
)
°°) *
)
°°* +
return
¢¢ 
false
¢¢ 
;
¢¢ 
if
££ 
(
££ 
obj
££ 
.
££ 
GetType
££ 
(
££ 
)
££ 
!=
££  
typeof
££! '
(
££' (
NewId
££( -
)
££- .
)
££. /
return
§§ 
false
§§ 
;
§§ 
return
•• 
this
•• 
.
•• 
Equals
•• 
(
•• 
(
••  
NewId
••  %
)
••% &
obj
••& )
)
••) *
;
••* +
}
¶¶ 	
public
®® 
override
®® 
int
®® 
GetHashCode
®® '
(
®®' (
)
®®( )
{
©© 	
	unchecked
™™ 
{
´´ 
int
¨¨ 
result
¨¨ 
=
¨¨ 
_a
¨¨ 
;
¨¨  
result
≠≠ 
=
≠≠ 
(
≠≠ 
result
≠≠  
*
≠≠! "
$num
≠≠# &
)
≠≠& '
^
≠≠( )
_b
≠≠* ,
;
≠≠, -
result
ÆÆ 
=
ÆÆ 
(
ÆÆ 
result
ÆÆ  
*
ÆÆ! "
$num
ÆÆ# &
)
ÆÆ& '
^
ÆÆ( )
_c
ÆÆ* ,
;
ÆÆ, -
result
ØØ 
=
ØØ 
(
ØØ 
result
ØØ  
*
ØØ! "
$num
ØØ# &
)
ØØ& '
^
ØØ( )
_d
ØØ* ,
;
ØØ, -
return
∞∞ 
result
∞∞ 
;
∞∞ 
}
±± 
}
≤≤ 	
public
¥¥ 
static
¥¥ 
bool
¥¥ 
operator
¥¥ #
==
¥¥$ &
(
¥¥& '
NewId
¥¥' ,
left
¥¥- 1
,
¥¥1 2
NewId
¥¥3 8
right
¥¥9 >
)
¥¥> ?
{
µµ 	
return
∂∂ 
left
∂∂ 
.
∂∂ 
_a
∂∂ 
==
∂∂ 
right
∂∂ #
.
∂∂# $
_a
∂∂$ &
&&
∂∂' )
left
∂∂* .
.
∂∂. /
_b
∂∂/ 1
==
∂∂2 4
right
∂∂5 :
.
∂∂: ;
_b
∂∂; =
&&
∂∂> @
left
∂∂A E
.
∂∂E F
_c
∂∂F H
==
∂∂I K
right
∂∂L Q
.
∂∂Q R
_c
∂∂R T
&&
∂∂U W
left
∂∂X \
.
∂∂\ ]
_d
∂∂] _
==
∂∂` b
right
∂∂c h
.
∂∂h i
_d
∂∂i k
;
∂∂k l
}
∑∑ 	
public
ππ 
static
ππ 
bool
ππ 
operator
ππ #
!=
ππ$ &
(
ππ& '
NewId
ππ' ,
left
ππ- 1
,
ππ1 2
NewId
ππ3 8
right
ππ9 >
)
ππ> ?
{
∫∫ 	
return
ªª 
!
ªª 
(
ªª 
left
ªª 
==
ªª 
right
ªª "
)
ªª" #
;
ªª# $
}
ºº 	
public
ææ 
static
ææ 
void
ææ 
SetGenerator
ææ '
(
ææ' (
NewIdGenerator
ææ( 6
	generator
ææ7 @
)
ææ@ A
{
øø 	

_generator
¿¿ 
=
¿¿ 
	generator
¿¿ "
;
¿¿" #
}
¡¡ 	
public
√√ 
static
√√ 
void
√√ !
SetWorkerIdProvider
√√ .
(
√√. /
IWorkerIdProvider
√√/ @
provider
√√A I
)
√√I J
{
ƒƒ 	
_workerIdProvider
≈≈ 
=
≈≈ 
provider
≈≈  (
;
≈≈( )
}
∆∆ 	
public
»» 
static
»» 
void
»» "
SetProcessIdProvider
»» /
(
»»/ 0 
IProcessIdProvider
»»0 B
provider
»»C K
)
»»K L
{
…… 	 
_processIdProvider
   
=
    
provider
  ! )
;
  ) *
}
ÀÀ 	
public
ÕÕ 
static
ÕÕ 
void
ÕÕ 
SetTickProvider
ÕÕ *
(
ÕÕ* +
ITickProvider
ÕÕ+ 8
provider
ÕÕ9 A
)
ÕÕA B
{
ŒŒ 	
_tickProvider
œœ 
=
œœ 
provider
œœ $
;
œœ$ %
}
–– 	
public
““ 
static
““ 
NewId
““ 
Next
““  
(
““  !
)
““! "
{
”” 	
return
‘‘ 
	Generator
‘‘ 
.
‘‘ 
Next
‘‘ !
(
‘‘! "
)
‘‘" #
;
‘‘# $
}
’’ 	
public
◊◊ 
static
◊◊ 
string
◊◊ 
NextId
◊◊ #
(
◊◊# $
)
◊◊$ %
{
ÿÿ 	
return
ŸŸ 
NextGuid
ŸŸ 
(
ŸŸ 
)
ŸŸ 
.
ŸŸ 
ToString
ŸŸ &
(
ŸŸ& '
$str
ŸŸ' *
)
ŸŸ* +
;
ŸŸ+ ,
}
⁄⁄ 	
public
‹‹ 
static
‹‹ 
Guid
‹‹ 
NextGuid
‹‹ #
(
‹‹# $
)
‹‹$ %
{
›› 	
return
ﬁﬁ 
	Generator
ﬁﬁ 
.
ﬁﬁ 
Next
ﬁﬁ !
(
ﬁﬁ! "
)
ﬁﬁ" #
.
ﬁﬁ# $
ToGuid
ﬁﬁ$ *
(
ﬁﬁ* +
)
ﬁﬁ+ ,
;
ﬁﬁ, -
}
ﬂﬂ 	
static
·· 
void
·· 
FromByteArray
·· !
(
··! "
byte
··" &
[
··& '
]
··' (
bytes
··) .
,
··. /
out
··0 3
Int32
··4 9
a
··: ;
,
··; <
out
··= @
Int32
··A F
b
··G H
,
··H I
out
··J M
Int32
··N S
c
··T U
,
··U V
out
··W Z
Int32
··[ `
d
··a b
)
··b c
{
‚‚ 	
a
„„ 
=
„„ 
bytes
„„ 
[
„„ 
$num
„„ 
]
„„ 
<<
„„ 
$num
„„ 
|
„„  !
bytes
„„" '
[
„„' (
$num
„„( *
]
„„* +
<<
„„, .
$num
„„/ 1
|
„„2 3
bytes
„„4 9
[
„„9 :
$num
„„: <
]
„„< =
<<
„„> @
$num
„„A B
|
„„C D
bytes
„„E J
[
„„J K
$num
„„K M
]
„„M N
;
„„N O
b
‰‰ 
=
‰‰ 
bytes
‰‰ 
[
‰‰ 
$num
‰‰ 
]
‰‰ 
<<
‰‰ 
$num
‰‰ 
|
‰‰  !
bytes
‰‰" '
[
‰‰' (
$num
‰‰( *
]
‰‰* +
<<
‰‰, .
$num
‰‰/ 1
|
‰‰2 3
bytes
‰‰4 9
[
‰‰9 :
$num
‰‰: ;
]
‰‰; <
<<
‰‰= ?
$num
‰‰@ A
|
‰‰B C
bytes
‰‰D I
[
‰‰I J
$num
‰‰J K
]
‰‰K L
;
‰‰L M
c
ÂÂ 
=
ÂÂ 
bytes
ÂÂ 
[
ÂÂ 
$num
ÂÂ 
]
ÂÂ 
<<
ÂÂ 
$num
ÂÂ 
|
ÂÂ  
bytes
ÂÂ! &
[
ÂÂ& '
$num
ÂÂ' (
]
ÂÂ( )
<<
ÂÂ* ,
$num
ÂÂ- /
|
ÂÂ0 1
bytes
ÂÂ2 7
[
ÂÂ7 8
$num
ÂÂ8 9
]
ÂÂ9 :
<<
ÂÂ; =
$num
ÂÂ> ?
|
ÂÂ@ A
bytes
ÂÂB G
[
ÂÂG H
$num
ÂÂH I
]
ÂÂI J
;
ÂÂJ K
d
ÊÊ 
=
ÊÊ 
bytes
ÊÊ 
[
ÊÊ 
$num
ÊÊ 
]
ÊÊ 
<<
ÊÊ 
$num
ÊÊ 
|
ÊÊ  
bytes
ÊÊ! &
[
ÊÊ& '
$num
ÊÊ' (
]
ÊÊ( )
<<
ÊÊ* ,
$num
ÊÊ- /
|
ÊÊ0 1
bytes
ÊÊ2 7
[
ÊÊ7 8
$num
ÊÊ8 9
]
ÊÊ9 :
<<
ÊÊ; =
$num
ÊÊ> ?
|
ÊÊ@ A
bytes
ÊÊB G
[
ÊÊG H
$num
ÊÊH I
]
ÊÊI J
;
ÊÊJ K
}
ÁÁ 	
}
ËË 
}ÈÈ Ò
JC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdExtensions.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
static 
class 
NewIdExtensions )
{ 
public 
static 
NewId 
ToNewId #
(# $
this$ (
Guid) -
guid. 2
)2 3
{ 	
return		 
new		 
NewId		 
(		 
guid		 !
.		! "
ToByteArray		" -
(		- .
)		. /
)		/ 0
;		0 1
}

 	
} 
} Ê+
ZC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdFormatters\Base32Formatter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdFormatters( 7
{ 
internal 
class 
Base32Formatter "
:# $
INewIdFormatter 
{ 
const 
string 
LowerCaseChars #
=$ %
$str& H
;H I
const		 
string		 
UpperCaseChars		 #
=		$ %
$str		& H
;		H I
readonly 
string 
_chars 
; 
public 
Base32Formatter 
( 
bool #
	upperCase$ -
=. /
false0 5
)5 6
{ 	
_chars 
= 
	upperCase 
?  
UpperCaseChars! /
:0 1
LowerCaseChars2 @
;@ A
} 	
public 
Base32Formatter 
( 
string %
chars& +
)+ ,
{ 	
if 
( 
chars 
. 
Length 
!= 
$num  "
)" #
throw 
new 
ArgumentException +
(+ ,
$str, `
)` a
;a b
_chars 
= 
chars 
; 
} 	
public 
string 
Format 
( 
byte !
[! "
]" #
bytes$ )
)) *
{ 	
var 
result 
= 
new 
char !
[! "
$num" $
]$ %
;% &
int 
offset 
= 
$num 
; 
for 
( 
int 
i 
= 
$num 
; 
i 
< 
$num  !
;! "
i# $
++$ &
)& '
{   
int!! 
indexed!! 
=!! 
i!! 
*!!  !
$num!!" #
;!!# $
long"" 
number"" 
="" 
bytes"" #
[""# $
indexed""$ +
]""+ ,
<<""- /
$num""0 2
|""3 4
bytes""5 :
["": ;
indexed""; B
+""C D
$num""E F
]""F G
<<""H J
$num""K L
|""M N
bytes""O T
[""T U
indexed""U \
+""] ^
$num""_ `
]""` a
>>""b d
$num""e f
;""f g
ConvertLongToBase32## #
(### $
result##$ *
,##* +
offset##, 2
,##2 3
number##4 :
,##: ;
$num##< =
,##= >
_chars##? E
)##E F
;##F G
offset%% 
+=%% 
$num%% 
;%% 
number'' 
='' 
('' 
bytes'' 
[''  
indexed''  '
+''( )
$num''* +
]''+ ,
&''- .
$num''/ 2
)''2 3
<<''4 6
$num''7 9
|'': ;
bytes''< A
[''A B
indexed''B I
+''J K
$num''L M
]''M N
<<''O Q
$num''R S
|''T U
bytes''V [
[''[ \
indexed''\ c
+''d e
$num''f g
]''g h
;''h i
ConvertLongToBase32(( #
(((# $
result(($ *
,((* +
offset((, 2
,((2 3
number((4 :
,((: ;
$num((< =
,((= >
_chars((? E
)((E F
;((F G
offset** 
+=** 
$num** 
;** 
}++ 
ConvertLongToBase32-- 
(--  
result--  &
,--& '
offset--( .
,--. /
bytes--0 5
[--5 6
$num--6 8
]--8 9
,--9 :
$num--; <
,--< =
_chars--> D
)--D E
;--E F
return// 
new// 
string// 
(// 
result// $
,//$ %
$num//& '
,//' (
$num//) +
)//+ ,
;//, -
}00 	
static22 
void22 
ConvertLongToBase3222 '
(22' (
char22( ,
[22, -
]22- .
buffer22/ 5
,225 6
int227 :
offset22; A
,22A B
long22C G
value22H M
,22M N
int22O R
count22S X
,22X Y
string22Z `
chars22a f
)22f g
{33 	
for44 
(44 
int44 
i44 
=44 
count44 
-44  
$num44! "
;44" #
i44$ %
>=44& (
$num44) *
;44* +
i44, -
--44- /
)44/ 0
{55 
var66 
index66 
=66 
(66 
int66  
)66  !
(66! "
value66" '
%66( )
$num66* ,
)66, -
;66- .
buffer77 
[77 
offset77 
+77 
i77  !
]77! "
=77# $
chars77% *
[77* +
index77+ 0
]770 1
;771 2
value88 
/=88 
$num88 
;88 
}99 
}:: 	
};; 
}<< ±?
]C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdFormatters\DashedHexFormatter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdFormatters( 7
{ 
internal 
class 
DashedHexFormatter %
:& '
INewIdFormatter 
{ 
readonly 
int 
_alpha 
; 
readonly 
int 
_length 
; 
readonly 
char 
_prefix 
; 
readonly		 
char		 
_suffix		 
;		 
public 
DashedHexFormatter !
(! "
char" &
prefix' -
=. /
$char0 4
,4 5
char6 :
suffix; A
=B C
$charD H
,H I
boolJ N
	upperCaseO X
=Y Z
false[ `
)` a
{ 	
if 
( 
prefix 
== 
$char 
|| !
suffix" (
==) +
$char, 0
)0 1
_length 
= 
$num 
; 
else 
{ 
_prefix 
= 
prefix  
;  !
_suffix 
= 
suffix  
;  !
_length 
= 
$num 
; 
} 
_alpha 
= 
	upperCase 
?  
$char! $
:% &
$char' *
;* +
} 	
public 
string 
Format 
( 
byte !
[! "
]" #
bytes$ )
)) *
{ 	
var 
result 
= 
new 
char !
[! "
_length" )
]) *
;* +
int 
i 
= 
$num 
; 
int 
offset 
= 
$num 
; 
if 
( 
_prefix 
!= 
$char 
)  
result   
[   
offset   
++   
]    
=  ! "
_prefix  # *
;  * +
for!! 
(!! 
;!! 
i!! 
<!! 
$num!! 
;!! 
i!! 
++!! 
)!! 
{"" 
int## 
value## 
=## 
bytes## !
[##! "
i##" #
]### $
;##$ %
result$$ 
[$$ 
offset$$ 
++$$ 
]$$  
=$$! "
	HexToChar$$# ,
($$, -
value$$- 2
>>$$3 5
$num$$6 7
,$$7 8
_alpha$$9 ?
)$$? @
;$$@ A
result%% 
[%% 
offset%% 
++%% 
]%%  
=%%! "
	HexToChar%%# ,
(%%, -
value%%- 2
,%%2 3
_alpha%%4 :
)%%: ;
;%%; <
}&& 
result'' 
['' 
offset'' 
++'' 
]'' 
='' 
$char'' "
;''" #
for(( 
((( 
;(( 
i(( 
<(( 
$num(( 
;(( 
i(( 
++(( 
)(( 
{)) 
int** 
value** 
=** 
bytes** !
[**! "
i**" #
]**# $
;**$ %
result++ 
[++ 
offset++ 
++++ 
]++  
=++! "
	HexToChar++# ,
(++, -
value++- 2
>>++3 5
$num++6 7
,++7 8
_alpha++9 ?
)++? @
;++@ A
result,, 
[,, 
offset,, 
++,, 
],,  
=,,! "
	HexToChar,,# ,
(,,, -
value,,- 2
,,,2 3
_alpha,,4 :
),,: ;
;,,; <
}-- 
result.. 
[.. 
offset.. 
++.. 
].. 
=.. 
$char.. "
;.." #
for// 
(// 
;// 
i// 
<// 
$num// 
;// 
i// 
++// 
)// 
{00 
int11 
value11 
=11 
bytes11 !
[11! "
i11" #
]11# $
;11$ %
result22 
[22 
offset22 
++22 
]22  
=22! "
	HexToChar22# ,
(22, -
value22- 2
>>223 5
$num226 7
,227 8
_alpha229 ?
)22? @
;22@ A
result33 
[33 
offset33 
++33 
]33  
=33! "
	HexToChar33# ,
(33, -
value33- 2
,332 3
_alpha334 :
)33: ;
;33; <
}44 
result55 
[55 
offset55 
++55 
]55 
=55 
$char55 "
;55" #
for66 
(66 
;66 
i66 
<66 
$num66 
;66 
i66 
++66 
)66 
{77 
int88 
value88 
=88 
bytes88 !
[88! "
i88" #
]88# $
;88$ %
result99 
[99 
offset99 
++99 
]99  
=99! "
	HexToChar99# ,
(99, -
value99- 2
>>993 5
$num996 7
,997 8
_alpha999 ?
)99? @
;99@ A
result:: 
[:: 
offset:: 
++:: 
]::  
=::! "
	HexToChar::# ,
(::, -
value::- 2
,::2 3
_alpha::4 :
)::: ;
;::; <
};; 
result<< 
[<< 
offset<< 
++<< 
]<< 
=<< 
$char<< "
;<<" #
for== 
(== 
;== 
i== 
<== 
$num== 
;== 
i== 
++== 
)== 
{>> 
int?? 
value?? 
=?? 
bytes?? !
[??! "
i??" #
]??# $
;??$ %
result@@ 
[@@ 
offset@@ 
++@@ 
]@@  
=@@! "
	HexToChar@@# ,
(@@, -
value@@- 2
>>@@3 5
$num@@6 7
,@@7 8
_alpha@@9 ?
)@@? @
;@@@ A
resultAA 
[AA 
offsetAA 
++AA 
]AA  
=AA! "
	HexToCharAA# ,
(AA, -
valueAA- 2
,AA2 3
_alphaAA4 :
)AA: ;
;AA; <
}BB 
ifCC 
(CC 
_suffixCC 
!=CC 
$charCC 
)CC  
resultDD 
[DD 
offsetDD 
]DD 
=DD  
_suffixDD! (
;DD( )
returnFF 
newFF 
stringFF 
(FF 
resultFF $
,FF$ %
$numFF& '
,FF' (
_lengthFF) 0
)FF0 1
;FF1 2
}GG 	
staticII 
charII 
	HexToCharII 
(II 
intII !
valueII" '
,II' (
intII) ,
alphaII- 2
)II2 3
{JJ 	
valueKK 
=KK 
valueKK 
&KK 
$numKK 
;KK  
returnLL 
(LL 
charLL 
)LL 
(LL 
(LL 
valueLL  
>LL! "
$numLL# $
)LL$ %
?LL& '
valueLL( -
-LL. /
$numLL0 2
+LL3 4
alphaLL5 :
:LL; <
valueLL= B
+LLC D
$numLLE I
)LLI J
;LLJ K
}MM 	
}NN 
}OO ¨
WC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdFormatters\HexFormatter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdFormatters( 7
{ 
internal 
class 
HexFormatter 
:  !
INewIdFormatter 
{ 
readonly 
int 
_alpha 
; 
public 
HexFormatter 
( 
bool  
	upperCase! *
=+ ,
false- 2
)2 3
{		 	
_alpha

 
=

 
	upperCase

 
?

  
$char

! $
:

% &
$char

' *
;

* +
} 	
public 
string 
Format 
( 
byte !
[! "
]" #
bytes$ )
)) *
{ 	
var 
result 
= 
new 
char !
[! "
$num" $
]$ %
;% &
int 
offset 
= 
$num 
; 
for 
( 
int 
i 
= 
$num 
; 
i 
< 
$num  "
;" #
i$ %
++% '
)' (
{ 
byte 
value 
= 
bytes "
[" #
i# $
]$ %
;% &
result 
[ 
offset 
++ 
]  
=! "
	HexToChar# ,
(, -
value- 2
>>3 5
$num6 7
,7 8
_alpha9 ?
)? @
;@ A
result 
[ 
offset 
++ 
]  
=! "
	HexToChar# ,
(, -
value- 2
,2 3
_alpha4 :
): ;
;; <
} 
return 
new 
string 
( 
result $
,$ %
$num& '
,' (
$num) +
)+ ,
;, -
} 	
static 
char 
	HexToChar 
( 
int !
value" '
,' (
int) ,
alpha- 2
)2 3
{ 	
value 
= 
value 
& 
$num 
;  
return 
( 
char 
) 
( 
( 
value  
>! "
$num# $
)$ %
?& '
value( -
-. /
$num0 2
+3 4
alpha5 :
:; <
value= B
+C D
$numE I
)I J
;J K
}   	
}!! 
}"" ì
[C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdFormatters\ZBase32Formatter.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdFormatters( 7
{ 
internal 
class 
ZBase32Formatter #
:$ %
Base32Formatter 
{ 
const 
string 
LowerCaseChars #
=$ %
$str& H
;H I
const 
string 
UpperCaseChars #
=$ %
$str& H
;H I
public

 
ZBase32Formatter

 
(

  
bool

  $
	upperCase

% .
=

/ 0
false

1 6
)

6 7
: 
base 
( 
	upperCase 
? 
UpperCaseChars -
:. /
LowerCaseChars0 >
)> ?
{ 	
} 	
} 
} ±&
IC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdGenerator.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
{ 
internal 
class 
NewIdGenerator !
{ 
readonly 
int 
_c 
; 
readonly 
int 
_d 
; 
readonly 
object 
_sync 
= 
new  #
object$ *
(* +
)+ ,
;, -
readonly		 
ITickProvider		 
_tickProvider		 ,
;		, -
int

 
_a

 
;

 
int 
_b 
; 
long 
	_lastTick 
; 
ushort 
	_sequence 
; 
public 
NewIdGenerator 
( 
ITickProvider +
tickProvider, 8
,8 9
IWorkerIdProvider: K
workerIdProviderL \
,\ ]
IProcessIdProvider^ p
processIdProvider	q Ç
=
É Ñ
null
Ö â
,
â ä
int
ã é
workerIndex
è ö
=
õ ú
$num
ù û
)
û ü
{ 	
_tickProvider 
= 
tickProvider (
;( )
byte 
[ 
] 
workerId 
= 
workerIdProvider .
.. /
GetWorkerId/ :
(: ;
workerIndex; F
)F G
;G H
_c 
= 
workerId 
[ 
$num 
] 
<< 
$num  "
|# $
workerId% -
[- .
$num. /
]/ 0
<<1 3
$num4 6
|7 8
workerId9 A
[A B
$numB C
]C D
<<E G
$numH I
|J K
workerIdL T
[T U
$numU V
]V W
;W X
if   
(   
processIdProvider    
!=  ! #
null  $ (
)  ( )
{!! 
var"" 
	processId"" 
="" 
processIdProvider""  1
.""1 2
GetProcessId""2 >
(""> ?
)""? @
;""@ A
_d## 
=## 
	processId## 
[## 
$num##  
]##  !
<<##" $
$num##% '
|##( )
	processId##* 3
[##3 4
$num##4 5
]##5 6
<<##7 9
$num##: <
;##< =
}$$ 
else%% 
{&& 
_d'' 
='' 
workerId'' 
['' 
$num'' 
]''  
<<''! #
$num''$ &
|''' (
workerId'') 1
[''1 2
$num''2 3
]''3 4
<<''5 7
$num''8 :
;'': ;
}(( 
})) 	
public// 
NewId// 
Next// 
(// 
)// 
{00 	
long11 
ticks11 
=11 
_tickProvider11 &
.11& '
Ticks11' ,
;11, -
lock22 
(22 
_sync22 
)22 
{33 
if44 
(44 
ticks44 
>44 
	_lastTick44 %
)44% &
this55 
.55 
UpdateTimestamp55 (
(55( )
ticks55) .
)55. /
;55/ 0
if77 
(77 
	_sequence77 
==77  
$num77! &
)77& '
this88 
.88 
UpdateTimestamp88 (
(88( )
	_lastTick88) 2
+883 4
$num885 6
)886 7
;887 8
ushort:: 
sequence:: 
=::  !
	_sequence::" +
++::+ -
;::- .
return<< 
new<< 
NewId<<  
(<<  !
_a<<! #
,<<# $
_b<<% '
,<<' (
_c<<) +
,<<+ ,
_d<<- /
|<<0 1
sequence<<2 :
)<<: ;
;<<; <
}== 
}>> 	
void@@ 
UpdateTimestamp@@ 
(@@ 
long@@ !
tick@@" &
)@@& '
{AA 	
	_lastTickBB 
=BB 
tickBB 
;BB 
	_sequenceCC 
=CC 
$numCC 
;CC 
_aEE 
=EE 
(EE 
intEE 
)EE 
(EE 
tickEE 
>>EE 
$numEE !
)EE! "
;EE" #
_bFF 
=FF 
(FF 
intFF 
)FF 
(FF 
tickFF 
&FF 
$numFF (
)FF( )
;FF) *
}GG 	
}HH 
}II Ä3
TC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdParsers\Base32Parser.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdParsers( 4
{ 
internal 
class 
Base32Parser 
:  !
INewIdParser 
{ 
const 
string 
ConvertChars !
=" #
$str$ f
;f g
const

 
string

 
HexChars

 
=

 
$str

  2
;

2 3
readonly 
string 
_chars 
; 
public 
Base32Parser 
( 
string "
chars# (
)( )
{ 	
if 
( 
chars 
. 
Length 
% 
$num !
!=" $
$num% &
)& '
throw 
new 
ArgumentException +
(+ ,
$str, U
)U V
;V W
_chars 
= 
chars 
; 
} 	
public 
Base32Parser 
( 
) 
{ 	
_chars 
= 
ConvertChars !
;! "
} 	
public 
NewId 
Parse 
( 
string !
text" &
)& '
{ 	
if 
( 
text 
? 
. 
Length 
!= 
$num  "
)" #
{ 
throw 
new '
ArgumentOutOfRangeException 5
(5 6
nameof6 <
(< =
text= A
)A B
,B C
$strD o
)o p
;p q
}   
var"" 
buffer"" 
="" 
new"" 
char"" !
[""! "
$num""" $
]""$ %
;""% &
int$$ 
bufferOffset$$ 
=$$ 
$num$$  
;$$  !
int%% 
offset%% 
=%% 
$num%% 
;%% 
long&& 
number&& 
;&& 
for'' 
('' 
int'' 
i'' 
='' 
$num'' 
;'' 
i'' 
<'' 
$num''  !
;''! "
++''# %
i''% &
)''& '
{(( 
number)) 
=)) 
$num)) 
;)) 
for** 
(** 
int** 
j** 
=** 
$num** 
;** 
j**  !
<**" #
$num**$ %
;**% &
j**' (
++**( *
)*** +
{++ 
int,, 
index,, 
=,, 
_chars,,  &
.,,& '
IndexOf,,' .
(,,. /
text,,/ 3
[,,3 4
offset,,4 :
+,,; <
j,,= >
],,> ?
),,? @
;,,@ A
if-- 
(-- 
index-- 
<-- 
$num--  !
)--! "
throw.. 
new.. !
ArgumentException.." 3
(..3 4
$str..4 a
)..a b
;..b c
number00 
=00 
number00 #
*00$ %
$num00& (
+00) *
(00+ ,
index00, 1
%002 3
$num004 6
)006 7
;007 8
}11 
ConvertLongToBase1633 #
(33# $
buffer33$ *
,33* +
bufferOffset33, 8
,338 9
number33: @
,33@ A
$num33B C
)33C D
;33D E
offset55 
+=55 
$num55 
;55 
bufferOffset66 
+=66 
$num66  !
;66! "
}77 
number99 
=99 
$num99 
;99 
for:: 
(:: 
int:: 
j:: 
=:: 
$num:: 
;:: 
j:: 
<:: 
$num::  !
;::! "
j::# $
++::$ &
)::& '
{;; 
int<< 
index<< 
=<< 
_chars<< "
.<<" #
IndexOf<<# *
(<<* +
text<<+ /
[<</ 0
offset<<0 6
+<<7 8
j<<9 :
]<<: ;
)<<; <
;<<< =
if== 
(== 
index== 
<== 
$num== 
)== 
throw>> 
new>> 
ArgumentException>> /
(>>/ 0
$str>>0 ]
)>>] ^
;>>^ _
number@@ 
=@@ 
number@@ 
*@@  !
$num@@" $
+@@% &
(@@' (
index@@( -
%@@. /
$num@@0 2
)@@2 3
;@@3 4
}AA 
ConvertLongToBase16BB 
(BB  
bufferBB  &
,BB& '
bufferOffsetBB( 4
,BB4 5
numberBB6 <
,BB< =
$numBB> ?
)BB? @
;BB@ A
returnDD 
newDD 
NewIdDD 
(DD 
newDD  
stringDD! '
(DD' (
bufferDD( .
,DD. /
$numDD0 1
,DD1 2
$numDD3 5
)DD5 6
)DD6 7
;DD7 8
}EE 	
staticGG 
voidGG 
ConvertLongToBase16GG '
(GG' (
charGG( ,
[GG, -
]GG- .
bufferGG/ 5
,GG5 6
intGG7 :
offsetGG; A
,GGA B
longGGC G
valueGGH M
,GGM N
intGGO R
countGGS X
)GGX Y
{HH 	
forII 
(II 
intII 
iII 
=II 
countII 
-II  
$numII! "
;II" #
iII$ %
>=II& (
$numII) *
;II* +
iII, -
--II- /
)II/ 0
{JJ 
varKK 
indexKK 
=KK 
(KK 
intKK  
)KK  !
(KK! "
valueKK" '
%KK( )
$numKK* ,
)KK, -
;KK- .
bufferLL 
[LL 
offsetLL 
+LL 
iLL  !
]LL! "
=LL# $
HexCharsLL% -
[LL- .
indexLL. 3
]LL3 4
;LL4 5
valueMM 
/=MM 
$numMM 
;MM 
}NN 
}OO 	
}PP 
}QQ »
UC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdParsers\ZBase32Parser.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdParsers( 4
{ 
internal 
class 
ZBase32Parser  
:! "
Base32Parser 
{ 
const 
string 
ConvertChars !
=" #
$str$ f
;f g
const 
string 
TransposeChars #
=$ %
$str& h
;h i
public

 
ZBase32Parser

 
(

 
bool

 !&
handleTransposedCharacters

" <
=

= >
false

? D
)

D E
: 
base 
( &
handleTransposedCharacters -
?. /
ConvertChars0 <
+= >
TransposeChars? M
:N O
ConvertCharsP \
)\ ]
{ 	
} 	
} 
} √
fC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\BestPossibleWorkerIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class (
BestPossibleWorkerIdProvider /
:0 1
IWorkerIdProvider 
{ 
public		 
byte		 
[		 
]		 
GetWorkerId		 !
(		! "
int		" %
index		& +
)		+ ,
{

 	
var 

exceptions 
= 
new  
List! %
<% &
	Exception& /
>/ 0
(0 1
)1 2
;2 3
try 
{ 
return 
new *
NetworkAddressWorkerIdProvider 9
(9 :
): ;
.; <
GetWorkerId< G
(G H
indexH M
)M N
;N O
} 
catch 
( 
	Exception 
ex 
)  
{ 

exceptions 
. 
Add 
( 
ex !
)! "
;" #
} 
try 
{ 
return 
new (
HostNameHashWorkerIdProvider 7
(7 8
)8 9
.9 :
GetWorkerId: E
(E F
indexF K
)K L
;L M
} 
catch 
( 
	Exception 
ex 
)  
{ 

exceptions 
. 
Add 
( 
ex !
)! "
;" #
} 
throw 
new 
AggregateException (
(( )

exceptions) 3
)3 4
;4 5
}   	
}!! 
}"" –
^C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\DateTimeTickProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class  
DateTimeTickProvider '
:( )
ITickProvider 
{ 
public 
long 
Ticks 
{		 	
get

 
{

 
return

 
DateTime

 !
.

! "
UtcNow

" (
.

( )
Ticks

) .
;

. /
}

0 1
} 	
} 
} à
fC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\HostNameHashWorkerIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class (
HostNameHashWorkerIdProvider /
:0 1
IWorkerIdProvider		 
{

 
public 
byte 
[ 
] 
GetWorkerId !
(! "
int" %
index& +
)+ ,
{ 	
return 
GetNetworkAddress $
($ %
)% &
;& '
} 	
static 
byte 
[ 
] 
GetNetworkAddress '
(' (
)( )
{ 	
try 
{ 
string 
hostName 
=  !
Dns" %
.% &
GetHostName& 1
(1 2
)2 3
;3 4
byte 
[ 
] 
hash 
; 
using 
( 
SHA1 
hasher "
=# $
SHA1% )
.) *
Create* 0
(0 1
)1 2
)2 3
{ 
hash 
= 
hasher !
.! "
ComputeHash" -
(- .
Encoding. 6
.6 7
UTF87 ;
.; <
GetBytes< D
(D E
hostNameE M
)M N
)N O
;O P
} 
var 
bytes 
= 
new 
byte  $
[$ %
$num% &
]& '
;' (
Buffer 
. 
	BlockCopy  
(  !
hash! %
,% &
$num' )
,) *
bytes+ 0
,0 1
$num2 3
,3 4
$num5 6
)6 7
;7 8
bytes 
[ 
$num 
] 
|= 
$num  
;  !
return   
bytes   
;   
}!! 
catch"" 
("" 
	Exception"" 
ex"" 
)""  
{## 
throw$$ 
new$$ %
InvalidOperationException$$ 3
($$3 4
$str$$4 Q
,$$Q R
ex$$S U
)$$U V
;$$V W
}%% 
}&& 	
}'' 
}(( Õ
hC:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\NetworkAddressWorkerIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class *
NetworkAddressWorkerIdProvider 1
:2 3
IWorkerIdProvider 
{		 
public

 
byte

 
[

 
]

 
GetWorkerId

 !
(

! "
int

" %
index

& +
)

+ ,
{ 	
return 
GetNetworkAddress $
($ %
index% *
)* +
;+ ,
} 	
static 
byte 
[ 
] 
GetNetworkAddress '
(' (
int( +
index, 1
)1 2
{ 	
byte 
[ 
] 
network 
= 
NetworkInterface -
. #
GetAllNetworkInterfaces (
(( )
)) *
. 
Where 
( 
x 
=> 
x 
.  
NetworkInterfaceType 2
==3 5 
NetworkInterfaceType6 J
.J K
EthernetK S
|| 
x  
.  ! 
NetworkInterfaceType! 5
==6 8 
NetworkInterfaceType9 M
.M N
GigabitEthernetN ]
|| 
x  
.  ! 
NetworkInterfaceType! 5
==6 8 
NetworkInterfaceType9 M
.M N
Wireless80211N [
|| 
x  
.  ! 
NetworkInterfaceType! 5
==6 8 
NetworkInterfaceType9 M
.M N
FastEthernetFxN \
|| 
x  
.  ! 
NetworkInterfaceType! 5
==6 8 
NetworkInterfaceType9 M
.M N
FastEthernetTN [
)[ \
. 
Select 
( 
x 
=> 
x 
. 
GetPhysicalAddress 1
(1 2
)2 3
)3 4
. 
Where 
( 
x 
=> 
x 
!=  
null! %
)% &
. 
Select 
( 
x 
=> 
x 
. 
GetAddressBytes .
(. /
)/ 0
)0 1
. 
Where 
( 
x 
=> 
x 
. 
Length $
==% '
$num( )
)) *
. 
Skip 
( 
index 
) 
. 
FirstOrDefault 
(  
)  !
;! "
if 
( 
network 
== 
null 
)  
throw   
new   %
InvalidOperationException   3
(  3 4
$str  4 n
)  n o
;  o p
return"" 
network"" 
;"" 
}## 	
}$$ 
}%% ˙	
[C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\ProcessIdProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class $
CurrentProcessIdProvider +
:, -
IProcessIdProvider 
{ 
public		 
byte		 
[		 
]		 
GetProcessId		 "
(		" #
)		# $
{

 	
var 
	processId 
= 
BitConverter (
.( )
GetBytes) 1
(1 2
Process2 9
.9 :
GetCurrentProcess: K
(K L
)L M
.M N
IdN P
)P Q
;Q R
if 
( 
	processId 
. 
Length 
<  !
$num" #
)# $
{ 
throw 
new %
InvalidOperationException 3
(3 4
$str4 b
)b c
;c d
} 
return 
	processId 
; 
} 	
} 
} Á

_C:\Source\Stacks\Core\src\Slalom.Stacks\Utilities\NewId\NewIdProviders\StopwatchTickProvider.cs
	namespace 	
Slalom
 
. 
Stacks 
. 
	Utilities !
.! "
NewId" '
.' (
NewIdProviders( 6
{ 
internal 
class !
StopwatchTickProvider (
:) *
ITickProvider 
{ 
readonly		 
	Stopwatch		 

_stopwatch		 %
;		% &
DateTime

 
_start

 
;

 
public !
StopwatchTickProvider $
($ %
)% &
{ 	
_start 
= 
DateTime 
. 
UtcNow $
;$ %

_stopwatch 
= 
	Stopwatch "
." #
StartNew# +
(+ ,
), -
;- .
} 	
public 
long 
Ticks 
{ 	
get 
{ 
return 
( 
_start  
.  !
AddTicks! )
() *

_stopwatch* 4
.4 5
Elapsed5 <
.< =
Ticks= B
)B C
)C D
.D E
TicksE J
;J K
}L M
} 	
} 
} ü
>C:\Source\Stacks\Core\src\Slalom.Stacks\Validation\Argument.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
public 

static 
class 
Argument  
{ 
public 
static 
void 
NotNull "
(" #
object# )
instance* 2
,2 3
string4 :
name; ?
)? @
{ 	
if 
( 
instance 
== 
null  
)  !
{ 
throw 
new !
ArgumentNullException /
(/ 0
name0 4
)4 5
;5 6
} 
} 	
public"" 
static"" 
void"" 
NotNullOrEmpty"" )
<"") *
T""* +
>""+ ,
("", -
IEnumerable""- 8
<""8 9
T""9 :
>"": ;
value""< A
,""A B
string""C I
name""J N
)""N O
{## 	
if$$ 
($$ 
value$$ 
==$$ 
null$$ 
||$$  
!$$! "
value$$" '
.$$' (
Any$$( +
($$+ ,
)$$, -
)$$- .
{%% 
throw&& 
new&& 
ArgumentException&& +
(&&+ ,
string&&, 2
.&&2 3
Format&&3 9
(&&9 :
CultureInfo&&: E
.&&E F
InvariantCulture&&F V
,&&V W
$str'' P
,''P Q
name''R V
)''V W
,''W X
name''Y ]
)''] ^
;''^ _
}(( 
})) 	
public.. 
static.. 
void.. 
NotNullOrWhiteSpace.. .
(... /
string../ 5
value..6 ;
,..; <
string..= C
name..D H
)..H I
{// 	
if00 
(00 
string00 
.00 
IsNullOrWhiteSpace00 )
(00) *
value00* /
)00/ 0
)000 1
{11 
throw22 
new22 
ArgumentException22 +
(22+ ,
string22, 2
.222 3
Format223 9
(229 :
CultureInfo22: E
.22E F
InvariantCulture22F V
,22V W
$str33 b
,33b c
name33d h
,33h i
value33j o
)33o p
,33p q
name33r v
)33v w
;33w x
}44 
}55 	
}66 
}77 ®
JC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\GreaterThanAttribute.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 

Validation

 "
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

class  
GreaterThanAttribute %
:& '
ValidationAttribute( ;
{ 
public  
GreaterThanAttribute #
(# $
int$ '
value( -
,- .
string/ 5
message6 =
)= >
: 
base 
( 
message 
) 
{ 	
this 
. 
Value 
= 
value 
; 
} 	
public"" 
int"" 
Value"" 
{"" 
get"" 
;"" 
}""  !
public)) 
override)) 
bool)) 
IsValid)) $
())$ %
object))% +
value)), 1
)))1 2
{** 	
return++ 
value++ 
is++ 
int++ 
&&++  "
(++# $
int++$ '
)++' (
value++) .
>++/ 0
this++1 5
.++5 6
Value++6 ;
;++; <
},, 	
}-- 
}.. ≈
?C:\Source\Stacks\Core\src\Slalom.Stacks\Validation\IValidate.cs
	namespace

 	
Slalom


 
.

 
Stacks

 
.

 

Validation

 "
{ 
public 

	interface 
	IValidate 
{ 
IEnumerable 
< 
ValidationError #
># $
Validate% -
(- .
). /
;/ 0
} 
} ∑
EC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\IValidationRule.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
public 

	interface 
	IValidate 
< 
in !
TValue" (
>( )
{ 
IEnumerable 
< 
ValidationError #
># $
Validate% -
(- .
TValue. 4
instance5 =
)= >
;> ?
} 
} á
CC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\JsonAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
[

 
AttributeUsage

 
(

 
AttributeTargets

 $
.

$ %
Property

% -
)

- .
]

. /
public 

class 
JsonAttribute 
:  
ValidationAttribute! 4
{ 
public 
JsonAttribute 
( 
string #
message$ +
)+ ,
: 
base 
( 
message 
) 
{ 	
} 	
public 
JsonAttribute 
( 
) 
: 
base 
( 
null 
) 
{ 	
} 	
public 
override 
ValidationError '
GetValidationError( :
(: ;
PropertyInfo; G
propertyH P
)P Q
{   	
return!! 
new!! 
ValidationError!! &
(!!& '
this!!' +
.!!+ ,
Code!!, 0
,!!0 1
this!!2 6
.!!6 7
Message!!7 >
??!!? A
property!!B J
.!!J K
Name!!K O
+!!P Q
$str!!R j
)!!j k
;!!k l
}"" 	
public%% 
override%% 
bool%% 
IsValid%% $
(%%$ %
object%%% +
value%%, 1
)%%1 2
{&& 	
if'' 
('' 
value'' 
is'' 
string'' 
)''  
{(( 
var)) 
strInput)) 
=)) 
())  
())  !
string))! '
)))' (
value))( -
)))- .
.)). /
Trim))/ 3
())3 4
)))4 5
;))5 6
if** 
(** 
strInput** 
.** 

StartsWith** '
(**' (
$str**( +
)**+ ,
&&**- /
strInput**0 8
.**8 9
EndsWith**9 A
(**A B
$str**B E
)**E F
||**G I
strInput**J R
.**R S

StartsWith**S ]
(**] ^
$str**^ a
)**a b
&&**c e
strInput**f n
.**n o
EndsWith**o w
(**w x
$str**x {
)**{ |
)**| }
{++ 
try,, 
{-- 
JToken.. 
... 
Parse.. $
(..$ %
strInput..% -
)..- .
;... /
return// 
true// #
;//# $
}00 
catch11 
{22 
return33 
false33 $
;33$ %
}44 
}55 
}66 
return77 
false77 
;77 
}88 	
}99 
}:: €
FC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\NotNullAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
[

 
AttributeUsage

 
(

 
AttributeTargets

 $
.

$ %
Property

% -
)

- .
]

. /
public 

class 
NotNullAttribute !
:" #
ValidationAttribute$ 7
{ 
public 
NotNullAttribute 
(  
string  &
message' .
). /
: 
base 
( 
message 
) 
{ 	
} 	
public 
NotNullAttribute 
(  
)  !
: 
base 
( 
null 
) 
{ 	
} 	
public 
override 
bool 
IsValid $
($ %
object% +
value, 1
)1 2
{   	
return!! 
value!! 
!=!! 
null!!  
;!!  !
}"" 	
public%% 
override%% 
ValidationError%% '
GetValidationError%%( :
(%%: ;
PropertyInfo%%; G
property%%H P
)%%P Q
{&& 	
return'' 
new'' 
ValidationError'' &
(''& '
this''' +
.''+ ,
Code'', 0
,''0 1
this''2 6
.''6 7
Message''7 >
??''? A
property''B J
.''J K
Name''K O
+''P Q
$str''R g
)''g h
;''h i
}(( 	
})) 
}** ˝
RC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\NotNullOrWhitespaceAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
[

 
AttributeUsage

 
(

 
AttributeTargets

 $
.

$ %
Property

% -
)

- .
]

. /
public 

class (
NotNullOrWhiteSpaceAttribute -
:. /
ValidationAttribute0 C
{ 
public (
NotNullOrWhiteSpaceAttribute +
(+ ,
string, 2
message3 :
): ;
: 
base 
( 
message 
) 
{ 	
} 	
public (
NotNullOrWhiteSpaceAttribute +
(+ ,
), -
: 
base 
( 
null 
) 
{ 	
} 	
public 
override 
ValidationError '
GetValidationError( :
(: ;
PropertyInfo; G
propertyH P
)P Q
{   	
return!! 
new!! 
ValidationError!! &
(!!& '
this!!' +
.!!+ ,
Code!!, 0
,!!0 1
this!!2 6
.!!6 7
Message!!7 >
??!!? A
property!!B J
.!!J K
Name!!K O
+!!P Q
$str!!R g
)!!g h
;!!h i
}"" 	
public)) 
override)) 
bool)) 
IsValid)) $
())$ %
object))% +
value)), 1
)))1 2
{** 	
return++ 
!++ 
string++ 
.++ 
IsNullOrWhiteSpace++ -
(++- .
value++. 3
as++4 6
string++7 =
)++= >
;++> ?
},, 	
}-- 
}.. µ
BC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\UrlAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
[		 
AttributeUsage		 
(		 
AttributeTargets		 $
.		$ %
Property		% -
)		- .
]		. /
public

 

class

 
UrlAttribute

 
:

 
ValidationAttribute

  3
{ 
public 
UrlAttribute 
( 
string "
message# *
)* +
: 
base 
( 
message 
) 
{ 	
} 	
public 
UrlAttribute 
( 
) 
: 
base 
( 
null 
) 
{ 	
} 	
public 
override 
ValidationError '
GetValidationError( :
(: ;
PropertyInfo; G
propertyH P
)P Q
{ 	
return   
new   
ValidationError   &
(  & '
this  ' +
.  + ,
Code  , 0
,  0 1
this  2 6
.  6 7
Message  7 >
??  ? A
property  B J
.  J K
Name  K O
+  P Q
$str  R o
)  o p
;  p q
}!! 	
public$$ 
override$$ 
bool$$ 
IsValid$$ $
($$$ %
object$$% +
value$$, 1
)$$1 2
{%% 	
if&& 
(&& 
value&& 
is&& 
Uri&& 
)&& 
{'' 
return(( 
true(( 
;(( 
})) 
if** 
(** 
value** 
is** 
string** 
)**  
{++ 
var,, 
strInput,, 
=,, 
(,,  
(,,  !
string,,! '
),,' (
value,,( -
),,- .
.,,. /
Trim,,/ 3
(,,3 4
),,4 5
;,,5 6
return-- 
Uri-- 
.-- !
IsWellFormedUriString-- 0
(--0 1
strInput--1 9
,--9 :
UriKind--; B
.--B C
RelativeOrAbsolute--C U
)--U V
;--V W
}.. 
return// 
false// 
;// 
}00 	
}11 
}22 Ò
IC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\ValidationAttribute.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
public

 

abstract

 
class

 
ValidationAttribute

 -
:

. /
	Attribute

0 9
{ 
	protected 
ValidationAttribute %
(% &
string& ,
message- 4
)4 5
{ 	
this 
. 
Message 
= 
message "
;" #
} 	
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Message 
{ 
get  #
;# $
}% &
public&& 
abstract&& 
bool&& 
IsValid&& $
(&&$ %
object&&% +
value&&, 1
)&&1 2
;&&2 3
public-- 
virtual-- 
ValidationError-- &
GetValidationError--' 9
(--9 :
PropertyInfo--: F
property--G O
)--O P
{.. 	
return// 
new// 
ValidationError// &
(//& '
this//' +
.//+ ,
Code//, 0
,//0 1
this//2 6
.//6 7
Message//7 >
)//> ?
;//? @
}00 	
}11 
}22 õ
EC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\ValidationError.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
public 

class 
ValidationError  
{ 
public 
ValidationError 
( 
)  
{ 	
} 	
public 
ValidationError 
( 
string %
message& -
)- .
: 
this 
( 
null 
, 
message  
,  !
ValidationType" 0
.0 1
None1 5
)5 6
{ 	
}   	
public'' 
ValidationError'' 
('' 
string'' %
message''& -
,''- .
ValidationType''/ =
type''> B
)''B C
:(( 
this(( 
((( 
null(( 
,(( 
message((  
,((  !
type((" &
)((& '
{)) 	
}** 	
public11 
ValidationError11 
(11 
string11 %
code11& *
,11* +
string11, 2
message113 :
)11: ;
:22 
this22 
(22 
code22 
,22 
message22  
,22  !
ValidationType22" 0
.220 1
None221 5
)225 6
{33 	
}44 	
public<< 
ValidationError<< 
(<< 
string<< %
code<<& *
,<<* +
string<<, 2
message<<3 :
,<<: ;
ValidationType<<< J
type<<K O
)<<O P
{== 	
this>> 
.>> 
Code>> 
=>> 
code>> 
;>> 
this?? 
.?? 
Message?? 
=?? 
message?? "
;??" #
this@@ 
.@@ 
Type@@ 
=@@ 
type@@ 
;@@ 
}AA 	
publicII 
stringII 
CodeII 
{II 
getII  
;II  !
}II" #
publicQQ 
stringQQ 
MessageQQ 
{QQ 
getQQ  #
;QQ# $
}QQ% &
[YY 	
JsonConverterYY	 
(YY 
typeofYY 
(YY 
StringEnumConverterYY 1
)YY1 2
)YY2 3
]YY3 4
publicZZ 
ValidationTypeZZ 
TypeZZ "
{ZZ# $
getZZ% (
;ZZ( )
privateZZ* 1
setZZ2 5
;ZZ5 6
}ZZ7 8
publiccc 
staticcc 
implicitcc 
operatorcc '
ValidationErrorcc( 7
(cc7 8
stringcc8 >
messagecc? F
)ccF G
{dd 	
returnee 
newee 
ValidationErroree &
(ee& '
messageee' .
)ee. /
;ee/ 0
}ff 	
publicmm 
ValidationErrormm 
WithTypemm '
(mm' (
ValidationTypemm( 6
typemm7 ;
)mm; <
{nn 	
thisoo 
.oo 
Typeoo 
=oo 
typeoo 
;oo 
returnqq 
thisqq 
;qq 
}rr 	
}ss 
}tt È	
IC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\ValidationException.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{ 
public 

class 
ValidationException $
:% &%
InvalidOperationException' @
{ 
public 
ValidationException "
(" #
params# )
ValidationError* 9
[9 :
]: ;
errors< B
)B C
: 
base 
( 
string 
. 
Join 
( 
Environment *
.* +
NewLine+ 2
,2 3
errors4 :
.: ;
Select; A
(A B
eB C
=>D F
eG H
.H I
MessageI P
)P Q
)Q R
)R S
{ 	
this 
. 
ValidationErrors !
=" #
errors$ *
;* +
} 	
public!! 
ValidationError!! 
[!! 
]!!  
ValidationErrors!!! 1
{!!2 3
get!!4 7
;!!7 8
}!!9 :
}"" 
}## ±
DC:\Source\Stacks\Core\src\Slalom.Stacks\Validation\ValidationType.cs
	namespace 	
Slalom
 
. 
Stacks 
. 

Validation "
{		 
public 

enum 
ValidationType 
{ 
None 
, 
Input 
, 
Security 
, 
Business!! 
}"" 
}## 