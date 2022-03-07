import{_ as y,V as f,v as x,k as A,P as N,A as n}from"./index.83b4152d.js";import{E as S,c as i,a,o as c,b as l,d as s,t as r,F as u,p as g,e as d,n as _,q as I,s as P}from"./vendor.a0086ab9.js";const j={components:{Vault:f},setup(){return S(async()=>{try{await x.getVaultsForUser(),await A.getMyKeeps()}catch(e){logger.error(e),N.toast(e,"error")}}),{account:i(()=>n.account),keeps:i(()=>n.keeps),vaults:i(()=>n.userVaults),vaultKeeps:i(()=>n.vaultKeeps)}}},p=e=>(I("data-v-2bdd7757"),e=e(),P(),e),B={class:"container-fluid"},D={class:"row mt-5"},F={class:"col-md-2"},E=["src"],M={class:"col-md-3"},q={class:"row"},C={class:"col-md-12"},L={class:"col-md-12"},T=_(" Keeps: "),U={class:"col-md-12"},z=_(" Vaults: "),G={class:"row mt-5"},H=p(()=>s("div",{class:"col-md-12"},[s("h3",null,[_(" Vaults "),s("i",{title:"create vault",class:"mdi mdi-plus text-success selectable","data-bs-toggle":"modal","data-bs-target":"#create-vault-modal"}),s("i")])],-1)),J={class:"col-md-12"},O={class:"row ms-4"},Q=p(()=>s("div",null,null,-1)),R={class:"row mt-5"},W=p(()=>s("div",{class:"col-md-12"},[s("h3",null,[_(" Keeps "),s("i",{"data-bs-toggle":"modal","data-bs-target":"#create-keep-modal",title:"create vault",class:"mdi mdi-plus text-success selectable"})])],-1)),X={class:"col-md-12"},Y={class:"row ms-4"},Z={class:"col-md-12 masonry-with-columns"};function $(e,ss,es,t,ts,os){var m,v,h;const w=a("Vault"),k=a("Keep"),V=a("AddNewVault"),b=a("KeepDetails"),K=a("AddNewKeep");return c(),l(u,null,[s("div",B,[s("div",D,[s("div",F,[s("img",{class:"rounded object-cover-fit",src:t.account.picture,alt:""},null,8,E)]),s("div",M,[s("div",q,[s("div",C,[s("h1",null,r((m=t.account.name)==null?void 0:m.split("@")[0]),1)]),s("div",L,[s("h4",null,[T,s("span",null,r((v=t.keeps)==null?void 0:v.length),1)])]),s("div",U,[s("h4",null,[z,s("span",null,r((h=t.vaults)==null?void 0:h.length),1)])])])])]),s("div",G,[H,s("div",J,[s("div",O,[(c(!0),l(u,null,g(t.vaults,o=>(c(),l("div",{class:"col-md-2 my-2",key:o.id},[d(w,{vault:o},null,8,["vault"])]))),128))])]),Q]),s("div",R,[W,s("div",X,[s("div",Y,[s("div",Z,[(c(!0),l(u,null,g(t.keeps,o=>(c(),l("div",{class:"keep",key:o.id},[s("div",null,[d(k,{keep:o},null,8,["keep"])])]))),128))])])])])]),d(V),d(b),d(K)],64)}var ls=y(j,[["render",$],["__scopeId","data-v-2bdd7757"]]);export{ls as default};
