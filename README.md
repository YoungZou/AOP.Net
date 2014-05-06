AOP(aspect oriented programming) is the concept of seperating the business logic and basic logic such as add log, catch exception, event, authoration validation, we can focus on the business logic and migrate all other code in the common place.

.Net framework supports this by the ContextAttribute, MessageSink and ContextBoundObject.

In this project, you can add the dll of Gbi.DemoService to you project and then create your own attribute that derive from the class of BaseAopAttribute in that dll, then override two method PreProcess and PostProcess in the paraent clas, you can add your logic in that two method, the PreProcess will be executed before your target object method and the PostProcess will be executed after your target method, then add your customize attribute into you class that which you want to add AOP.

BE AWARE: all of your class that you want to implement AOP should be derived from ContentBoundObject, this is some limitions and make it far from perfect for AOP.

about the details, you can get is via here: http://msdn.microsoft.com/en-us/magazine/cc164165.aspx
