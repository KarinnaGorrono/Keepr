import{E as l,c as i,a as r,o as s,b as t,d as a,F as n,p as m,e as c}from"./vendor.a0086ab9.js";import{_ as u,k as v,P as f,A as g}from"./index.83b4152d.js";const k={name:"Home",setup(){return l(async()=>{try{await v.getAllKeeps()}catch(e){logger.error(e),f.toast(e,"error")}}),{keeps:i(()=>g.keeps)}}},h={class:"container-fluid"},y={class:"homePage masonry-with-columns"};function x(e,K,P,p,b,A){const _=r("Keep"),d=r("KeepDetails");return s(),t(n,null,[a("div",h,[a("div",y,[(s(!0),t(n,null,m(p.keeps,o=>(s(),t("div",{class:"thing pt-1",key:o.id},[c(_,{keep:o},null,8,["keep"])]))),128))])]),a("div",null,[c(d)])],64)}var $=u(k,[["render",x],["__scopeId","data-v-2491887f"]]);export{$ as default};