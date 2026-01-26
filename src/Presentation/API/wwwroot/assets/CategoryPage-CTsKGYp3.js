import{b as p,u as h,a as y,r as i,j as e}from"./index-DVEBoSxW.js";import{L as u}from"./Loader-DoB6LG-b.js";import{u as b}from"./useProducts-fGJefkQj.js";import"./useQuery-CJ4rvfds.js";import"./dependencyInjector-uylwiMyK.js";import"./httpClient-gAZW64Lv.js";const d={all:"🏪",electronics:"💻",jewelery:"💎","men's clothing":"👔","women's clothing":"👗"},j={all:"primary",electronics:"info",jewelery:"warning","men's clothing":"secondary","women's clothing":"danger"},k=()=>{const c=p(),{t:a}=h(),{data:t,isLoading:m,isError:x}=b(),[g]=y(),l=g.get("category")||"all",n=i.useMemo(()=>t?Array.from(new Set(t.map(o=>o.category))).sort():[],[t]),s=i.useCallback(r=>{c(`/?category=${encodeURIComponent(r)}`)},[c]);return m?e.jsx("div",{className:"container my-5",role:"status","aria-live":"polite",children:e.jsx(u,{})}):x||n.length===0?e.jsx("div",{className:"container my-5 text-center",role:"alert",children:e.jsxs("div",{className:"p-5 bg-light rounded-3 shadow-sm",children:[e.jsx("h2",{className:"display-6 text-secondary mb-3",children:a("error")}),e.jsx("p",{className:"text-muted",children:a("productNotFound")})]})}):e.jsxs("div",{className:"container my-5",children:[e.jsxs("div",{className:"text-center mb-5",children:[e.jsx("h1",{className:"display-4 fw-bold mb-3",children:a("categories")}),e.jsx("p",{className:"text-muted fs-5",children:a("selectCategory")??"Select a category to start shopping"})]}),e.jsxs("div",{className:"row g-4",children:[e.jsx("div",{className:"col-md-6 col-lg-4",children:e.jsx("div",{onClick:()=>s("all"),className:`category-card card border-0 shadow-sm h-100 ${l==="all"?"active":""}`,style:{cursor:"pointer",transition:"all 0.3s ease"},role:"button",tabIndex:0,onKeyPress:r=>r.key==="Enter"&&s("all"),children:e.jsxs("div",{className:"card-body text-center p-4",children:[e.jsx("div",{className:"category-icon fs-1 mb-3",children:d.all}),e.jsx("h3",{className:"card-title fw-bold text-capitalize mb-2",children:a("products")}),e.jsx("p",{className:"text-muted small mb-0",children:a("viewAllProducts")??"View products from all categories"})]})})}),n.map(r=>e.jsx("div",{className:"col-md-6 col-lg-4",children:e.jsx("div",{onClick:()=>s(r),className:`category-card card border-0 shadow-sm h-100 ${l===r?"active":""}`,style:{cursor:"pointer",transition:"all 0.3s ease"},role:"button",tabIndex:0,onKeyPress:o=>o.key==="Enter"&&s(r),children:e.jsxs("div",{className:"card-body text-center p-4",children:[e.jsx("div",{className:"category-icon fs-1 mb-3",children:d[r]||"📦"}),e.jsx("h3",{className:"card-title fw-bold text-capitalize mb-2",children:r}),e.jsx("span",{className:`badge bg-${j[r]||"primary"} px-3 py-2`,children:a("viewDetails")})]})})},r))]}),e.jsx("style",{children:`
        .category-card {
          transform: translateY(0);
          border: 2px solid transparent !important;
        }
        .category-card:hover {
          transform: translateY(-8px);
          box-shadow: 0 8px 24px rgba(0,0,0,0.15) !important;
        }
        .category-card.active {
          border-color: var(--bs-primary) !important;
          box-shadow: 0 8px 24px rgba(13,110,253,0.3) !important;
        }
        .category-icon {
          transition: transform 0.3s ease;
        }
        .category-card:hover .category-icon {
          transform: scale(1.2) rotate(5deg);
        }
      `})]})};export{k as default};
