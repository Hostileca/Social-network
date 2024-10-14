import {Component, Directive, Input, OnChanges, OnInit} from '@angular/core';
import {PageSettings} from "../../../Data/Queries/PageSettings";
import {fromEvent, Observable, tap} from "rxjs";

@Component({template: ``})
export abstract class PaginationBaseComponent<TEntity> implements OnInit, OnChanges {
  public Entities: TEntity[] = []
  public LoadingThreshold = 40;

  private _pageSettings: PageSettings = {
    pageSize: 1,
    pageNumber: 1
  };

  @Input({required: true}) set PageSize(pageSize: number) {
    this._pageSettings.pageSize = pageSize
  }

  @Input({required: true}) set EntitySource(entitySource: (pageSettings: PageSettings) => Observable<TEntity[]>) {
    this._entitySource = entitySource
  }

  private _entitySource!: (pageSettings: PageSettings) => Observable<TEntity[]>
  private _isLoading: boolean = false
  private _isEnded: boolean = false

  protected constructor() {
    fromEvent(document, 'scroll')
      .pipe(
        tap(() => {
          this.ReachingBottomHandler()
        })
      )
      .subscribe();
  }

  ngOnInit(): void {
    this.LoadEntities()
  }

  ngOnChanges(): void {
    this.Entities = []
    this._pageSettings.pageNumber = 1
    this._isLoading = false
    this._isEnded = false
    this.LoadEntities()
  }

  private ReachingBottomHandler(){
    let fullDocumentHeight = Math.max(
      document.body.scrollHeight,
      document.documentElement.scrollHeight,
      document.body.offsetHeight,
      document.documentElement.offsetHeight,
      document.body.clientHeight,
      document.documentElement.clientHeight
    );
    const haveIReachedBottom =
      fullDocumentHeight - this.LoadingThreshold <
      window.scrollY + document.documentElement.clientHeight;
    if (haveIReachedBottom) {
      this.LoadEntities()
    }
  }

  private LoadEntities(){
    if (this._isLoading || this._isEnded) {
      return
    }

    this._isLoading = true
    this._entitySource(this._pageSettings).subscribe({
      next: entities => {
        this.Entities = [...this.Entities, ...entities]
        this.CheckIsPostsEnded(entities)
        this._pageSettings.pageNumber += 1
      },
      error: err => {
        console.error(err)
      },
      complete: () => {
        this._isLoading = false
      }
    })
  }

  private CheckIsPostsEnded(entities: TEntity[]) {
    this._isEnded = entities.length < this._pageSettings.pageSize
  }

  public IsLoading(): boolean {
    return this._isLoading
  }

  public IsEnded(): boolean {
    return this._isEnded
  }
}
