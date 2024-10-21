export interface Post{
  id: string
  content: string
  createdAt: Date
  ownerId: string
  attachmentsId: string[]
}
