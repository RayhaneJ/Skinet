import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit{
  products: Product[] = []
  types: Type[] = []
  brands: Brand[] = []
  typeIdSelected: number = 0
  brandIdSelected: number = 0
  sortSelected = 'name'
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to high', value: 'priceAsc'},
    {name: 'High to low', value: 'priceDesc'}
  ]

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
    this.getProducts()
    this.getBrands()
    this.getTypes()
  }

  getProducts(){
    this.shopService.getProducts(this.typeIdSelected, this.brandIdSelected, this.sortSelected).subscribe({
      next: response => this.products = response.data,
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
    this.typeIdSelected = typeId
    console.log(typeId)
    this.getProducts()
  }

  onBrandIdSelected(brandId:number){
    this.brandIdSelected = brandId
    this.getProducts()
  }

  onSortSelected(event:any){
    this.sortSelected = event.target.value
    this.getProducts();
  }
}
