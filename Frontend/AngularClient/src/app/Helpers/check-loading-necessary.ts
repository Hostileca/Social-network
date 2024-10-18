export function CheckLoadingNecessaryFromBottom(htmlElement: HTMLElement, loadingThreshold: number): boolean {
  const containerHeight = htmlElement.scrollHeight;
  const scrollPosition = htmlElement.scrollTop;
  const visibleHeight = htmlElement.clientHeight;
  const haveIReachedBottom = (scrollPosition + visibleHeight + loadingThreshold) >= containerHeight;

  return haveIReachedBottom
}

export function CheckLoadingNecessaryFromTop(htmlElement: HTMLElement, loadingThreshold: number): boolean {
  const scrollPosition = htmlElement.scrollTop;
  const haveIReachedTop = scrollPosition <= loadingThreshold;

  return haveIReachedTop
}
