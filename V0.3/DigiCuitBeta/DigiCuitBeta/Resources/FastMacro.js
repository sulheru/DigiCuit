loadbb(1,1,64,1);
loadbb(1,2,64,1);
for(var i=0;i<64;i++){
    loadbb(i+1,3,1,5);
}
for(var i=0;i<64;i++){
    loadbb(i+1,10,1,5);
}
loadbb(1,15,64,1);
loadbb(1,16,64,1);
loadbb(1,20,1,2);
loadbb(2,20,1,2);

circuit.add(DCPowerSource);
circuit.add(IC7408);

Components.push(new DCPowerSource());
var i=Components.length-1;
chLoc(i,1,21);
Components.push(new IC7408());
i=Components.length-1;
chLoc(i,1,2);