import { useEffect, useLayoutEffect } from "react"

export const useLayoutEffectSSR = (effectFn: () => any, deps: any[]): void => {
  if (typeof window !== "undefined") {
    useLayoutEffect(effectFn, deps)
  } else {
    useEffect(effectFn, deps)
  }
}
