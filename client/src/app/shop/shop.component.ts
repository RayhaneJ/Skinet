import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { shopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit{
  products: Product[] = []
  types: Type[] = []
  brands: Brand[] = []
  shopParams = new shopParams()
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to high', value: 'priceAsc'},
    {name: 'High to low', value: 'priceDesc'}
  ]
  totalCount = 0

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
    this.getProducts()
    this.getBrands()
    this.getTypes()
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        this.products = response.data
        this.shopParams.pageSize = response.pageSize
        this.shopParams.pageNumber = response.pageIndex
        this.totalCount = response.count
      }, 
      error: error => console.log(error)
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next:response => this.brands = [{id: 0, name: 'all'}, ...response],
      error: error => console.log(error)
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next:response => this.types = [{id: 0, name: 'all'}, ...response], 
      error: error => console.log(error)
    })
  }

  onTypeIdSelected(typeId: number){
    this.shopParams.typeId = typeId
    console.log(typeId)
    this.getProducts()
  }

  onBrandIdSelected(brandId:number){
    this.shopParams.brandId = brandId
    this.getProducts()
  }

  onSortSelected(event:any){
    this.shopParams.sort = event.target.value
    this.getProducts();
  }

  onPageChanged(event:any){
    if(this.shopParams.pageNumber != event){
      this.shopParams.pageNumber = event
      this.getProducts()
    }
  }
}
