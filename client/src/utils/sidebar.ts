interface SidebarItem {
  title: string;
  icon: string;
  path : string;
}

const sidebarItems: SidebarItem[] = [
    {
        title: 'หน้าหลัก',
        icon: 'fas fa-home',
        path: '/'
    },
  {
    title: 'หมวดหมู่',
    icon: 'fas fa-th-list',
    path: '/categories'
  }
]


export default sidebarItems;