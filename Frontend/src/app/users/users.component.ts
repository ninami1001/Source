import { Component, OnInit } from '@angular/core';
import { UsersService } from './users.service';
import { map, tap } from 'rxjs/operators';
export interface Lists
{
  Item1: any;
  Item2: any;
}
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  //panelOpenState: boolean;
  usersList: any;
  postsList: any;
  displayedColumns: string[] = ['name', 'userName',  'emailAddress','posts' ];
  constructor(private userService: UsersService) { }

  ngOnInit()
  {
  }
  updateData() {
    this.userService.importAndGetData()
      // .pipe(
      //   map(({ items }) => ({
      //     usersList: items,
      //   })),
      //   tap((_) => console.log("users array", this.usersList))
      // )
       .subscribe(response => { 
        this.usersList = response;
       
      console.log( this.usersList,response,response.Item1,"aaa")      });
  }
}
