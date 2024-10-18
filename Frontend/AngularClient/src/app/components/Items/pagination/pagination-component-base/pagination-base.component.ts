import {AfterViewInit, Component, Directive, Input, OnChanges, OnInit} from '@angular/core';
import {PageSettings} from "../../../../Data/Queries/PageSettings";
import {fromEvent, Observable, tap} from "rxjs";

@Component({template: ``})
export abstract class PaginationBaseComponent<TEntity> implements OnInit, OnChanges, AfterViewInit  {
  public Entities: TEntity[] = []
  public LoadingThreshold = 40;
  protected _loadingContainerId?: string
  private _loadingContainer!: HTMLElement;
  public CheckLoadingNecessary?: (htmlElement: HTMLElement, loadingThreshold: number) => boolean

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

  protected _entitySource!: (pageSettings: PageSettings) => Observable<TEntity[]>
  private _isLoading: boolean = false
  private _isEnded: boolean = false

  protected constructor() {
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

  ngAfterViewInit(): void {
    if(!this._loadingContainerId){
      console.warn(`No container id provided`)
      return
    }

    const container = document.getElementById(this._loadingContainerId)

    if(!container){
      console.warn(`Container not found with id ${this._loadingContainerId}`)
      return
    }

    if(!this.CheckLoadingNecessary){
      console.warn(`No CheckLoadingNecessary function provided`)
      return
    }

    this._loadingContainer = container
    fromEvent(container, 'scroll')
      .pipe(
        tap(() => {
          if(this.CheckLoadingNecessary!(this._loadingContainer, this.LoadingThreshold)){
            this.LoadEntities()
          }
        })
      )
      .subscribe();
  }

  private LoadEntities(){
    if (this._isLoading || this._isEnded) {
      return
    }

    this._isLoading = true
    this._entitySource(this._pageSettings).subscribe({
      next: entities => {
        this.OnLoadEntities(entities)
        this.UpdateIsPostsEnded(entities)
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

  protected OnLoadEntities(entities: TEntity[]){
    this.Entities = [...this.Entities, ...entities]
  }

  private UpdateIsPostsEnded(entities: TEntity[]) {
    this._isEnded = entities.length < this._pageSettings.pageSize
  }

  public IsLoading(): boolean {
    return this._isLoading
  }

  public IsEnded(): boolean {
    return this._isEnded
  }
}
